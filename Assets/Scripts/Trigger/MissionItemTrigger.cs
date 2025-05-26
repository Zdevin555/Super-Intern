using System.Collections;
using UnityEngine;

public class MissionItemTrigger : MonoBehaviour
{
    private LevelManager m_LevelManager;
    private ResManager m_ResManager;
    public UIPanel UI;

    private bool m_IsKeyDown;

    public AudioSource m_AudioSource;

    private float m_CurrentKeyDownTime = 0;

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
                if (m_AudioSource != null)
                {
                    m_AudioSource.clip = m_ResManager.SoundScriptableObject.BadgeSound;
                    m_AudioSource.Play();
                }
                UI.SetBadgeTextActive(false);
                other.GetComponent<PlayerManager>().MissionItemCount += 1;
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
