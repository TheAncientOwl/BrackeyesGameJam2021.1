using UnityEngine;
using Characters.Movement;

namespace Characters.CharacterTypes
{
    public class GroundHandler
    {
        public RunManager RunManager { get; private set; }
        public JumpManager JumpManager { get; private set; }

        public GroundHandler(Character character)
        {
            RunManager = character.gameObject.GetComponent<RunManager>();
            JumpManager = character.gameObject.GetComponent<JumpManager>();
        }

        public void Enable()
        {
            RunManager.enabled = true;
            JumpManager.enabled = true;
        }

        public void Disable()
        {
            RunManager.enabled = false;
            JumpManager.enabled = false;
        }

        public void SetCommon(Commons commons)
        {
            RunManager.SetRunSpeed(commons.runSpeed);
            JumpManager.SetJumpForce(commons.jumpForce);
        }

        public void SetNormal()
        {
            RunManager.SetDefaultRunSpeed();
            JumpManager.SetDefaultJumpForce();
        }
    }
}