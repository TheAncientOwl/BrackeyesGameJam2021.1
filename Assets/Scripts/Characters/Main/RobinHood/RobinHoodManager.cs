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

        private bool m_Shooting = false;

        private void Update()
        {
            if (m_IsMain)
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
        }

        public override void DisableSpecialMechanics()
        {
        }

        public override void EnableSpecialMechanics()
        {
        }
    }
}