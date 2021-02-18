using UnityEngine;
using Characters.CharacterTypes;
using Characters.Main.BirdUtils;

namespace Characters.Main
{
    public class Bird : Character
    {
        private static readonly int s_FLY  = Animator.StringToHash("Fly");
        private static readonly int s_IDLE = Animator.StringToHash("Idle");
        private static readonly int s_Walk = Animator.StringToHash("Walk");
        private int m_LastHash = 0;

        private BirdState m_State = BirdState.InAir;

        private AirHandler m_AirHandler = new AirHandler();
        private GroundHandler m_GroundHandler = new GroundHandler();

        new private void Start()
        {
            base.Start();

            m_LastHash = s_FLY;

            m_AirHandler.Init(this);
            m_GroundHandler.Init(this);

            m_GroundHandler.Disable();
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

        public override void SetMain(bool main)
        {
            m_IsMain = main;
            if (main)
            {
                EnableMovement();
            }
            else
            {
                DisableMovement();
            }
        }

    }
}