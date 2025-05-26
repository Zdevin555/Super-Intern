using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{

    private readonly int IsOpen = Animator.StringToHash("IsOpen");

    private ResManager m_ResManager;

    private Animator m_Animator;

    private AudioSource m_AudioSource;

    [SerializeField]
    private UIPanel UI;

    [SerializeField]
    private bool IsLDoor = false;

    [SerializeField]
    private int LockCount = 0;

    [SerializeField]
    private bool IsMission = false;

    private int m_CurrentUnLockCount = 0;

    public bool CanControl = true;

    public bool TriggerClose = false;

    private PlayerData m_PlayerData;

    public int CurrentUnLockCount
    {
        get
        {
            return m_CurrentUnLockCount;
        }
        set
        {

            m_CurrentUnLockCount = value;
        }
    }
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_ResManager = ResManager.Instance;
        m_AudioSource = gameObject.AddComponent<AudioSource>();
        m_PlayerData = FindObjectOfType<PlayerData>();
    }

    public void SetDoorOpen()
    {
        m_Animator.SetBool(IsOpen, true);
    }

    public void SetDoorClose()
    {
        m_Animator.SetBool(IsOpen, false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!CanControl)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            if (TriggerClose)
            {
                m_Animator.SetBool(IsOpen, false);
                if (IsLDoor)
                {
                    m_AudioSource.clip = m_ResManager.SoundScriptableObject.L_DoorClose;
                }
                else
                {
                    m_AudioSource.clip = m_ResManager.SoundScriptableObject.S_DoorClose;

                }
            }
            else
            {
                if (IsMission)
                {
                    int itemCount = other.GetComponent<PlayerManager>().MissionItemCount;
                    if (itemCount < m_PlayerData.MissionItem)
                    {
                        if (UI != null)
                        {
                            UI.SetMissionText(true, m_PlayerData.MissionItem - itemCount);
                        }
                        return;
                    }
                }
                if (m_CurrentUnLockCount < LockCount)
                {
                    if (UI != null)
                    {
                        UI.SetUnLockText(true, LockCount - m_CurrentUnLockCount);
                    }

                    return;
                }
                m_Animator.SetBool(IsOpen, true);
                if (IsLDoor)
                {
                    m_AudioSource.clip = m_ResManager.SoundScriptableObject.L_DoorOpen;
                }
                else
                {
                    m_AudioSource.clip = m_ResManager.SoundScriptableObject.S_DoorOpen;

                }
            }

            m_AudioSource.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!CanControl)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            if (TriggerClose)
            {
                m_Animator.SetBool(IsOpen, false);
            }
            else
            {
                if (IsMission)
                {
                    if (other.GetComponent<PlayerManager>().MissionItemCount < m_PlayerData.MissionItem)
                    {
                        return;
                    }
                }
                if (m_CurrentUnLockCount < LockCount)
                {
                    return;
                }

                m_Animator.SetBool(IsOpen, true);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CanControl)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            if (TriggerClose)
            {
                return;
            }
            if (IsMission)
            {
                if (other.GetComponent<PlayerManager>().MissionItemCount < m_PlayerData.MissionItem)
                {
                    if (UI != null)
                    {
                        UI.SetMissionText(false);
                    }
                    return;
                }
            }
            if (m_CurrentUnLockCount < LockCount)
            {
                if (UI != null)
                {
                    UI.SetUnLockText(false);
                }

                return;
            }
            m_Animator.SetBool(IsOpen, false);
            if (IsLDoor)
            {
                m_AudioSource.clip = m_ResManager.SoundScriptableObject.L_DoorClose;
            }
            else
            {
                m_AudioSource.clip = m_ResManager.SoundScriptableObject.S_DoorClose;

            }
            m_AudioSource.Play();
        }
    }
}
