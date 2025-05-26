using System.Collections;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_TrapSphere;

    private float m_GenerationTime = 5f;

    private float m_CurrentGenerationTime = 0f;


    private void Update()
    {
        m_CurrentGenerationTime += Time.deltaTime;
        if (m_CurrentGenerationTime >= m_GenerationTime)
        {
            m_CurrentGenerationTime = 0;
            var obj = Instantiate(m_TrapSphere, transform);
            obj.AddComponent<DelayedTrapSphere>();

        }
    }

}
