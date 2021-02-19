using UnityEngine;

namespace Characters
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float m_MaxHealth = 0f;
        private float m_Health = 0f;

        private void Start()
        {
            m_Health = m_MaxHealth;
        }
    }
}