using UnityEngine;

namespace Characters.Main.RobinHood
{
    public class Bow : MonoBehaviour
    {
        [SerializeField] private GameObject m_Arrow;
        [SerializeField] private float m_LaunchForce;
        [SerializeField] private Transform m_ShotPoint;

        [SerializeField] private GameObject m_Point;
        [SerializeField] private int m_PointsNumber;
        [SerializeField] private float m_SpaceBetweenPoints;
        private GameObject[] m_Points;

        private Camera m_Camera;
        private Vector2 m_Direction;

        private bool m_FacingRight = true;

        private void Awake() => m_Camera = Camera.main;

        private void Start()
        {
            m_Points = new GameObject[m_PointsNumber];
            for (int i = 0; i < m_PointsNumber; ++i)
            {
                m_Points[i] = Instantiate(m_Point, m_ShotPoint.position, Quaternion.identity);
            }

        }

        private void Update()
        {
            Vector2 bowPos = transform.position;
            Vector2 mousePos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            m_Direction = mousePos - bowPos;
            transform.right = m_Direction;

            if (Input.GetMouseButtonDown(0))
            {
                GameObject newArrow = Instantiate(m_Arrow, m_ShotPoint.position, m_ShotPoint.rotation);
                newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * m_LaunchForce;
            }

            for (int i = 0; i < m_PointsNumber; ++i)
            {
                m_Points[i].transform.position = PointPosition(i * m_SpaceBetweenPoints);
            }
        }

        private Vector2 PointPosition(float t)
        {
            return (Vector2)m_ShotPoint.position + (m_Direction.normalized * m_LaunchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        }

        private void OnEnable()
        {
            if (m_Points != null)
                foreach (var point in m_Points)
                    if (point != null)
                        point.SetActive(true);
        }

        private void OnDisable()
        {
            if (m_Points != null)
                foreach (var point in m_Points)
                    if (point != null)
                        point.SetActive(false);
        }

        public void ForceFacingRight()
        {
            m_FacingRight = true;
            Vector3 scale = transform.localScale;
            if (scale.x < 0)
                scale.x *= -1;
            transform.localScale = scale;
        }

        public void ForceFacingLeft()
        {
            m_FacingRight = false;
            Vector3 scale = transform.localScale;
            if (scale.x > 0)
                scale.x *= -1;
            transform.localScale = scale;
        }
    }
}