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


    [SerializeField]
    private Sprite[] _moodBarSprite;

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

        // reset mood bar 
        GameManager.Instance.moodSlider.fillRect.GetComponent<UnityEngine.UI.Image>().color = new Color(22f, 195f, 0f, 255f);
        GameManager.Instance.moodSlider.handleRect.GetComponent<UnityEngine.UI.Image>().sprite = _moodBarSprite[0];
    }

    private void Update()
    {
        if (GameManager.Instance.moodSlider.value > 10 && GameManager.Instance.moodSlider.value < 35)
        {
            // change color and sprite of handle to yellow
            GameManager.Instance.moodSlider.fillRect.GetComponent<UnityEngine.UI.Image>().color = new Color(255f,136f,0f,255f);
            GameManager.Instance.moodSlider.handleRect.GetComponent<UnityEngine.UI.Image>().sprite = _moodBarSprite[1];
        }
        else if (GameManager.Instance.moodSlider.value <= 10 && GameManager.Instance.moodSlider.value > 0)
        {
            // change color and sprite of handle to red
            GameManager.Instance.moodSlider.fillRect.GetComponent<UnityEngine.UI.Image>().color = new Color(255f, 12f, 0, 255f);
            GameManager.Instance.moodSlider.handleRect.GetComponent<UnityEngine.UI.Image>().sprite = _moodBarSprite[2];
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
