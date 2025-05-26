using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Level1Text = null;

    [SerializeField]
    private GameObject m_Level2Text = null;

    [SerializeField]
    private Button m_RestButtom = null;

    [SerializeField]
    private TextMeshProUGUI m_ButtonText = null;

    [SerializeField]
    private Button m_QuitButton = null;

    private string m_Level1Pass = "Next Level";
    private string m_Level2Pass = "Play Again";

    private LevelManager m_LevelManager;
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        m_LevelManager = LevelManager.Instance;
        if (m_LevelManager == null)
        {
            Debug.LogError("LevelManager is null");
        }
    }

    public void NextLevel()
    {
        m_RestButtom.onClick.AddListener(LoadNextLevel);
        m_QuitButton.onClick.AddListener(QuitGame);
        m_Level2Text.gameObject.SetActive(false);
        m_Level1Text.gameObject.SetActive(true);
        m_ButtonText.text = m_Level1Pass;
        Time.timeScale = 0f;
    }

    public void PassLevel()
    {
        m_RestButtom.onClick.AddListener(RestGame);
        m_QuitButton.onClick.AddListener(QuitGame);
        m_Level1Text.gameObject.SetActive(false);
        m_Level2Text.gameObject.SetActive(true);
        m_ButtonText.text = m_Level2Pass;
        Time.timeScale = 0f;
    }

    private void LoadNextLevel()
    {
        m_LevelManager.NextLevel();
    }

    private void RestGame()
    {
        m_LevelManager.ReLoadLevel(true);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
