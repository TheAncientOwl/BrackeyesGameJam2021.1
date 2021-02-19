using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    private bool m_Alive = true;
    public void Break()
    {
        Destroy(this.gameObject, 5f);
        Debug.Log("IGHT IMMA HEAD OUT SOON LOL. xD");
        m_Alive = false;
    }

    public bool IsAlive() => m_Alive;
}
