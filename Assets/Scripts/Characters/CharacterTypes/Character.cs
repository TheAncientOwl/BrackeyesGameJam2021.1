using UnityEngine;

namespace Characters.CharacterTypes
{
    public abstract class Character : MonoBehaviour
    {
        public abstract void EnableMovement();
        public abstract void DisableMovement();
        public abstract void SetCommonMovement(Commons commons);
        public abstract void SetNormalMovement();
        public abstract float GetHorizontalDirection();
    }
}