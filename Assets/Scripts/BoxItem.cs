using System.Collections;
using UnityEngine;


public class BoxItem : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private ResManager m_ResManager;

    private UIPanel UI;

    private bool m_IsKeyDown = false;


    private float m_CurrentKeyDownTime = 0;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_ResManager = ResManager.Instance;
        UI = FindObjectOfType<UIPanel>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (m_AudioSource != null)
            {
                m_AudioSource.clip = m_ResManager.SoundScriptableObject.BoxItem;
                m_AudioSource.Play();
            }
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
            if (m_IsKeyDown)
            {
                m_IsKeyDown = false;
                UI.SetBadgeTextActive(false);
                other.gameObject.GetComponent<PlayerManager>().BoxItem += 1;
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
