using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level
{
    Level0,
    Level1,
    Level2,
}

public class LevelManager : MonoBehaviour
{
    private int m_DefaultLevel = 0;
    private int m_CurrentLevel = 0;

    private PlayerData m_PlayerData;
    private ResManager m_ResManager;

    public bool IsReStart = false;

    public static LevelManager Instance
    {
        private set; get;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        m_CurrentLevel = SceneManager.GetActiveScene().buildIndex;

        m_PlayerData = FindObjectOfType<PlayerData>();
        m_ResManager = ResManager.Instance;
        if (m_PlayerData == null)
        {
            Debug.LogError("PlayerData is null");
        }
        if (m_ResManager == null)
        {
            Debug.LogError("ResManager is null");
        }
    }

    public void NextLevel()
    {
        int count = SceneManager.sceneCountInBuildSettings;
        m_CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        if (m_CurrentLevel >= count - 1)
        {
            Debug.Log("No Next Level");
            return;
        }
        Time.timeScale = 1f;
        IsReStart = false;
        SceneManager.LoadScene(++m_CurrentLevel);
    }

    public void ReLoadLevel(bool isGameOver)
    {
        if (isGameOver)
        {
            SceneManager.LoadScene(m_DefaultLevel);
            IsReStart = false;
            Destroy(m_PlayerData.gameObject);
            Destroy(m_ResManager.gameObject);
            Destroy(gameObject);
        }
        else
        {
            m_CurrentLevel = SceneManager.GetActiveScene().buildIndex;
            IsReStart = true;
            SceneManager.LoadScene(m_CurrentLevel);
        }
        Time.timeScale = 1f;
    }

    public Level GetCurrentLevel()
    {
        return (Level)SceneManager.GetActiveScene().buildIndex;
    }


}
