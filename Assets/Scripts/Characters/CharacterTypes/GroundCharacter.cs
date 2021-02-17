using UnityEngine;
using Characters.Movement;

namespace Characters.CharacterTypes
{
    public class GroundCharacter : Character
    {
        protected RunManager m_RunManager;
        protected JumpManager m_JumpManager;

        private void Start()
        {
            m_RunManager = GetComponent<RunManager>();
            m_JumpManager = GetComponent<JumpManager>();
        }

        public override void EnableMovement()
        {
            m_RunManager.enabled = true;
            m_JumpManager.enabled = true;
        }

        public override void DisableMovement()
        {
            m_RunManager.enabled = false;
            m_JumpManager.enabled = false;
        }

        public override void CommonMovement(Commons commons)
        {
            m_RunManager.SetRunSpeed(commons.runSpeed);
            m_JumpManager.SetJumpForce(commons.jumpForce);
        }

        public override void NormalMovement()
        {
            m_RunManager.DefaultRunSpeed();
            m_JumpManager.DefaultJumpForce();
        }

        public override float Direction() => m_RunManager.Direction();

    }
}


