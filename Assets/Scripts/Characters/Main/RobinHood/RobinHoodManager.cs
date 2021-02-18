using UnityEngine;
using Characters.CharacterTypes;
using Characters.Movement;

namespace Characters.Main.RobinHood
{
    public class RobinHoodManager : GroundCharacterManager
    {
        private static readonly int s_IDLE  = Animator.StringToHash("Idle");
        private static readonly int s_SHOOT = Animator.StringToHash("Shoot");
        private static readonly int s_WALK  = Animator.StringToHash("Walk");
        private static readonly int s_JUMP  = Animator.StringToHash("Jump");
        private static readonly int s_FALL  = Animator.StringToHash("Fall");

        [SerializeField] private GameObject m_Bow;
        private bool m_ShootMode = false;

        private void Start()
        {
            m_Bow.SetActive(false);
        }

        private void Update()
        {
            if (m_IsMain)
            {
                if (Input.GetKeyDown(KeyCode.Space) && GetHorizontalDirection() == 0)
                {
                    m_ShootMode = !m_ShootMode;
                    m_Bow.SetActive(m_ShootMode);
                    if (m_ShootMode)
                        DisableMovement();
                    else
                        EnableMovement();
                }
            }
            m_Animator.SetBool(s_JUMP, m_Rigidbody2D.velocity.y > 0f);
            m_Animator.SetBool(s_FALL, m_Rigidbody2D.velocity.y < 0f);
            m_Animator.SetBool(s_IDLE, GetHorizontalDirection() == 0f);
            m_Animator.SetBool(s_WALK, GetHorizontalDirection() != 0f);
            m_Animator.SetBool(s_SHOOT, m_ShootMode);
        }

        public override void DisableSpecialMechanics()
        {
            m_ShootMode = false;
            m_Bow.SetActive(false);
        }

        public override void EnableSpecialMechanics()
        {
        }
    }
}