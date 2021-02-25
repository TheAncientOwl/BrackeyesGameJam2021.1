using System.Collections.Generic;
using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.General
{
    public class CharactersClicker : MonoBehaviour
    {
        private readonly HashSet<GameObject> m_Selection = new HashSet<GameObject>();
        private Character m_Clicked = null;

        private Camera m_Camera;

        private void Start() => m_Camera = Camera.main;

        private void OnEnable()
        {
            m_Clicked = null;
            m_Selection.Clear();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_Clicked = null;
                Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.transform != null)
                {
                    GameObject obj = hit.transform.gameObject;
                    CharacterAvatar characterAvatar = obj.GetComponent<CharacterAvatar>();
                    if (characterAvatar != null && characterAvatar.CanBeChoosed())
                    {
                        m_Clicked = characterAvatar.GetCharacterManager();
                        m_Selection.Add(obj);
                    }
                }
            }
        }

        public bool Clicked() => m_Clicked != null;

        public Character ExtractLastClicked()
        {
            Character obj = m_Clicked;
            m_Clicked = null;
            return obj;
        }

        public HashSet<GameObject> GetSelection() => m_Selection;
    }
}