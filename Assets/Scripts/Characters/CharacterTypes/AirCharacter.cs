using UnityEngine;
using Characters.Movement;

namespace Characters.CharacterTypes
{
    public class AirCharacter : Character
    {
        protected FlyManager m_FlyManager;

        private void Start() => m_FlyManager = GetComponent<FlyManager>();

        public override void EnableMovement() => m_FlyManager.enabled = true;

        public override void DisableMovement() => m_FlyManager.enabled = false;

        public override void SetCommonMovement(Commons commons) => m_FlyManager.SetSpeed(commons.flySpeed);

        public override void SetNormalMovement() => m_FlyManager.SetDefaultSpeed();

        public override float GetHorizontalDirection() => m_FlyManager.GetHorizontalDirection();

    }
}
