using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{

    [SerializeField]
    private Button m_StartGame = null;

    [SerializeField]
    private Button m_QuitButton = null;

    private void Start()
    {
        m_StartGame.onClick.AddListener(StartGame);
        m_QuitButton.onClick.AddListener(QuitGame);

    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
