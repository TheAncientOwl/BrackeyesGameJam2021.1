using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.General
{
    public class CharactersManager : MonoBehaviour
    {
        [SerializeField] private Commons m_Commons = null;
        [SerializeField] private Character[] m_Characters;
        [SerializeField] private Character m_Main;

        private bool m_CanSwitch = true;

        private void Start()
        {
            foreach (var character in m_Characters)
                character.SetMain(false);

            m_Main.SetMain(true);

            SwitchMenu.Instance.Disable();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) 
                GoTogether();
            else if (Input.GetKeyDown(KeyCode.L)) 
                GoSeparate();

            CheckSwitch();
        }

        private void CheckSwitch()
        {
            if (m_CanSwitch)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (!SwitchMenu.Instance.IsOn)
                        SwitchMenu.Instance.Enable();
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    if (SwitchMenu.Instance.IsOn)
                        SwitchMenu.Instance.Disable();
                }
            }
        }

        public void SetMain(Character newMain)
        {
            if (newMain != null)
            {
                m_Main.SetMain(false);
                m_Main = newMain;
                m_Main.SetMain(true);
                Debug.Log("Now playing as " + m_Main.gameObject.name);
            }
        }

        public Character GetMain() => m_Main;

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