using System.Collections;
using UnityEngine;


public class DelayedTrapSphere : MonoBehaviour
{

    private float m_DestoryTime = 15f;

    private float m_CurrentTime = 0f;

    private void Update()
    {
        m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime >= m_DestoryTime)
        {
            m_CurrentTime = 0;
            Destroy(gameObject);
        }
    }

}
