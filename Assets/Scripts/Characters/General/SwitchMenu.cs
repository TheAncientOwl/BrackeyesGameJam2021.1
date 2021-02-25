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
        [SerializeField] private CharactersManager m_CharactersManager;
        private AvatarClicker m_AvatarClicker;

        public bool IsOn { get => this.enabled; private set{} }

        public static SwitchMenu Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start() => m_AvatarClicker = GetComponent<AvatarClicker>();

        private void SetEnabled(bool enabled)
        {
            m_AvatarClicker.enabled = enabled;
            this.enabled = enabled;
            m_AvatarsObj.SetActive(enabled);
            if (enabled)
                m_CharactersManager.GetMain().DisableSpecialMechanics();
            else
                m_CharactersManager.GetMain().EnableSpecialMechanics();
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
                    m_CharactersManager.SetMain(newMain);
                    Disable();
                }
            }
        }
    }
}