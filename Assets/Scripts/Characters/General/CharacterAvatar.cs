using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.General
{
    public class CharacterAvatar : MonoBehaviour
    {
        [SerializeField] private SpritePack m_Sprites;
        [SerializeField] private Character m_Character;
        [SerializeField] private bool m_Unlocked = true;
        [SerializeField] private bool m_Banned = false;

        private SpriteRenderer m_SpriteRenderer;

        private void Awake() => m_SpriteRenderer = GetComponent<SpriteRenderer>();

        private void OnDisable() => m_Banned = false;

        private void OnEnable() => SetBanned(m_Banned);

        public void SetBanned(bool banned)
        {
            m_Banned = banned;
            m_SpriteRenderer.sprite = m_Banned ? m_Sprites.banned : (m_Unlocked ? m_Sprites.unlocked : m_Sprites.locked);
        }

        public Character GetCharacterManager() => m_Character;

        public bool CanBeChoosed() => m_Unlocked && !m_Banned;
    }

    [System.Serializable]
    public struct SpritePack
    {
        public Sprite locked;
        public Sprite unlocked;
        public Sprite banned;
    }
}