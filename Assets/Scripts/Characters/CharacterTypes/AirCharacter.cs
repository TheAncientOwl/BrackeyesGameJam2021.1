using UnityEngine;

namespace Characters.CharacterTypes
{
    public abstract class AirCharacter : Character
    {
        protected AirHandler m_AirHandler;

        new protected void Start()
        {
            base.Start();
            m_AirHandler = new AirHandler();
            m_AirHandler.Init(this);
        }

        public override void EnableMovement() => m_AirHandler.Enable();

        public override void DisableMovement() => m_AirHandler.Disable();

        public override void SetCommonMovement(Commons commons) => m_AirHandler.SetCommon(commons);

        public override void SetNormalMovement() => m_AirHandler.SetNormal();

        public override float GetHorizontalDirection() => m_AirHandler.FlyManager.GetHorizontalDirection();
    }
}
