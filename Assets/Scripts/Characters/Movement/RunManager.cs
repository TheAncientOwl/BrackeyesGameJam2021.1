using UnityEngine;

namespace Characters.Movement
{
    public class RunManager : MonoBehaviour
    {
        [SerializeField] private float m_MovementSmoothing = 0.085f;
        [SerializeField] private float m_RunSpeed = 250f;

        private Vector2 m_AuxVelocity = Vector2.zero;
        private float m_Direction = 0f;

        private Rigidbody2D m_Rigidbody2D;

        private float BACKUP_RUN_SPEED = 0f;

        private void Start()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            this.BACKUP_RUN_SPEED = m_RunSpeed;
        }

        private void Update()
        {
            m_Direction = Input.GetAxisRaw("Horizontal");
        }

        private void FixedUpdate()
        {
            float move = m_RunSpeed * m_Direction * Time.fixedDeltaTime;
            m_Rigidbody2D.velocity = Vector2.SmoothDamp
            (
                current: m_Rigidbody2D.velocity,
                target: new Vector2(move, m_Rigidbody2D.velocity.y),
                currentVelocity: ref m_AuxVelocity,
                smoothTime: m_MovementSmoothing
            );
        }

        private void OnDisable()
        {
            m_Direction = 0f;
            m_Rigidbody2D.velocity = Vector2.zero;
        }

        public void SetRunSpeed(float runSpeed)
        {
            m_RunSpeed = runSpeed;
        }

        public void DefaultRunSpeed()
        {
            m_RunSpeed = this.BACKUP_RUN_SPEED;
        }
    }
}