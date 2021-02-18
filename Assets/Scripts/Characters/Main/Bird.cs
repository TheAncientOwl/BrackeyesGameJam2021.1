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

        [SerializeField] private LayerMask m_GroundLayerMask = 0;
        [SerializeField] private GameObject m_GroundCheckPoint = null;
        [SerializeField] private Vector2 m_GroundCheckSize = new Vector2(0f, 0f);

        [SerializeField] private Vector2 m_FlyBoxColliderSize = new Vector2(0f, 0f);
        [SerializeField] private Vector2 m_FlyBoxColliderOffset = new Vector2(0f, 0f);
        [SerializeField] private Vector2 m_GroundBoxColliderSize = new Vector2(0f, 0f);
        [SerializeField] private Vector2 m_GroundBoxColliderOffset = new Vector2(0f, 0f);

        private BirdState m_State = BirdState.InAir;

        private AirHandler m_AirHandler;
        private GroundHandler m_GroundHandler;

        private BoxCollider2D m_BoxCollider2D;

        new private void Start()
        {
            base.Start();

            m_AirHandler = new AirHandler(this);
            m_GroundHandler = new GroundHandler(this);

            m_GroundHandler.Disable();

            m_BoxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (m_State == BirdState.Grounded && !m_GroundHandler.JumpManager.IsGrounded())
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    m_State = BirdState.InAir;
                    m_GroundHandler.Disable();
                    m_AirHandler.Enable();
                    m_Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                    m_BoxCollider2D.size = m_FlyBoxColliderSize;
                    m_BoxCollider2D.offset = m_FlyBoxColliderOffset;
                }
            }
        }

        private void FixedUpdate()
        {
            if (m_State == BirdState.InAir)
            {
                bool grounded = Physics2D.BoxCast
                (
                    origin: m_GroundCheckPoint.transform.position,
                    size: m_GroundCheckSize,
                    layerMask: m_GroundLayerMask,
                    direction: Vector2.down,
                    distance: 0f,
                    angle: 0f
                ).collider != null;

                if (grounded)
                {
                    m_State = BirdState.Grounded;
                    m_AirHandler.Disable();
                    m_GroundHandler.Enable();
                    m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    m_BoxCollider2D.size = m_GroundBoxColliderSize;
                    m_BoxCollider2D.offset = m_GroundBoxColliderOffset;
                }
            }

            switch (m_State)
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
                    if (GetHorizontalDirection() == 0f)
                    {
                        m_Animator.SetBool(s_IDLE, true);
                        m_Animator.SetBool(s_Walk, false);
                    }   
                    else
                    {
                        m_Animator.SetBool(s_Walk, true);
                        m_Animator.SetBool(s_IDLE, false);
                    }
                    break;
                }
            }   
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(m_GroundCheckPoint.transform.position, m_GroundCheckSize);
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