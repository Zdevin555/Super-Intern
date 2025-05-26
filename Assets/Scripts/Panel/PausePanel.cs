using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private Button m_Resume = null;

    [SerializeField]
    private Button m_QuitButton = null;

    private PlayerManager m_PlayerManager;

    private void Start()
    {
        m_PlayerManager = FindFirstObjectByType<PlayerManager>();
        m_Resume.onClick.AddListener(Resume);
        m_QuitButton.onClick.AddListener(QuitGame);
        m_PlayerManager.IsPause = true;
        Time.timeScale = 0f;
    }

    private void QuitGame()
    {
        Application.Quit();

    }

    private void Resume()
    {
        Time.timeScale = 1f;
        m_PlayerManager.IsPause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(gameObject);
    }
}
