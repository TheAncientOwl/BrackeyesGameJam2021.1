using UnityEngine;

namespace Characters.General
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private GameObject m_SpriteRendererPrefab;
        [SerializeField] private Sprite m_FullHeartSprite;
        [SerializeField] private Sprite m_EmptyHeartSprite;
        [SerializeField] private int m_MaxHealth = 0;
        [SerializeField] private Transform m_HeartsMiddle;
        [SerializeField] private float m_SpaceBetweenHearts = 0f;

        private SpriteRenderer[] m_SpriteRenderers;

        private int m_Health = 0;

        private void Start()
        {
            m_Health = m_MaxHealth;

            m_SpriteRenderers = new SpriteRenderer[m_Health];

            Vector2 position = new Vector2(m_HeartsMiddle.position.x - m_Health / 2f * m_SpaceBetweenHearts, m_HeartsMiddle.position.y);
            for (int i = 0; i < m_Health; ++i)
            {
                GameObject heart = Instantiate(m_SpriteRendererPrefab, position, Quaternion.identity);
                heart.transform.SetParent(m_HeartsMiddle);
                m_SpriteRenderers[i] = heart.GetComponent<SpriteRenderer>();
                m_SpriteRenderers[i].sprite = m_FullHeartSprite;
                position.x += m_SpaceBetweenHearts;
            }
        }

        private void Die()
        {
            Debug.LogError("DIE LMAO");
        }

        public void TakeDamage(int amount)
        {
            m_Health -= amount;
            if (m_Health > 0)
            {
                for (int i = m_Health; i < m_MaxHealth; ++i)
                {
                    m_SpriteRenderers[i].sprite = m_EmptyHeartSprite;
                }
            }
            else
            {
                m_SpriteRenderers[0].sprite = m_EmptyHeartSprite;
                Die();
            }
        }

        public void Heal(int amount)
        {
            m_Health += amount;
            m_Health = Mathf.Min(m_Health, m_MaxHealth);
            for (int i = 0; i < m_Health; ++i)
            {
                m_SpriteRenderers[i].sprite = m_FullHeartSprite;
            }
        }
    }
}