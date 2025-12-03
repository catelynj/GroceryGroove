using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * ScanItem.cs 
 * Author: Catelyn Jones
 * 
 * Purpose:
 * Handles item scanning logic
 * Keeps track of what is on and off beat, calls GoodScan/BadScan from GameManager.cs accordingly
 * Changes mood bar color/sprite based on value (not implemented yet)
 * Controls game over condition
 * 
 * Not referenced in any scripts as of 11/5
 * 
 * */
public class ScanItem : MonoBehaviour
{
    private bool _canScan;
    private float _beatInterval = 0.5f;

    private void Start()
    {
        _canScan = false;
    }

    private void Update()
    {
        // check if Time.deltatime is on a beat -- tempo is 120 BPM, one beat is every .5 seconds
        if (Mathf.Abs(GameManager.Instance.timer % _beatInterval) < 0.4) // for a little leeway of what is on beat
        {
            _canScan = true;
        }
        else
        {
            _canScan = false;
        }

        
        if (GameManager.Instance.moodSlider.value <= 0)
        {
           GameManager.Instance.levelOneMusic.enabled = false;
           SceneManager.LoadScene("GameLose");
        }

        GameManager.Instance.scoreText.text = "Score: " + GameManager.Instance.score;
        GameManager.Instance.streakText.text = "Streak: " + GameManager.Instance.streak;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canScan)
        {
            GameManager.Instance.GoodScan();
        }
        else if (!_canScan)
        {
            GameManager.Instance.BadScan();
        }
        Destroy(collision.gameObject);
    }
}
