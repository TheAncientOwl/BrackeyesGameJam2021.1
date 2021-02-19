using System.Collections.Generic;
using UnityEngine;
using Characters.Movement;
using Characters.General;

namespace Characters.Main.Bird
{
    public class BirdPlaneMechanic : MonoBehaviour
    {
        private const float MINIMIZE_FACTOR = 4f;

        [SerializeField] private GeneralManager m_GeneralManager;
        [SerializeField] private float m_PlaneModeCheckRadius = 0f;
        [SerializeField] private LayerMask m_CharacterLayerMask = 0;
        [SerializeField] private GameObject m_GeneralParent;
        [SerializeField] private Transform m_PlaneSeatPoint;

        private readonly LinkedList<GameObject> m_Characters = new LinkedList<GameObject>();
        private bool m_PlaneMode = false;

        private SpriteFlipper m_SpriteFlipper;
        private BoxCollider2D m_BoxCollider2D;

        private void Start()
        {
            m_BoxCollider2D = GetComponent<BoxCollider2D>();
            m_SpriteFlipper = GetComponent<SpriteFlipper>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_PlaneMode = !m_PlaneMode;
                if (m_PlaneMode)
                    CollectCharacters();
                else
                    ReleaseCharacters();
            }
        }

        private void CollectCharacters()
        {
            m_GeneralManager.SetCharacterSwitch(false);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_BoxCollider2D.bounds.center, m_PlaneModeCheckRadius, m_CharacterLayerMask);

            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    GameObject obj = collider.gameObject;
                    if (obj != this.gameObject)
                    {
                        m_Characters.AddLast(obj);
                        obj.transform.localScale /= MINIMIZE_FACTOR;
                        obj.transform.position = m_PlaneSeatPoint.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.1f, 0.1f));
                        obj.transform.SetParent(this.transform);
                        obj.GetComponent<Rigidbody2D>().isKinematic = true;
                    }
                }
            }
        }

        private void ReleaseCharacters()
        {
            m_GeneralManager.SetCharacterSwitch(true);
            foreach (var character in m_Characters)
            {
                character.transform.localScale *= MINIMIZE_FACTOR;
                character.transform.SetParent(m_GeneralParent.transform);
                character.GetComponent<Rigidbody2D>().isKinematic = false;
                if (m_SpriteFlipper.FacingRight())
                    character.GetComponent<SpriteFlipper>().ForceFacingRight();
                else
                    character.GetComponent<SpriteFlipper>().ForceFacingLeft();
            }
            m_Characters.Clear();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(GetComponent<BoxCollider2D>().bounds.center, m_PlaneModeCheckRadius);
        }
    }
}