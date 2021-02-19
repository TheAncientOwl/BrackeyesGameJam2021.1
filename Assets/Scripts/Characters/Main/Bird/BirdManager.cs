using System.Collections.Generic;
using UnityEngine;
using Characters.CharacterTypes;
using Characters.Movement;

namespace Characters.Main.Bird
{
    public class BirdManager : CharacterManager
    {
        private static readonly int s_FLY  = Animator.StringToHash("Fly");
        private static readonly int s_IDLE = Animator.StringToHash("Idle");
        private static readonly int s_Walk = Animator.StringToHash("Walk");

        private const float MINIMIZE_FACTOR = 4f;

        [SerializeField] private CharactersManager m_CharactersManager;

        [SerializeField] private LayerMask m_GroundLayerMask = 0;
        [SerializeField] private GameObject m_GroundCheckPoint = null;
        [SerializeField] private Vector2 m_GroundCheckSize = Vector2.zero;

        [SerializeField] private Vector2 m_FlyBoxColliderSize = Vector2.zero;
        [SerializeField] private Vector2 m_FlyBoxColliderOffset = Vector2.zero;
        [SerializeField] private Vector2 m_GroundBoxColliderSize = Vector2.zero;
        [SerializeField] private Vector2 m_GroundBoxColliderOffset = Vector2.zero;

        [SerializeField] private float m_PlaneModeCheckRadius = 0f;
        [SerializeField] private LayerMask m_CharacterLayerMask = 0;
        [SerializeField] private GameObject m_CharacterManager;
        [SerializeField] private Transform m_PlanePoint;
        private LinkedList<GameObject> m_Characters = new LinkedList<GameObject>();
        private bool m_PlaneMode = false;

        private BirdState m_State = BirdState.InAir;

        private AirHandler m_AirHandler = new AirHandler();
        private GroundHandler m_GroundHandler = new GroundHandler();

        private BoxCollider2D m_BoxCollider2D;

        private SpriteFlipper m_SpriteFlipper;
        
        new private void Awake()
        {
            base.Awake();

            m_AirHandler.Init(this);
            m_GroundHandler.Init(this);
            m_GroundHandler.Disable();

            m_BoxCollider2D = GetComponent<BoxCollider2D>();
            m_SpriteFlipper = GetComponent<SpriteFlipper>();
        }

        private void Update()
        {
            CheckFly();
            if (m_IsMain && Input.GetKeyDown(KeyCode.Space))
            {
                m_PlaneMode = !m_PlaneMode;
                if (m_PlaneMode)
                    CollectCharacters();
                else
                    ReleaseCharacters();
            }
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            SetAnimation();
        }

        private void CheckFly()
        {
            if (Input.GetKeyDown(KeyCode.W) && m_State == BirdState.Grounded && !m_GroundHandler.JumpManager.IsGrounded())
            {
                m_State = BirdState.InAir;
                m_GroundHandler.Disable();
                m_AirHandler.Enable();
                m_Rigidbody2D.gravityScale = 0f;
                m_BoxCollider2D.size = m_FlyBoxColliderSize;
                m_BoxCollider2D.offset = m_FlyBoxColliderOffset;
            }
        }

        private void CheckGrounded()
        {
            if (m_State != BirdState.InAir)
                return;

            bool grounded = Physics2D.BoxCast
            (
                origin: m_GroundCheckPoint.transform.position,
                size: m_GroundCheckSize,
                layerMask: m_GroundLayerMask,
                direction: Vector2.down,
                distance: 0f,
                angle: 0f
            ).collider != null;

            if (grounded)
            {
                m_State = BirdState.Grounded;
                m_AirHandler.Disable();
                m_GroundHandler.Enable();
                m_Rigidbody2D.gravityScale = 3f;
                m_BoxCollider2D.size = m_GroundBoxColliderSize;
                m_BoxCollider2D.offset = m_GroundBoxColliderOffset;
            }
        }

        private void CollectCharacters()
        {
            m_CharactersManager.SetCharacterSwitch(false);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_BoxCollider2D.bounds.center, m_PlaneModeCheckRadius, m_CharacterLayerMask);

            if (colliders.Length == 0)
                return;

            foreach (var collider in colliders)
            {
                GameObject obj = collider.gameObject;
                if (obj != this.gameObject)
                {
                    m_Characters.AddLast(obj);
                    obj.transform.localScale /= MINIMIZE_FACTOR;
                    obj.transform.position = m_PlanePoint.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.1f, 0.1f));
                    obj.transform.SetParent(this.transform);
                    obj.GetComponent<Rigidbody2D>().isKinematic = true;
                }
            }
        }

        private void ReleaseCharacters()
        {
            m_CharactersManager.SetCharacterSwitch(true);
            foreach (var character in m_Characters)
            {
                character.transform.localScale *= MINIMIZE_FACTOR;
                character.transform.SetParent(m_CharacterManager.transform);
                character.GetComponent<Rigidbody2D>().isKinematic = false;
                if (m_SpriteFlipper.FacingRight())
                    character.GetComponent<SpriteFlipper>().ForceFacingRight();
                else
                    character.GetComponent<SpriteFlipper>().ForceFacingLeft();
            }
            m_Characters.Clear();
        }

        private void SetAnimation()
        {
            switch (m_State)
            {
                case BirdState.InAir:
                    {
                        m_Animator.SetBool(s_FLY, true);
                        m_Animator.SetBool(s_Walk, false);
                        m_Animator.SetBool(s_IDLE, false);
                        break;
                    }
                case BirdState.Grounded:
                    {
                        m_Animator.SetBool(s_FLY, false);
                        if (GetHorizontalDirection() == 0f)
                        {
                            m_Animator.SetBool(s_IDLE, true);
                            m_Animator.SetBool(s_Walk, false);
                        }
                        else
                        {
                            m_Animator.SetBool(s_Walk, true);
                            m_Animator.SetBool(s_IDLE, false);
                        }
                        break;
                    }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(GetComponent<BoxCollider2D>().bounds.center, m_PlaneModeCheckRadius);
            Gizmos.DrawWireCube(m_GroundCheckPoint.transform.position, m_GroundCheckSize);
        }

        public override void EnableMovement()
        {
            switch (m_State)
            {
                case BirdState.InAir: m_AirHandler.Enable(); break;
                case BirdState.Grounded: m_GroundHandler.Enable(); break;
            }
        }

        public override void DisableMovement()
        {
            switch (m_State)
            {
                case BirdState.InAir: m_AirHandler.Disable(); break;
                case BirdState.Grounded: m_GroundHandler.Disable(); break;
            }
            m_Rigidbody2D.velocity = Vector2.zero;
        }

        public override void SetCommonMovement(Commons commons)
        {
            switch (m_State)
            {
                case BirdState.InAir: m_AirHandler.SetCommon(commons); break;
                case BirdState.Grounded: m_GroundHandler.SetCommon(commons); break;
            }
        }

        public override void SetNormalMovement()
        {
            switch (m_State)
            {
                case BirdState.InAir: m_AirHandler.SetNormal(); break;
                case BirdState.Grounded: m_GroundHandler.SetNormal(); break;
            }
        }

        public override float GetHorizontalDirection()
        {
            switch (m_State)
            {
                case BirdState.InAir: return m_AirHandler.FlyManager.GetHorizontalDirection();
                case BirdState.Grounded: return m_GroundHandler.RunManager.GetDirection();
            }
            return 0f;
        }

        public override void DisableSpecialMechanics()
        {
        }

        public override void EnableSpecialMechanics()
        {
        }
    }
}