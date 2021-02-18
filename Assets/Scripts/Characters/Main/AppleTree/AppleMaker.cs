using UnityEngine;

namespace Characters.Main.AppleTree
{
    public class AppleMaker : MonoBehaviour
    {
        private static readonly int s_GO = Animator.StringToHash("go");

        [SerializeField] private float m_WaitTime = 0.5f;

        private Animator m_Animator;

        private const int MAX_PHASES = 5;
        private int m_CurrentPhase = 0;

        private float m_PhaseTime = 0f;
        private float m_Timer = 0f;

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_PhaseTime = m_WaitTime / MAX_PHASES;
        }

        private void OnEnable()
        {
            m_Timer = 0f;
            m_CurrentPhase = 0;
        }
        
        private void Update()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_PhaseTime)
            {
                m_Timer = 0f;
                m_CurrentPhase++;
                if (m_CurrentPhase < MAX_PHASES)
                    m_Animator.SetTrigger(s_GO);
            }
        }
    }
}