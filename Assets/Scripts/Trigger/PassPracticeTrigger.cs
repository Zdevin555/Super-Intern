using System.Collections;
using UnityEngine;

public class PassPracticeTrigger : MonoBehaviour
{
    [SerializeField]
    private bool m_IsLevelOver = false;
    private ResManager m_ResManager;
    private void Start()
    {
        m_ResManager = ResManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_ResManager != null)
            {
                GameObject obj = Instantiate(m_ResManager.PopPanel);
                PopPanel popPanel = obj.GetComponent<PopPanel>();
                if (m_IsLevelOver)
                {
                    popPanel.PassLevel();
                }
                else
                {
                    popPanel.NextLevel();
                }



            }
        }
    }
}
