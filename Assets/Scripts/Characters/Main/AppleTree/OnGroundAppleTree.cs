using UnityEngine;
using Characters.General;

namespace Characters.Main.AppleTree
{
    [RequireComponent(typeof(Animator))]
    public class OnGroundAppleTree : MonoBehaviour
    {
        [SerializeField] private float m_WaitTime = 0.5f;
        [SerializeField] private int m_HealFactor = 2;
        [SerializeField] private Health[] m_CharactersHealth;

        private Animator m_Animator;

        private const int MAX_PHASES = 5;
        private int m_CurrentPhase = 0;

        private float m_PhaseTime = 0f;
        private float m_Timer = 0f;

        private bool m_Healed = false;

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_PhaseTime = m_WaitTime / MAX_PHASES;
        }

        private void OnEnable()
        {
            m_Timer = 0f;
            m_CurrentPhase = 0;
            m_Healed = false;
        }
        
        private void Update()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_PhaseTime)
            {
                m_Timer = 0f;
                m_CurrentPhase++;
                if (m_CurrentPhase < MAX_PHASES)
                    m_Animator.SetTrigger(AnimatorHashes.GO);
                else if (!m_Healed)
                {
                    m_Healed = true;
                    foreach (var health in m_CharactersHealth)
                    {
                        health.Heal(m_HealFactor);
                    }
                }
            }
        }
    }
}