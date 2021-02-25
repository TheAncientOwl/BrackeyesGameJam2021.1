using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.Movement
{
    public class SpriteFlipper : MonoBehaviour
    {
        private Character m_Character;
        private bool m_FacingRight = true;

        private void Start() => m_Character = GetComponent<Character>();

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

        public void ForceFacingRight()
        {
            m_FacingRight = true;
            Vector3 scale = transform.localScale;
            if (scale.x < 0)
                scale.x *= -1;
            transform.localScale = scale;
        }

        public void ForceFacingLeft()
        {
            m_FacingRight = false;
            Vector3 scale = transform.localScale;
            if (scale.x > 0)
                scale.x *= -1;
            transform.localScale = scale;
        }
    }
}