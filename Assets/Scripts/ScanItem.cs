using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScanItem : MonoBehaviour
{
    private bool canScan;
    private float beatInterval = 0.5f;

    private void Start()
    {
        canScan = false;
    }

    private void Update()
    {
        // check if Time.deltatime is on a beat -- tempo is 120 BPM, one beat is every .5 seconds
        if (Mathf.Abs(GameManager.Instance.timer % beatInterval) < 0.1) // for a little leeway of what is on beat
        {
            canScan = true;
        }
        else
        {
            canScan = false;
        }
    }


    //TODO: make sure that only notes that are touched count for score, not just every note because sometimes a note is incidentally on beat 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canScan)
        {
            GameManager.Instance.score += 100;
            GameManager.Instance.streak += 1;
        }
        else if (!canScan)
        {
            GameManager.Instance.streak = 0;
            GameManager.Instance.moodSlider.value -= 5;
            GameManager.Instance.mood -= 5;
            GameManager.Instance.score -= 25;

            if (GameManager.Instance.moodSlider.value > 10 && GameManager.Instance.moodSlider.value < 35)
            {
                // change color and sprite of handle to yellow
            }
            else if(GameManager.Instance.moodSlider.value <= 10 && GameManager.Instance.moodSlider.value > 0)
            {
                // change color and sprite of handle to red
            }
            else if(GameManager.Instance.moodSlider.value <= 0)
            {
                //GAME OVER
                SceneManager.LoadScene("GameLose");
            }
        }

        GameManager.Instance.scoreText.text = "Score: " + GameManager.Instance.score;
        GameManager.Instance.streakText.text = "Streak: " + GameManager.Instance.streak;
        Destroy(collision.gameObject);
    }
}
