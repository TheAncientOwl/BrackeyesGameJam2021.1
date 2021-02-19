using UnityEngine;
using Characters.CharacterTypes;

namespace Characters
{
    public class CharacterSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject m_CharacterAvatars;
        [SerializeField] private CharactersClicker m_CharactersClicker;
        [SerializeField] private GeneralManager m_GeneralManager;

        private void OnEnable()
        {
            m_CharacterAvatars.SetActive(true);
            m_CharactersClicker.enabled = true;
            m_GeneralManager.GetMain().DisableSpecialMechanics();
        }

        private void OnDisable()
        {
            if (m_CharacterAvatars != null)
                m_CharacterAvatars.SetActive(false);
            m_CharactersClicker.enabled = false;
            m_GeneralManager.GetMain().EnableSpecialMechanics();
        }

        private void Update()
        {
            if (m_CharactersClicker.Clicked())
            {
                CharacterManager newMain = m_CharactersClicker.ExtractLastClicked();
                if (newMain != null)
                {
                    m_GeneralManager.SetMain(newMain);
                    this.enabled = false;
                }
            }
        }
    }
}