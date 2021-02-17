using UnityEngine;

namespace Characters.CharacterTypes
{
    public abstract class Character : MonoBehaviour
    {
        public abstract void EnableMovement();
        public abstract void DisableMovement();
        public abstract void CommonMovement(Commons commons);
        public abstract void NormalMovement();
        public abstract float Direction();
    }
}