using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public bool isPaused = false;


    private static UIController _instance;
    public static UIController Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("UIController");
                _instance = go.AddComponent<UIController>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMainMenu()
    {
        ResetGameState();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void TogglePause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.levelOneMusic?.Pause();
            if (GameManager.Instance.itemMov != null)
            {
                GameManager.Instance.itemMov.enabled = false;
            }
        }
    }

    private void ResumeGame()
    {
        isPaused = false;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.levelOneMusic?.UnPause();
            if (GameManager.Instance.itemMov != null)
            {
                GameManager.Instance.itemMov.enabled = true;
            }
        }
    }
    
    private void ResetGameState()
    {
        isPaused = false;
        if (GameManager.Instance != null)
        {
            // Reset any game state as needed
            GameManager.Instance.score = 0;
        }
    }
}
