using System.Collections;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    private LevelManager m_LevelManager;
    private ResManager m_ResManager;

    private void Start()
    {
        m_LevelManager = LevelManager.Instance;
        if (m_LevelManager == null)
        {
            Debug.LogError("LevelManager is null");
        }
        m_ResManager = ResManager.Instance;
        if (m_ResManager == null)
        {
            Debug.LogError("ResManager is null");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_ResManager != null)
            {
                Instantiate(m_ResManager.PopPanel);
            }
        }
    }


}
