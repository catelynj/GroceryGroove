using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanItem : MonoBehaviour
{

    //hey so literally all of this commented code is irrelvant now that i changed how the game is going to play...yay

    /**
    [SerializeField]
    private string itemType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bag"))
        {
            switch (itemType)
            {
                case "Food":
                    if( other.gameObject.name.Contains("Food"))
                    {
                        Debug.Log("Correct Bag! Score +50");
                        GameManager.Instance.score += 50;
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Wrong Bag! Score -20");
                        GameManager.Instance.score -= 20;
                        Destroy(gameObject);
                    }
                    break;
                case "Fragile":
                    if(other.gameObject.name.Contains("Fragile"))
                    {
                        Debug.Log("Correct Bag! Score +50");
                        GameManager.Instance.score += 50;
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Wrong Bag! Score -20");
                        GameManager.Instance.score -= 20;
                        Destroy(gameObject);
                    }
                    break;
                case "Cleaning":
                    if(other.gameObject.name.Contains("Cleaning"))
                    {
                        Debug.Log("Correct Bag! Score +50");
                        GameManager.Instance.score += 50;
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Wrong Bag! Score -20");
                        GameManager.Instance.score -= 20;
                        Destroy(gameObject);
                    }
                    break;
                case "Junk":
                    if(other.gameObject.name.Contains("Junk"))
                    {
                        Debug.Log("Correct Bag! Score +50");
                        GameManager.Instance.score += 50;
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Wrong Bag! Score -20");
                        GameManager.Instance.score -= 20;
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
    **/

    private bool canScan;
    private float beatInterval = 0.5f;
    private float timer = 0f;

    private void Start()
    {
        canScan = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // check if Time.deltatime is on a beat -- tempo is 120 BPM, one beat is every .5 seconds
        if (Mathf.Abs(timer % beatInterval) < 0.05) // 0.05 seconds to allow for a little leeway of what is on beat
        {
            canScan = true;
        }
        else
        {
            canScan = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canScan)
        {
            Debug.Log("scanned on beat +10");
        }
        else if (!canScan)
        {
            Debug.Log("scanned off beat -5");
        }

        Destroy(collision.gameObject);
    }
}
