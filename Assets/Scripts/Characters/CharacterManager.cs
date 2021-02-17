using UnityEngine;

namespace Characters.CharacterTypes
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private RobinHood m_RobinHood;
        [SerializeField] private Gorilla m_Gorilla;
        [SerializeField] private AppleTree m_AppleTree;
        [SerializeField] private Cloud m_Cloud;
        [SerializeField] private FireFly m_FireFly;
        [SerializeField] private Bird m_Bird;

        [SerializeField] private Commons m_Commons;

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
                character.CommonMovement(m_Commons);
            }
        }

        void GoSeparate()
        {
            foreach(var character in m_Characters)
            {
                character.NormalMovement();
            }
        }

    }
}