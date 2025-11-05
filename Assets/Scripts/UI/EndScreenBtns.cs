using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * EndScreenBtns.cs 
 * Author: Catelyn Jones
 * 
 * Purpose:
 * UI Script for Game Lose Screen
 * 
 * */


public class EndScreenBtns : MonoBehaviour
{
    public TMPro.TMP_Text finalScoreText;

    private void Start()
    {
        finalScoreText.text = "Score: " + GameManager.Instance.score.ToString();
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
