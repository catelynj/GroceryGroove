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

        ChangeMoodBarColor("default");
    }

    private void Update()
    {
        if (GameManager.Instance.moodSlider.value > 10 && GameManager.Instance.moodSlider.value < 50)
        {
            ChangeMoodBarColor("orange");
        }
        else if (GameManager.Instance.moodSlider.value <= 10 && GameManager.Instance.moodSlider.value >= 0)
        {
            ChangeMoodBarColor("red");
        }
    }

    public void ChangeMoodBarColor(string color)
    {
        switch (color)
        {
            case "orange":
                GameManager.Instance.moodSlider.fillRect.GetComponent<UnityEngine.UI.Image>().color = new Color(255f, 136f, 0f, 255f);
                GameManager.Instance.moodSlider.handleRect.GetComponent<UnityEngine.UI.Image>().sprite = _moodBarSprite[1];
                break;
            case "red":
                GameManager.Instance.moodSlider.fillRect.GetComponent<UnityEngine.UI.Image>().color = new Color(255f, 0f, 0, 255f);
                GameManager.Instance.moodSlider.handleRect.GetComponent<UnityEngine.UI.Image>().sprite = _moodBarSprite[2];
                break;
            default:
                GameManager.Instance.moodSlider.fillRect.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 255f, 0f, 255f);
                GameManager.Instance.moodSlider.handleRect.GetComponent<UnityEngine.UI.Image>().sprite = _moodBarSprite[0];
                break;
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
