using System.Collections;
using UnityEngine;

public enum InteractiveType
{
    Lift,
    CanControlDoor,
    TimeClose,
    PartiallyUnlocked,
}

public class InteractiveControl : MonoBehaviour
{
    public InteractiveType m_InteractiveType;

    private UIPanel UI;

    private readonly int IsOpen = Animator.StringToHash("IsOpen");

    [SerializeField]
    private GameObject m_Interactive;

    private ResManager m_ResManager;

    private Animator m_Animator;

    private DoorControl m_DoorControl;

    private bool m_IsOpen = false;

    private float m_DefaultCloseTime = 7f;
    private float m_CurrentTime = 0f;

    private AudioSource m_AudioSource;

    private AudioSource m_LiftAudioSource;

    private bool m_IsKeyDown = false;

    private int m_CanUnLockCount = 1;

    private float m_CurrentKeyDownTime = 0;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        switch (m_InteractiveType)
        {
            case InteractiveType.Lift:
                m_Animator = m_Interactive.GetComponent<Animator>();
                m_LiftAudioSource = m_Interactive.GetComponent<AudioSource>();
                break;
            case InteractiveType.CanControlDoor:
                m_DoorControl = m_Interactive.GetComponent<DoorControl>();
                break;
            case InteractiveType.TimeClose:
                m_Animator = m_Interactive.GetComponent<Animator>();
                break;
            case InteractiveType.PartiallyUnlocked:
                m_DoorControl = m_Interactive.GetComponent<DoorControl>();
                break;
            default:
                break;
        }
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
        if (m_IsOpen)
        {
            m_CurrentTime += Time.deltaTime;
            if (m_CurrentTime >= m_DefaultCloseTime)
            {
                m_CurrentTime = 0;
                switch (m_InteractiveType)
                {
                    case InteractiveType.TimeClose:
                        m_IsOpen = false;
                        m_Animator.SetBool(IsOpen, m_IsOpen);
                        break;
                    default:
                        break;
                }
            }

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
                switch (m_InteractiveType)
                {
                    case InteractiveType.Lift:
                        m_IsOpen = !m_IsOpen;
                        m_Animator.SetBool(IsOpen, m_IsOpen);
                        if (m_LiftAudioSource != null)
                        {
                            m_LiftAudioSource.clip = m_ResManager.SoundScriptableObject.LiftSound;
                            if (m_IsOpen)
                            {
                                m_LiftAudioSource.Play();
                            }
                            else
                            {
                                m_LiftAudioSource.Pause();
                            }
                        }
                        break;
                    case InteractiveType.CanControlDoor:
                        m_DoorControl.CanControl = true;
                        break;
                    case InteractiveType.TimeClose:
                        m_IsOpen = true;
                        m_Animator.SetBool(IsOpen, m_IsOpen);
                        break;
                    case InteractiveType.PartiallyUnlocked:
                        if (m_CanUnLockCount > 0)
                        {
                            m_DoorControl.CurrentUnLockCount += m_CanUnLockCount;
                            m_CanUnLockCount--;
                        }

                        break;
                    default:
                        break;
                }
                if (m_AudioSource != null)
                {
                    m_AudioSource.clip = m_ResManager.SoundScriptableObject.ComputerSound;
                    m_AudioSource.Play();
                }
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
