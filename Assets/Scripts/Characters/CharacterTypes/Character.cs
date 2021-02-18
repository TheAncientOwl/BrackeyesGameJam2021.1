using UnityEngine;

namespace Characters.CharacterTypes
{
    public abstract class Character : MonoBehaviour
    {
        protected Rigidbody2D m_Rigidbody2D;
        protected Animator m_Animator;

        [SerializeField] protected bool m_IsMain = false;

        protected void Start()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
        }

        public bool IsMain() => m_IsMain;

        public abstract void SetMain(bool main);
        public abstract void EnableMovement();
        public abstract void DisableMovement();
        public abstract void SetCommonMovement(Commons commons);
        public abstract void SetNormalMovement();
        public abstract float GetHorizontalDirection();
    }
}