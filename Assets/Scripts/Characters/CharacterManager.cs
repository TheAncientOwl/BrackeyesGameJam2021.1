using UnityEngine;
using Characters.Main;

namespace Characters.CharacterTypes
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private RobinHood m_RobinHood = null;
        [SerializeField] private Gorilla m_Gorilla = null;
        [SerializeField] private AppleTree m_AppleTree = null;
        [SerializeField] private Cloud m_Cloud = null;
        [SerializeField] private FireFly m_FireFly = null;
        [SerializeField] private Bird m_Bird = null;

        [SerializeField] private Commons m_Commons = null;

        private Character[] m_Characters;

        private void Start()
        {
            m_Characters = new Character[6];
            m_Characters[0] = m_RobinHood;
            m_Characters[1] = m_Gorilla;
            m_Characters[2] = m_AppleTree;
            m_Characters[3] = m_Cloud;
            m_Characters[4] = m_FireFly;
            m_Characters[5] = m_Bird;
        }

        private void Update()
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
            foreach(var character in m_Characters)
            {
                character.SetCommonMovement(m_Commons);
            }
        }

        void GoSeparate()
        {
            foreach(var character in m_Characters)
            {
                character.SetNormalMovement();
            }
        }

    }
}