using System.Collections;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    private readonly string TransparentWallLayer = "TransparentWall";
    private bool m_IsTransparent = false;

    private float m_DurationTime = 4f;
    private float m_CurrentTime = 0f;

    private int m_DefaultLayer;

    void Start()
    {
        m_DefaultLayer = gameObject.layer;
    }

    void Update()
    {
        if (m_IsTransparent)
        {
            m_CurrentTime += Time.deltaTime;
            if (m_CurrentTime >= m_DurationTime)
            {
                m_CurrentTime = 0;
                m_IsTransparent = false;
                gameObject.layer = m_DefaultLayer;
            }
        }
    }

    public void SetTransparentWallOn()
    {
        m_IsTransparent = true;
        gameObject.layer = LayerMask.NameToLayer(TransparentWallLayer);
    }
}
