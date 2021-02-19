using UnityEngine;
using Characters.CharacterTypes;

namespace Characters
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private Commons m_Commons = null;
        [SerializeField] private CharacterManager[] m_Characters;
        [SerializeField] private CharacterManager m_Main;

        [SerializeField] private GameObject m_CharacterAvatars;

        private bool m_CanSwitch = true;
        private CharactersClicker m_CharactersClicker;

        private void Start()
        {
            foreach (var character in m_Characters)
                character.SetMain(false);

            m_Main.SetMain(true);

            m_CharacterAvatars.SetActive(false);
            m_CharactersClicker = GetComponent<CharactersClicker>();
            m_CharactersClicker.enabled = false;
        }

        private void Update()
        {
            CheckTeamMovement();
            if (m_CanSwitch)
                CheckMainCharacterSwitch();
        }

        void CheckMainCharacterSwitch()
        {
            if (m_CharactersClicker.Clicked())
            {
                m_CharacterAvatars.SetActive(false);
                m_CharactersClicker.enabled = true;
                CharacterManager newMain = m_CharactersClicker.ExtractLastClicked();
                if (newMain != null)
                {
                    m_Main.SetMain(false);
                    m_Main = newMain;
                    m_Main.SetMain(true);
                    Debug.Log("Now playing as " + m_Main.gameObject.name);
                }
                return;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                m_CharacterAvatars.SetActive(true);
                m_CharactersClicker.enabled = true;
                return;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_CharacterAvatars.SetActive(false);
                m_CharactersClicker.enabled = false;
            }
        }

        void CheckTeamMovement()
        {
            if (Input.GetKeyDown(KeyCode.K))
                GoTogether();
            else if (Input.GetKeyDown(KeyCode.L))
                GoSeparate();
        }

        void GoTogether()
        {
            m_CanSwitch = false;
            foreach(var character in m_Characters)
            {
                character.DisableSpecialMechanics();
                character.EnableMovement();
                character.SetCommonMovement(m_Commons);
            }
        }

        void GoSeparate()
        {
            m_CanSwitch = true;
            foreach(var character in m_Characters)
            {
                character.DisableMovement();
                character.SetNormalMovement();
            }
            m_Main.EnableMovement();
        }

        public void SetCharacterSwitch(bool value) => m_CanSwitch = value;
    }
}