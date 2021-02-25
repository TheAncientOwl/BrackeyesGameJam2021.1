using UnityEngine;
using Characters.CharacterTypes.Air;

namespace Characters.Main.Cloud
{
    public class Cloud : AirCharacter
    {
        [SerializeField] private GameObject m_RainDrop;
        [SerializeField] private Transform m_RainSpawnPoint;
        [SerializeField] private Vector2 m_SpawnRandomness;
        [SerializeField] private int m_RainDrops;

        private void Update()
        {
            TryMechanic();
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

        public override void DisableSpecialMechanics()
        {
        }

        public override void EnableSpecialMechanics()
        {
        }
    }
}