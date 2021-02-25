using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.General
{
    public class CharacterSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject m_CharacterAvatars;
        [SerializeField] private CharactersClicker m_CharactersClicker;
        [SerializeField] private CharactersManager m_CharactersManager;

        private void OnEnable()
        {
            m_CharacterAvatars.SetActive(true);
            m_CharactersClicker.enabled = true;
            m_CharactersManager.GetMain().DisableSpecialMechanics();
        }

        private void OnDisable()
        {
            if (m_CharacterAvatars != null)
                m_CharacterAvatars.SetActive(false);
            m_CharactersClicker.enabled = false;
            m_CharactersManager.GetMain().EnableSpecialMechanics();
        }

        private void Update()
        {
            if (m_CharactersClicker.Clicked())
            {
                Character newMain = m_CharactersClicker.ExtractLastClicked();
                if (newMain != null)
                {
                    m_CharactersManager.SetMain(newMain);
                    this.enabled = false;
                }
            }
        }
    }
}