using System.Collections;
using UnityEngine;

public enum BadgeType
{
    Elastic,
    SuperRun,
    Throughwall,
    ControlTime,

}

public class BadgeTrigger : MonoBehaviour
{
    public UIPanel UI;

    public BadgeType badgeType;

    private PlayerData m_PlayerData;

    public DoorControl m_DoorControl;

    private ResManager m_ResManager;

    public AudioSource m_AudioSource;

    private bool m_IsKeyDown;

    private float m_CurrentKeyDownTime = 0;
    private void Start()
    {
        m_PlayerData = FindObjectOfType<PlayerData>();
        m_ResManager = ResManager.Instance;
        if (m_PlayerData == null)
        {
            Debug.LogError("PlayerData is null");
        }
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
            UI.SetBadgeTextActive(true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetBadgeTextActive(true);
            if (m_IsKeyDown)
            {
                m_IsKeyDown = false;
                PlayerManager playerManager = other.GetComponent<PlayerManager>();
                if (playerManager == null)
                {
                    Debug.LogError("PlayerManager is null");
                    return;
                }
                UI.ShowGetSkill(badgeType);
                switch (badgeType)
                {
                    case BadgeType.Elastic:
                        playerManager.Elastic = true;
                        m_PlayerData.Elastic = true;
                        break;
                    case BadgeType.SuperRun:
                        m_PlayerData.SuperRun = true;
                        playerManager.SuperRun = true;
                        m_DoorControl.SetDoorOpen();
                        break;
                    case BadgeType.Throughwall:
                        m_PlayerData.Throughwall = true;
                        playerManager.Throughwall = true;
                        m_DoorControl.SetDoorOpen();
                        break;
                    case BadgeType.ControlTime:
                        m_PlayerData.ControlTime = true;
                        playerManager.ControlTime = true;
                        m_DoorControl.SetDoorOpen();
                        break;
                    default:
                        break;
                }
                if (m_AudioSource != null)
                {
                    m_AudioSource.clip = m_ResManager.SoundScriptableObject.BadgeSound;
                    m_AudioSource.Play();
                }
                UI.SetBadgeTextActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetBadgeTextActive(false);
        }
    }
}
