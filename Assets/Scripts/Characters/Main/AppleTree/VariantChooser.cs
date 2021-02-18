using UnityEngine;
using Characters.CharacterTypes;

namespace Characters.Main.AppleTree
{
    public class VariantChooser : MonoBehaviour
    {
        [SerializeField] private GameObject m_OnCartVariant;
        [SerializeField] private GameObject m_OnGroundVariant;
        [SerializeField] private CharacterManager m_Character;

        [SerializeField] private float m_CheckOffset = 1f;
        [SerializeField] private float m_CheckDepth = 1f;
        [SerializeField] private LayerMask m_PlantLayerMask = 0;

        private bool m_OnCart = true;
        private Vector2 CHECK_SIZE = Vector2.zero;
        private BoxCollider2D m_BoxCollider2D;

        private void Start()
        {
            m_OnGroundVariant.SetActive(false);
            m_BoxCollider2D = m_OnCartVariant.GetComponent<BoxCollider2D>();
            this.CHECK_SIZE = new Vector2(m_BoxCollider2D.bounds.size.x, m_CheckDepth);
        }

        private void Update()
        {
            if (m_Character.IsMain())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (m_OnCart)
                    {
                        Collider2D collider = Physics2D.BoxCast
                        (
                            origin: (Vector2)m_BoxCollider2D.bounds.center - new Vector2(0f, m_CheckOffset),
                            size: this.CHECK_SIZE,
                            layerMask: m_PlantLayerMask,
                            direction: Vector2.down,
                            distance: 0f,
                            angle: 0f
                        ).collider;

                        if (collider != null)
                        {
                            m_OnCartVariant.SetActive(false);
                            m_OnGroundVariant.GetComponent<Transform>().position = collider.GetComponent<PlantPointScript>().GetPlantPos();
                            m_OnGroundVariant.SetActive(true);
                            m_OnCart = !m_OnCart;
                        }

                    }
                    else
                    {
                        m_OnGroundVariant.SetActive(false);
                        m_OnCartVariant.GetComponent<Transform>().position = m_OnGroundVariant.GetComponent<Transform>().position;
                        m_OnCartVariant.SetActive(true);
                        m_OnCart = !m_OnCart;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            BoxCollider2D boxCollider2D = m_OnCartVariant.GetComponent<BoxCollider2D>();
            Gizmos.DrawWireCube
            (
               center: boxCollider2D.bounds.center - new Vector3(0f, m_CheckOffset),
               size: new Vector3(boxCollider2D.bounds.size.x, m_CheckDepth)
            );
        }
    }
}