using UnityEngine;
using Characters.CharacterTypes;

namespace Characters
{
    public class CharactersClicker : MonoBehaviour
    {
        private CharacterManager m_Clicked = null;

        private Camera m_Camera;

        private void Start() => m_Camera = Camera.main;

        private void OnEnable() => m_Clicked = null;

        private void OnDisable() => m_Clicked = null;

        private void Update()
        {
            if (!m_Clicked && Input.GetMouseButtonDown(0))
            {
                m_Clicked = null;
                Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.transform != null)
                {
                    CharacterAvatar characterAvatar = hit.transform.GetComponent<CharacterAvatar>();
                    if (characterAvatar != null && characterAvatar.CanBeChoosed())
                    {
                        m_Clicked = characterAvatar.GetCharacterManager();
                    }
                }
            }
        }

        public bool Clicked() => m_Clicked != null;

        public CharacterManager ExtractLastClicked()
        {
            CharacterManager obj = m_Clicked;
            m_Clicked = null;
            return obj;
        }
    }
}