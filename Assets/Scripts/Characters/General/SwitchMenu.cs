using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.General
{
    /// <summary>
    /// Attach to the character avatars game object;
    /// </summary>
    [RequireComponent(typeof(AvatarClicker))]
    public class SwitchMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_AvatarsObj;
        private AvatarClicker m_AvatarClicker;

        public bool IsOn { get => this.enabled; private set{} }

        public static SwitchMenu Instance { get; private set; }

        private void Awake() => Instance = this;

        private void Start() => m_AvatarClicker = GetComponent<AvatarClicker>();

        private void SetEnabled(bool enabled)
        {
            m_AvatarClicker.enabled = enabled;
            this.enabled = enabled;
            m_AvatarsObj.SetActive(enabled);
            if (enabled)
                CharactersManager.Instance.GetMain().DisableSpecialMechanics();
            else
                CharactersManager.Instance.GetMain().EnableSpecialMechanics();
        }

        public void Enable() => SetEnabled(true);

        public void Disable() => SetEnabled(false);

        private void Update()
        {
            if (m_AvatarClicker.Clicked())
            {
                Character newMain = m_AvatarClicker.ExtractLastClicked();
                if (newMain != null)
                {
                    CharactersManager.Instance.SetMain(newMain);
                    Disable();
                }
            }
        }
    }
}