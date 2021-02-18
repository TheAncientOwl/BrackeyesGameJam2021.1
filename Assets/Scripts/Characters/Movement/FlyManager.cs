using UnityEngine;

namespace Characters.Movement
{
    public class FlyManager : MonoBehaviour
    {
        [SerializeField] private float m_FlySmoothing = 0.085f;
        [SerializeField] private Vector2 m_Speed = new Vector2(250f, 150f);

        private Vector2 m_AuxVelocity = Vector2.zero;
        private Vector2 m_Direction = Vector2.zero;

        private Rigidbody2D m_Rigidbody2D;

        private Vector2 BACKUP_SPEED = Vector2.zero;

        private void Start()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            this.BACKUP_SPEED.x = m_Speed.x;
            this.BACKUP_SPEED.y = m_Speed.y;
        }

        private void Update()
        {
            m_Direction.x = Input.GetAxisRaw("Horizontal");
            m_Direction.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;

            m_Rigidbody2D.velocity = Vector2.SmoothDamp
            (
                current: m_Rigidbody2D.velocity,
                target: new Vector2
                        (
                            x: m_Speed.x * m_Direction.x * dt,
                            y: m_Speed.y * m_Direction.y * dt
                        ),
                currentVelocity: ref m_AuxVelocity,
                smoothTime: m_FlySmoothing
            );
        }

        private void OnDisable() => m_Direction = Vector2.zero;

        public void SetSpeed(Vector2 speed) => m_Speed = speed;

        public void SetDefaultSpeed() => m_Speed = this.BACKUP_SPEED;

        public float GetHorizontalDirection() => m_Direction.x;
    }
}