using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class WarningItem : MonoBehaviour
{
    private Transform m_Owner;
    private Canvas m_Canvas;
    private float m_CurrentHideTime = 0;
    private float m_DefaultHideTime = 4f;


    public void Init(Transform Owner, Canvas canvas)
    {
        m_Owner = Owner;
        m_Canvas = canvas;
        m_CurrentHideTime = 0;
    }

    public void UpdatePoint()
    {
        m_CurrentHideTime += Time.deltaTime;
        Vector3 worldPosition = m_Owner.transform.position + Vector3.forward;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)m_Canvas.transform, screenPosition,
            m_Canvas.worldCamera, out position))
        {
            transform.localPosition = position;
        }
    }

    public bool IsHide()
    {
        return m_CurrentHideTime >= m_DefaultHideTime;
    }

}
