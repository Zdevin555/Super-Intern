using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    private Level m_Level;

    [SerializeField]
    private TextMeshProUGUI m_Text;

    private string m_Level1Text = "You have been selected as a new generation of superpowers. You need to obtain 4 abilities in this mission";
    private string m_Level2Text = "In this mission, you need to retrieve the 3 stolen energy bars";

    private string m_CurrentText = "";

    [SerializeField]
    private Button m_Button;

    private PlayerManager m_PlayerManager;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        m_PlayerManager = FindFirstObjectByType<PlayerManager>();
        m_PlayerManager.IsShowMissionPanel = true;
        m_Button.onClick.AddListener(ConfirmButton);
        m_Button.gameObject.SetActive(false);
        m_Level = (Level)SceneManager.GetActiveScene().buildIndex;
        PlayText();

    }
    private void PlayText()
    {
        switch (m_Level)
        {
            case Level.Level1:
                StartCoroutine(ShowText(m_Level1Text));
                break;
            case Level.Level2:
                StartCoroutine(ShowText(m_Level2Text));
                break;
            default:
                break;
        }

    }
    IEnumerator ShowText(string text)
    {
        int i = 0;
        while (i < text.Length)
        {
            yield return new WaitForSeconds(0.05f);
            m_CurrentText += text[i].ToString();
            m_Text.text = m_CurrentText;
            i += 1;
        }
        StopAllCoroutines();
        m_Button.gameObject.SetActive(true);
    }

    private void ShowAllText(string text)
    {
        StopAllCoroutines();
        m_Text.text = text;
    }

    private void ConfirmButton()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_PlayerManager.IsShowMissionPanel = false;
        gameObject.SetActive(false);
    }

}
