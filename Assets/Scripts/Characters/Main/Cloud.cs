using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.Main
{
    public class Cloud : AirCharacter
    {
        private static readonly int s_IDLE    = Animator.StringToHash("Idle");
        private static readonly int s_VERTICAL_MOVE   = Animator.StringToHash("VerticalMove");
        private static readonly int s_HORIZONTAL_MOVE = Animator.StringToHash("HorizontalMove");
        private int m_LastHash = 0;

        new private void Start()
        {
            base.Start();

            m_LastHash = GetNormalizedVelocity().y != 0f ? s_VERTICAL_MOVE : (GetHorizontalDirection() == 0f ? s_IDLE : s_HORIZONTAL_MOVE);
            m_Animator.SetBool(m_LastHash, true);
        }

        private void Update()
        {
            int  newHash = GetNormalizedVelocity().y != 0f ? s_VERTICAL_MOVE : (GetHorizontalDirection() == 0f ? s_IDLE : s_HORIZONTAL_MOVE);

            if (m_LastHash != newHash)
            {
                m_Animator.SetBool(m_LastHash, false);
                m_Animator.SetBool(newHash, true);
                m_LastHash = newHash;
            }
        }
    }
}