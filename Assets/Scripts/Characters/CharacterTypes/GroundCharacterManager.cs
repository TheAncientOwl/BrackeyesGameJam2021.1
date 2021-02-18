using UnityEngine;

namespace Characters.CharacterTypes
{
    public abstract class GroundCharacterManager : CharacterManager
    {
        protected GroundHandler m_GroundHandler = new GroundHandler();

        new protected void Awake()
        {
            base.Awake();
            m_GroundHandler.Init(this);
        }

        public override void EnableMovement() => m_GroundHandler.Enable();

        public override void DisableMovement()
        {
            m_GroundHandler.Disable();
            m_Rigidbody2D.velocity = Vector2.zero;
        }

        public override void SetCommonMovement(Commons commons) => m_GroundHandler.SetCommon(commons);

        public override void SetNormalMovement() => m_GroundHandler.SetNormal();

        public override float GetHorizontalDirection() => m_GroundHandler.RunManager.GetDirection();
    }
}


