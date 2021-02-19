using UnityEngine;
using Characters.General;

namespace Characters.CharacterTypes
{
    public abstract class CharacterManager : MonoBehaviour
    {
        [SerializeField] private CharacterAvatar m_Avatar;

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

        public CharacterAvatar GetAvatar() => m_Avatar;

        public abstract void EnableMovement();
        public abstract void DisableMovement();
        public abstract void SetCommonMovement(Commons commons);
        public abstract void SetNormalMovement();
        public abstract float GetHorizontalDirection();
        public abstract void EnableSpecialMechanics();
        public abstract void DisableSpecialMechanics();
    }
}