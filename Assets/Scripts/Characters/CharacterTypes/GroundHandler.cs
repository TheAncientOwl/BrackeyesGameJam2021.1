using UnityEngine;
using Characters.Movement;

namespace Characters.CharacterTypes
{
    public class GroundHandler
    {
        public RunManager RunManager { get; private set; }
        public JumpManager JumpManager { get; private set; }

        public GroundHandler(CharacterManager character, bool disable = false)
        {
            RunManager = character.gameObject.GetComponent<RunManager>();
            JumpManager = character.gameObject.GetComponent<JumpManager>();
            if (disable)
                Disable();
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