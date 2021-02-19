using UnityEngine;
using Characters.CharacterTypes;

namespace Characters
{
    public class CharactersClicker : MonoBehaviour
    {
        private CharacterManager m_LastClicked = null;

        private Camera m_Camera;

        private void Start() => m_Camera = Camera.main;

        private void OnEnable() => m_LastClicked = null;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_LastClicked = null;
                Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.transform != null)
                {
                    CharacterAvatar characterAvatar = hit.transform.GetComponent<CharacterAvatar>();
                    if (characterAvatar != null)
                    {
                        m_LastClicked = characterAvatar.GetCharacterManager();
                    }
                }
            }
        }

        public CharacterManager ExtractLastClicked()
        {
            CharacterManager obj = m_LastClicked;
            m_LastClicked = null;
            return obj;
        }
    }
}