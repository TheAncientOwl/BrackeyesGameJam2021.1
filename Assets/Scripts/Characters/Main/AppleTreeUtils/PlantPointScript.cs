using UnityEngine;

namespace Characters.Main.AppleTreeUtils
{
    public class PlantPointScript : MonoBehaviour
    {
        [SerializeField] private GameObject m_PlantPos;

        public Vector2 GetPlantPos()
        {
            return m_PlantPos.transform.position;
        }
    }

}