using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.Movement
{
    public class DoubleJumpHelper : MonoBehaviour
    {
        private JumpManager m_JumpManager;

        private bool m_DoubleJumped = false;
        private void Start() => m_JumpManager = GetComponent<JumpManager>();

        private void Update()
        {
            if (m_JumpManager.IsGrounded())
                m_DoubleJumped = false;
            else if (Input.GetKeyDown(KeyCode.W) && m_JumpManager.Jumped() && !m_DoubleJumped)
            {
                m_JumpManager.Jump();
                m_DoubleJumped = true;
            }
        }

        private void OnDisable() => m_DoubleJumped = false;
    }
}