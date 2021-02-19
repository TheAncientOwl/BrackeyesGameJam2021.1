using UnityEngine;
using Characters.CharacterTypes;

namespace Characters
{
    public class CharacterAvatar : MonoBehaviour
    {
        [SerializeField] private CharacterManager m_Character;

        public CharacterManager GetCharacterManager() => m_Character;
    }
}