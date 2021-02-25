using UnityEngine;

namespace Characters.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class JumpManager : MonoBehaviour
    {
        [SerializeField] private float m_JumpForce = 7f;
        [SerializeField] private float m_DoubleJumpForce = 6f;
        [SerializeField] private float m_GroundCheckOffset = 0f;
        [SerializeField] private float m_BoxCastHeight = 0.5f;
        [SerializeField] private LayerMask m_GroundLayerMask = 0;

        private Rigidbody2D m_Rigidbody2D;
        private BoxCollider2D m_BoxCollider2D;

        private bool m_Grounded = false;

        private const float GROUNDED_BUFFER = 0.09f;
        private float m_GroundedBuffer = 0f;

        private const float JUMP_BUFFER = 0.2f;
        private float m_JumpBuffer = 0f;

        private float BACKUP_JUMP_FORCE = 0f;
        private Vector2 BOX_CAST_SIZE = Vector2.zero;

        private bool m_CanDoubleJump = false;

        private void Start()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_BoxCollider2D = GetComponent<BoxCollider2D>();
            this.BACKUP_JUMP_FORCE = m_JumpForce;
            this.BOX_CAST_SIZE = new Vector2(m_BoxCollider2D.bounds.size.x, m_BoxCastHeight);
        }

        private void Update()
        {
            m_GroundedBuffer = m_Grounded ? GROUNDED_BUFFER : m_GroundedBuffer - Time.deltaTime;

            m_JumpBuffer = Input.GetKeyDown(KeyCode.W) ? JUMP_BUFFER : m_JumpBuffer - Time.deltaTime;

            if (m_JumpBuffer > 0f)
            {
                m_JumpBuffer = 0f;
                if (m_GroundedBuffer > 0f) Jump();
                else if (m_CanDoubleJump)  DoubleJump();
            }
        }

        private void FixedUpdate()
        {
            if (m_Rigidbody2D.velocity.y < 1f) 
            {
                m_Grounded = Physics2D.BoxCast
                (
                    origin    : (Vector2)m_BoxCollider2D.bounds.center - new Vector2(0f, m_GroundCheckOffset),
                    size      : this.BOX_CAST_SIZE,
                    layerMask : m_GroundLayerMask,
                    direction : Vector2.down,
                    distance  : 0f,
                    angle     : 0f
                ).collider != null;
            }
        }

        private void OnDrawGizmos()
        {
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

            Gizmos.color = Color.red;

            Gizmos.DrawWireCube
            (
                center: boxCollider2D.bounds.center - new Vector3(0f, m_GroundCheckOffset),
                size: new Vector3(boxCollider2D.bounds.size.x, m_BoxCastHeight)
            );
        }

        private void OnDisable()
        {
            m_GroundedBuffer = 0f;
            m_JumpBuffer = 0f;
            m_CanDoubleJump = false;
        }

        private void Jump()
        {
            m_Grounded = false;
            m_GroundedBuffer = 0f;
            m_CanDoubleJump = true;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
        }

        private void DoubleJump()
        {
            m_CanDoubleJump = false;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_DoubleJumpForce);
        }

        public void SetJumpForce(float jumpForce) => m_JumpForce = jumpForce;

        public void SetDefaultJumpForce() => m_JumpForce = this.BACKUP_JUMP_FORCE;

        public bool IsGrounded() => m_Grounded;
    }
}