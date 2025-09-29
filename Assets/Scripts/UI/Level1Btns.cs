using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Btns : MonoBehaviour
{
    public GameObject PausePanel;
    public bool paused = false;
    private GameManager instance;
    public TMPro.TMP_Text hardMode;

    [SerializeField]
    private int difficulty;

    private void Start()
    {
        if (PausePanel != null)
        {
            PausePanel.SetActive(false);
        }

        instance = GameManager.Instance;

        difficulty = GameManager.Instance.GetDifficulty();

        if(difficulty == 1)
        {
            hardMode.text = "!!HARD MODE!!";
        }
        else
        {
            hardMode.text = "";
        }
    }

    public void TogglePauseMenu()
    {
        if (paused == true)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1f; //unpause game
            instance.UnpauseMusic();
            paused = false;
        }
        else
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0f; //pause game
            instance.PauseMusic();
            paused = true;
        }

        //Debug.Log("Paused: " + paused);
    }

    public void ExitLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }

}
