using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.Main.Cloud
{
    public class CloudManager : AirCharacterManager
    {
        private static readonly int s_IDLE            = Animator.StringToHash("Idle");
        private static readonly int s_VERTICAL_MOVE   = Animator.StringToHash("VerticalMove");
        private static readonly int s_HORIZONTAL_MOVE = Animator.StringToHash("HorizontalMove");
        private int m_LastHash = 0;

        [SerializeField] private GameObject m_RainDrop;
        [SerializeField] private Transform m_RainSpawnPoint;
        [SerializeField] private Vector2 m_SpawnRandomness;
        [SerializeField] private int m_RainDrops;

        private void Start()
        {
            m_LastHash = m_Rigidbody2D.velocity.normalized.y != 0f ? s_VERTICAL_MOVE : (GetHorizontalDirection() == 0f ? s_IDLE : s_HORIZONTAL_MOVE);
            m_Animator.SetBool(m_LastHash, true);
        }

        private void Update()
        {
            TryMechanic();
            SetAnimation();
        }

        private void TryMechanic()
        {
            if (m_IsMain && Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < m_RainDrops; ++i) 
                {
                    Instantiate
                    (
                        original: m_RainDrop,
                        position: m_RainSpawnPoint.position + new Vector3
                                                           (x: Random.Range(-m_SpawnRandomness.x, m_SpawnRandomness.x),
                                                            y: Random.Range(-m_SpawnRandomness.y, m_SpawnRandomness.y)),
                        rotation: Quaternion.identity
                    );
                }
            }
        }

        private void SetAnimation()
        {
            int newHash = m_Rigidbody2D.velocity.normalized.y != 0f ? s_VERTICAL_MOVE : (GetHorizontalDirection() == 0f ? s_IDLE : s_HORIZONTAL_MOVE);

            if (m_LastHash != newHash)
            {
                m_Animator.SetBool(m_LastHash, false);
                m_Animator.SetBool(newHash, true);
                m_LastHash = newHash;
            }
        }

        public override void DisableSpecialMechanics()
        {
        }

        public override void EnableSpecialMechanics()
        {
        }
    }
}