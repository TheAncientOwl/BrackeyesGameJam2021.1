using UnityEngine;
using Characters.CharacterTypes;
using Characters.Movement;

namespace Characters.Main
{
    public class RobinHood : GroundCharacter
    {
        private static readonly int s_IDLE  = Animator.StringToHash("Idle");
        private static readonly int s_SHOOT = Animator.StringToHash("Shoot");
        private static readonly int s_WALK  = Animator.StringToHash("Walk");
        private static readonly int s_JUMP  = Animator.StringToHash("Jump");
        private static readonly int s_FALL  = Animator.StringToHash("Fall");

        private DoubleJumpHelper m_DoubleJumpHelper;

        private bool m_Shooting = false;

        new private void Start()
        {
            base.Start();
            m_DoubleJumpHelper = GetComponent<DoubleJumpHelper>();
        }

        private void Update()
        {
            if (m_Shooting)
            {
                
            }
            else
            {
                m_Animator.SetBool(s_JUMP, m_Rigidbody2D.velocity.y > 0f);
                m_Animator.SetBool(s_FALL, m_Rigidbody2D.velocity.y < 0f);
                m_Animator.SetBool(s_IDLE, GetHorizontalDirection() == 0f);
                m_Animator.SetBool(s_WALK, GetHorizontalDirection() != 0f);
            }
        }

        public override void SetMain(bool main)
        {
            m_DoubleJumpHelper.enabled = main;
            m_IsMain = main;
        }
    }
}