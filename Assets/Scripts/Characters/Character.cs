using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour
    {
        public abstract void EnableMovement();
        public abstract void DisableMovement();

        public abstract void CommonMovement(Commons commons);

        public abstract void NormalMovement();
    }
}