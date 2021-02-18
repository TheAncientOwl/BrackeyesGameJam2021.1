using UnityEngine;

namespace Characters.Main.RobinHood
{
    public class Arrow : MonoBehaviour
    {
        private const float LIFE_TIME = 5f;
        private Rigidbody2D m_Rigidbody2D;
        private bool m_HasHit = false;

        private void Start()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            Destroy(this.gameObject, LIFE_TIME);
        }

        private void Update()
        {
            if (!m_HasHit)
            {
                transform.rotation = Quaternion.AngleAxis
                (
                    angle: Mathf.Atan2(m_Rigidbody2D.velocity.y, m_Rigidbody2D.velocity.x) * Mathf.Rad2Deg,
                    axis: Vector3.forward
                );
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            m_HasHit = true;
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.isKinematic = true;
        }
    }
}