using UnityEngine;

namespace Characters.CharacterTypes.Air
{
    [RequireComponent(typeof(Movement.FlyManager))]
    public abstract class AirCharacter : Character
    {
        protected AirHandler m_AirHandler;

        new protected void Awake()
        {
            base.Awake();
            m_AirHandler = new AirHandler(this);
        }

        public override void EnableMovement() => m_AirHandler.Enable();

        public override void DisableMovement()
        {
            m_AirHandler.Disable();
            m_Rigidbody2D.velocity = Vector2.zero;
        }

        public override void SetCommonMovement(General.Commons commons) => m_AirHandler.SetCommon(commons);

        public override void SetNormalMovement() => m_AirHandler.SetNormal();

        public override float GetHorizontalDirection() => m_AirHandler.FlyManager.GetHorizontalDirection();
    }
}
