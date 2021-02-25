using UnityEngine;

namespace Characters.CharacterTypes
{
    [RequireComponent(typeof(Movement.SpriteFlipper))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class Character : MonoBehaviour
    {
        protected Rigidbody2D m_Rigidbody2D;
        protected Animator m_Animator;

        protected bool m_IsMain = false;

        protected void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
        }

        public bool IsMain() => m_IsMain;

        public void SetMain(bool main)
        {
            if (main)
            {
                EnableSpecialMechanics();
                EnableMovement();
            }
            else
            {
                DisableSpecialMechanics();
                DisableMovement();
            }
            m_IsMain = main;
        }

        public abstract void EnableMovement();
        public abstract void DisableMovement();
        public abstract void SetCommonMovement(General.Commons commons);
        public abstract void SetNormalMovement();
        public abstract float GetHorizontalDirection();
        public abstract void EnableSpecialMechanics();
        public abstract void DisableSpecialMechanics();
    }
}