using UnityEngine;
using Characters.CharacterTypes.Ground;

namespace Characters.Main.AppleTree
{
    public class OnCartAppleTree : GroundCharacter
    {
        [SerializeField] private AppleTreeTypeSwitcher m_TypeSwitcher;

        public override void DisableSpecialMechanics() => m_TypeSwitcher.enabled = false;

        public override void EnableSpecialMechanics() => m_TypeSwitcher.enabled = true;
    }
}