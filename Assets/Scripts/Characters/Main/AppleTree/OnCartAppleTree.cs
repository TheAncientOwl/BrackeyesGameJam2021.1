using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.Main.AppleTree
{
    public class OnCartAppleTree : GroundCharacter
    {
        [SerializeField] private AppleTreeTypeSwitcher m_VariantChooser;

        public override void DisableSpecialMechanics()
        {
            m_VariantChooser.enabled = false;
        }

        public override void EnableSpecialMechanics()
        {
            m_VariantChooser.enabled = true;
        }
    }
}