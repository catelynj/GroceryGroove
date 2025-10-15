using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenBtns : MonoBehaviour
{
    private void Start()
    {
        
    }
    public void Replay()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
