using UnityEngine;

namespace Characters.CharacterTypes
{
    public abstract class Character : MonoBehaviour
    {
        protected Rigidbody2D m_Rigidbody2D;

        protected void Start() => m_Rigidbody2D = GetComponent<Rigidbody2D>();

        public abstract void EnableMovement();
        public abstract void DisableMovement();
        public abstract void SetCommonMovement(Commons commons);
        public abstract void SetNormalMovement();
        public abstract float GetHorizontalDirection();
        public float GetVelocityY() => m_Rigidbody2D.velocity.y;
    }
}