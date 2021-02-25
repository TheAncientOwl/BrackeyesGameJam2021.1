using UnityEngine;

namespace Characters.CharacterTypes.Ground
{
    [RequireComponent(typeof(Movement.RunManager))]
    [RequireComponent(typeof(Movement.JumpManager))]
    public abstract class GroundCharacter : Character
    {
        protected GroundHandler m_GroundHandler;

        new protected void Awake()
        {
            base.Awake();
            m_GroundHandler = new GroundHandler(this);
        }

        public override void EnableMovement() => m_GroundHandler.Enable();

        public override void DisableMovement()
        {
            m_GroundHandler.Disable();
            m_Rigidbody2D.velocity = Vector2.zero;
        }

        public override void SetCommonMovement(General.Commons commons) => m_GroundHandler.SetCommon(commons);

        public override void SetNormalMovement() => m_GroundHandler.SetNormal();

        public override float GetHorizontalDirection() => m_GroundHandler.RunManager.GetDirection();
    }
}


