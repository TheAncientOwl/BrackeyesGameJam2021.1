using UnityEngine;
using Characters.Movement;
using Characters.CharacterTypes;

namespace Characters.Main.Gorilla
{
    public class GorillaManager : GroundCharacterManager
    {
        private static readonly int s_WALK = Animator.StringToHash("Walk");
        private static readonly int s_IN_AIR = Animator.StringToHash("InAir");
        private static readonly int s_LEFT_PUNCH = Animator.StringToHash("LeftPunch");
        private static readonly int s_RIGHT_PUNCH = Animator.StringToHash("RightPunch");

        [SerializeField] private Vector2 m_PunchHitBox = Vector2.zero;
        [SerializeField] private Vector2 m_PunchHitBoxOffset = Vector2.zero;
        [SerializeField] private LayerMask m_BreakableObjectsLayerMask;
        [SerializeField] private BoxCollider2D m_BoxCollider2D;
        private SpriteFlipper m_SpriteFlipper;
        private bool m_LeftPunch = true;

        private void Start()
        {
            m_BoxCollider2D = GetComponent<BoxCollider2D>();
            m_SpriteFlipper = GetComponent<SpriteFlipper>();
        }

        private void Update()
        {
            TryMechanic();
            SetAnimation();
        }

        void TryMechanic()
        {
            if (m_IsMain && Input.GetKeyDown(KeyCode.Space))
            {
                m_Animator.SetTrigger(m_LeftPunch ? s_LEFT_PUNCH : s_RIGHT_PUNCH);

                RaycastHit2D[] colliders = Physics2D.BoxCastAll
                (
                    origin    : (Vector2)m_BoxCollider2D.bounds.center + (m_SpriteFlipper.FacingRight() ? m_PunchHitBoxOffset : NegativeX(m_PunchHitBoxOffset)),
                    direction : m_SpriteFlipper.FacingRight() ? Vector2.right : Vector2.left,
                    layerMask : m_BreakableObjectsLayerMask,
                    size      : m_PunchHitBox,
                    angle     : 0f,
                    distance  : 0f
                );

                if (colliders.Length > 0)
                {
                    foreach (var obj in colliders)
                    {
                        BreakableObject breakableObject = obj.collider.gameObject.GetComponent<BreakableObject>();
                        if (breakableObject.IsAlive())
                            breakableObject.Break();
                    }
                }
                
                m_LeftPunch = !m_LeftPunch;
            }
        }

        private Vector2 NegativeX(Vector2 vector2)
        {
            vector2.x *= -1;
            return vector2;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            if (m_BoxCollider2D != null && m_SpriteFlipper != null)
            {
                Gizmos.DrawWireCube(GetComponent<BoxCollider2D>().bounds.center + 
                    (Vector3)(m_SpriteFlipper.FacingRight() ? m_PunchHitBoxOffset : NegativeX(m_PunchHitBoxOffset)), m_PunchHitBox);
            }
            else
                Gizmos.DrawWireCube(GetComponent<BoxCollider2D>().bounds.center + (Vector3)m_PunchHitBoxOffset, m_PunchHitBox);
        }

        private void SetAnimation()
        {
            m_Animator.SetBool(s_WALK, GetHorizontalDirection() != 0f);
            m_Animator.SetBool(s_IN_AIR, m_Rigidbody2D.velocity.normalized.y != 0f);
        }

        public override void DisableSpecialMechanics()
        {
        }

        public override void EnableSpecialMechanics()
        {
        }
    }
}