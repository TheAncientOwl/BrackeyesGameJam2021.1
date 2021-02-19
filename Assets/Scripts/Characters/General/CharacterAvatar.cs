using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.General
{
    public class CharacterAvatar : MonoBehaviour
    {
        [SerializeField] private Sprite m_UnlockedSprite;
        [SerializeField] private Sprite m_LockedSprite;
        [SerializeField] private Sprite m_BannedSprite;
        [SerializeField] private CharacterManager m_CharacterManager;
        [SerializeField] private bool m_Unlocked = true;
        
        private bool m_Banned = false;
        private SpriteRenderer m_SpriteRenderer;

        private void Awake() => m_SpriteRenderer = GetComponent<SpriteRenderer>();

        private void OnDisable() => m_Banned = false;

        private void OnEnable() => SetBanned(m_Banned);

        public void SetBanned(bool banned)
        {
            m_Banned = banned;
            if (m_Banned)
                m_SpriteRenderer.sprite = m_BannedSprite;
            else
                m_SpriteRenderer.sprite = m_Unlocked ? m_UnlockedSprite : m_LockedSprite;
        }

        public CharacterManager GetCharacterManager() => m_CharacterManager;

        public bool CanBeChoosed() => m_Unlocked && !m_Banned;
    }
}