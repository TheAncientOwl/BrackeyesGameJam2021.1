using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.Movement
{
    public class SpriteFlipper : MonoBehaviour
    {
        private CharacterManager m_Character;
        private bool m_FacingRight = true;

        private void Start() => m_Character = GetComponent<CharacterManager>();

        private void FixedUpdate()
        {
            if ((m_Character.GetHorizontalDirection() > 0f && !m_FacingRight) || (m_Character.GetHorizontalDirection() < 0f && m_FacingRight)) 
            {
                m_FacingRight = !m_FacingRight;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        public bool FacingRight() => m_FacingRight;
    }
}