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

        private bool m_Together = false;

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
        }

        private void Update()
        {
            CheckTeamMovement();
            if (!m_Together)
                CheckMainCharacterSwitch();
        }

        void CheckMainCharacterSwitch()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchMain(m_RobinHood);
                Debug.Log("Now playing as RobinHood.");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchMain(m_Gorilla);
                Debug.Log("Now playing as Gorilla.");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchMain(m_AppleTree);
                Debug.Log("Now playing as AppleTree.");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SwitchMain(m_Cloud);
                Debug.Log("Now playing as Cloud.");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SwitchMain(m_FireFly);
                Debug.Log("Now playing as FireFly.");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SwitchMain(m_Bird);
                Debug.Log("Now playing as Bird.");
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
            m_Together = true;
            foreach(var character in m_Characters)
            {
                character.DisableSpecialMechanics();
                character.EnableMovement();
                character.SetCommonMovement(m_Commons);
            }
        }

        void GoSeparate()
        {
            m_Together = false;
            foreach(var character in m_Characters)
            {
                character.DisableMovement();
                character.SetNormalMovement();
            }
            m_Main.EnableMovement();
        }

    }
}