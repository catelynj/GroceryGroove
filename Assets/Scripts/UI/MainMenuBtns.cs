using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtns : MonoBehaviour
{
    private GameManager instance;
    public GameObject leaderboardPanel;
    

    private void Start()
    {
        instance = GameManager.Instance;
        leaderboardPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Dialogue");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowLeaderboardPanel()
    {
        leaderboardPanel.SetActive(true);
    }

    public void HideLeaderboardPanel()
    {
        leaderboardPanel.SetActive(false);
    }
}
