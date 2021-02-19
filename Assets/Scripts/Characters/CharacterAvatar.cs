using UnityEngine;
using Characters.CharacterTypes;

namespace Characters
{
    public class CharacterAvatar : MonoBehaviour
    {
        [SerializeField] private Sprite m_UnlockedSprite;
        [SerializeField] private Sprite m_LockedSprite;
        [SerializeField] private Sprite m_BannedSprite;
        [SerializeField] private CharacterManager m_Character;
        [SerializeField] private bool m_Unlocked = true;
        [SerializeField] private bool m_Banned;

        private SpriteRenderer m_SpriteRenderer;

        private void Awake() => m_SpriteRenderer = GetComponent<SpriteRenderer>();

        private void OnDisable() => m_Banned = false;

        private void Update()
        {
            if (m_Banned)
                m_SpriteRenderer.sprite = m_BannedSprite;
            else
                m_SpriteRenderer.sprite = m_Unlocked ? m_UnlockedSprite : m_LockedSprite;
        }

        public void SetBanned(bool banned) => m_Banned = banned;

        public CharacterManager GetCharacterManager() => m_Character;

        public bool CanBeChoosed() => m_Unlocked && !m_Banned;
    }
}