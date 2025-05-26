using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_LoseGameText = null;

    [SerializeField]
    private TextMeshProUGUI m_RePlayeGameText = null;

    [SerializeField]
    private Button m_RePlayer = null;

    [SerializeField]
    private Button m_QuitButton = null;

    private LevelManager m_LevelManager;
    private PlayerData m_PlayerData;

    private bool m_IsGameOver = false;
    private void Start()
    {
        m_RePlayer.onClick.AddListener(RePlayer);
        m_QuitButton.onClick.AddListener(QuitGame);

        m_LevelManager = LevelManager.Instance;
        m_PlayerData = FindObjectOfType<PlayerData>();
        if (m_LevelManager == null)
        {
            Debug.LogError("LevelManager is null");
        }
        if (m_PlayerData == null)
        {
            Debug.LogError("PlayerData is null");
        }
        Time.timeScale = 0f;

        if (m_PlayerData.Life > 0)
        {
            m_LoseGameText.text = "You're Dead";
            m_RePlayeGameText.text = "Replay";
            m_IsGameOver = false;
        }
        else
        {
            m_LoseGameText.text = "Game Over";
            m_RePlayeGameText.text = "Continue";
            m_IsGameOver = true;
        }
    }

    private void RePlayer()
    {
        if (m_IsGameOver)
        {
            m_PlayerData.Life = m_PlayerData.MaxLife;
        }
        m_LevelManager.ReLoadLevel(m_IsGameOver);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
