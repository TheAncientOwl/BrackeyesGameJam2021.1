using UnityEngine;
using Characters.Main.AppleTree;
using Characters.Main.Bird;
using Characters.Main.Cloud;
using Characters.Main.FireFly;
using Characters.Main.Gorilla;
using Characters.Main.RobinHood;
using Characters.CharacterTypes;

namespace Characters
{
    public class CharactersManager : MonoBehaviour
    {
        [SerializeField] private RobinHoodManager m_RobinHood = null;
        [SerializeField] private GorillaManager m_Gorilla = null;
        [SerializeField] private AppleTreeManager m_AppleTree = null;
        [SerializeField] private CloudManager m_Cloud = null;
        [SerializeField] private FireFlyManager m_FireFly = null;
        [SerializeField] private BirdManager m_Bird = null;

        [SerializeField] private Commons m_Commons = null;

        private CharacterManager[] m_Characters;
        private CharacterManager m_Main;

        [SerializeField] private GameObject m_CharacterAvatars;

        private bool m_CanSwitch = true;
        private CharactersClicker m_CharactersClicker;

        private void Start()
        {
            m_Characters = new CharacterManager[6];
            m_Characters[0] = m_RobinHood;
            m_Characters[1] = m_Gorilla;
            m_Characters[2] = m_AppleTree;
            m_Characters[3] = m_Cloud;
            m_Characters[4] = m_FireFly;
            m_Characters[5] = m_Bird;

            foreach (var character in m_Characters)
                character.SetMain(false);

            m_Main = m_RobinHood;
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
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                m_CharacterAvatars.SetActive(true);
                m_CharactersClicker.enabled = true;
                return;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
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
        }

        void SwitchMain(CharacterManager character)
        {
            m_Main.SetMain(false);
            m_Main = character;
            m_Main.SetMain(true);
        }

        void CheckTeamMovement()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("Together");
                GoTogether();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("Separate");
                GoSeparate();
            }
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