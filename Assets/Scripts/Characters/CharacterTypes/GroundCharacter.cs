using UnityEngine;

namespace Characters.CharacterTypes
{
    public abstract class GroundCharacter : Character
    {
        protected GroundHandler m_GroundHandler;

        new protected void Start()
        {
            base.Start();
            m_GroundHandler = new GroundHandler(this);
        }

        public override void EnableMovement() => m_GroundHandler.Enable();

        public override void DisableMovement() => m_GroundHandler.Disable();

        public override void SetCommonMovement(Commons commons) => m_GroundHandler.SetCommon(commons);

        public override void SetNormalMovement() => m_GroundHandler.SetNormal();

        public override float GetHorizontalDirection() => m_GroundHandler.RunManager.GetDirection();
    }
}


