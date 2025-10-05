using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtns : MonoBehaviour
{
    private GameManager instance;

    private void Start()
    {
        instance = GameManager.Instance;
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Dialogue");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
