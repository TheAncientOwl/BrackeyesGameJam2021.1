using UnityEngine;
using Characters.CharacterTypes;
using Characters.General;

namespace Characters.Main.Bird
{
    public class Bird : Character
    {
        private static readonly int s_FLY = Animator.StringToHash("Fly");
        private static readonly int s_IDLE = Animator.StringToHash("Idle");
        private static readonly int s_Walk = Animator.StringToHash("Walk");

        private BirdStateSwitcher m_StateChanger;
        private PlaneMode m_PlaneMode;

        public AirHandler AirHandler { get; private set; }
        public GroundHandler GroundHandler { get; private set; }
        
        new private void Awake()
        {
            base.Awake();

            AirHandler = new AirHandler(this);
            GroundHandler = new GroundHandler(this, disable: true);

            m_StateChanger = GetComponent<BirdStateSwitcher>();
            m_PlaneMode = GetComponent<PlaneMode>();
        }

        private void Update() => SetAnimation();

        private void SetAnimation()
        {
            switch (m_StateChanger.State)
            {
                case BirdState.InAir:
                {
                    m_Animator.SetBool(s_FLY, true);
                    m_Animator.SetBool(s_Walk, false);
                    m_Animator.SetBool(s_IDLE, false);
                    break;
                }
                case BirdState.Grounded:
                {
                    m_Animator.SetBool(s_FLY, false);
                    m_Animator.SetBool(s_IDLE, GetHorizontalDirection() == 0f);
                    m_Animator.SetBool(s_Walk, GetHorizontalDirection() != 0f);
                    break;
                }
            }
        }

        public override void EnableMovement()
        {
            switch (m_StateChanger.State)
            {
                case BirdState.InAir: AirHandler.Enable(); break;
                case BirdState.Grounded: GroundHandler.Enable(); break;
            }
        }

        public override void DisableMovement()
        {
            switch (m_StateChanger.State)
            {
                case BirdState.InAir: AirHandler.Disable(); break;
                case BirdState.Grounded: GroundHandler.Disable(); break;
            }
            m_Rigidbody2D.velocity = Vector2.zero;
        }

        public override void SetCommonMovement(Commons commons)
        {
            switch (m_StateChanger.State)
            {
                case BirdState.InAir: AirHandler.SetCommon(commons); break;
                case BirdState.Grounded: GroundHandler.SetCommon(commons); break;
            }
        }

        public override void SetNormalMovement()
        {
            switch (m_StateChanger.State)
            {
                case BirdState.InAir: AirHandler.SetNormal(); break;
                case BirdState.Grounded: GroundHandler.SetNormal(); break;
            }
        }

        public override float GetHorizontalDirection()
        {
            switch (m_StateChanger.State)
            {
                case BirdState.InAir: return AirHandler.FlyManager.GetHorizontalDirection();
                case BirdState.Grounded: return GroundHandler.RunManager.GetDirection();
            }
            return 0f;
        }

        public override void DisableSpecialMechanics() => m_PlaneMode.enabled = false;

        public override void EnableSpecialMechanics() => m_PlaneMode.enabled = true;
    }
}