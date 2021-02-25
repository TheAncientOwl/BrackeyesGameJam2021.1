using UnityEngine;

namespace Characters.Main.Bird
{
    public class BirdStateSwitcher : MonoBehaviour
    {
        [SerializeField] private LayerMask m_GroundLayerMask = 0;
        [SerializeField] private GameObject m_GroundCheckPoint = null;
        [SerializeField] private Vector2 m_GroundCheckSize = Vector2.zero;

        [SerializeField] private Vector2 m_FlyBoxColliderSize = Vector2.zero;
        [SerializeField] private Vector2 m_FlyBoxColliderOffset = Vector2.zero;
        [SerializeField] private Vector2 m_GroundBoxColliderSize = Vector2.zero;
        [SerializeField] private Vector2 m_GroundBoxColliderOffset = Vector2.zero;

        public BirdState State { get; private set; }

        private Bird m_BirdManager;

        private Rigidbody2D m_Rigidbody2D;
        private BoxCollider2D m_BoxCollider2D;

        private void Awake() => State = BirdState.InAir;

        private void Start()
        {
            m_BirdManager = GetComponent<Bird>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_BoxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            ///Fly check
            if (Input.GetKeyDown(KeyCode.W) && State == BirdState.Grounded && !m_BirdManager.GroundHandler.JumpManager.IsGrounded())
            {
                State = BirdState.InAir;
                m_BirdManager.GroundHandler.Disable();
                m_BirdManager.AirHandler.Enable();
                m_Rigidbody2D.gravityScale = 0f;
                m_BoxCollider2D.size = m_FlyBoxColliderSize;
                m_BoxCollider2D.offset = m_FlyBoxColliderOffset;
            }
        }

        private void FixedUpdate()
        {
            ///Ground check
            if (State == BirdState.InAir)
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
                    State = BirdState.Grounded;
                    m_BirdManager.AirHandler.Disable();
                    m_BirdManager.GroundHandler.Enable();
                    m_Rigidbody2D.gravityScale = 3f;
                    m_BoxCollider2D.size = m_GroundBoxColliderSize;
                    m_BoxCollider2D.offset = m_GroundBoxColliderOffset;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(m_GroundCheckPoint.transform.position, m_GroundCheckSize);
        }
    }
}