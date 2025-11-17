using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Btns : MonoBehaviour
{
    public GameObject PausePanel;
    public bool paused = false;
    private GameManager _instance;
    public TMPro.TMP_Text hardMode;

    [SerializeField]
    private int _difficulty;

    private void Start()
    {
        if (PausePanel != null)
        {
            PausePanel.SetActive(false);
        }

        _instance = GameManager.Instance;

        _difficulty = GameManager.Instance.GetDifficulty();

        if(_difficulty == 1)
        {
            hardMode.text = "!!HARD MODE!!";
        }
        else
        {
            hardMode.text = "";
        }
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f; //pause game
        _instance.PauseMusic();
        paused = true;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f; //unpause game
        _instance.UnpauseMusic();
        paused = false;
    }
    public void ExitLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }

}
