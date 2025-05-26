using System.Collections;
using UnityEngine;


   public class CollectableItem : MonoBehaviour
{

    public UIPanel UI;

    private readonly int IsOpen = Animator.StringToHash("IsOpen");

    [SerializeField]
    private GameObject m_Lift;

    private ResManager m_ResManager;

    private Animator m_Animator;

    private bool m_IsOpen = false;

    private bool m_IsKeyDown = false;

    private float m_CurrentKeyDownTime = 0;
    void Start()
    {
        m_Animator = m_Lift.GetComponent<Animator>();
        m_ResManager = ResManager.Instance;
        UI = FindObjectOfType<UIPanel>();
    }


    private void Update()
    {
        m_CurrentKeyDownTime += Time.deltaTime;
        if (InputManager.Take())
        {
            m_IsKeyDown = true;
            m_CurrentKeyDownTime = 0;
        }
        if (m_CurrentKeyDownTime > 0.5f)
        {
            m_IsKeyDown = false;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetUseLiftPanel(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetUseLiftPanel(true);
            if (m_IsKeyDown)
            {
                m_IsKeyDown = false;
                m_IsOpen = !m_IsOpen;
                m_Animator.SetBool(IsOpen, m_IsOpen);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetUseLiftPanel(false);
        }
    }
}
