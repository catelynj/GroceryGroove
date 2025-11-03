using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinScreen : MonoBehaviour
{
    public TMPro.TMP_Text firstLetter;
    public TMPro.TMP_Text secondLetter;
    public TMPro.TMP_Text thirdLetter;

    public Button btnTop_1;
    public Button btnTop_2;
    public Button btnTop_3;
    public Button btnBot_1;
    public Button btnBot_2;
    public Button btnBot_3;

    private bool scoreSubmitted = false;
    public Button btnSubmit;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreSubmitted = false;
        btnSubmit.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreSubmitted)
        {
            btnSubmit.interactable = false;
        }
    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SubmitScore()
    {
        string name = firstLetter.text + secondLetter.text + thirdLetter.text;
        int score = GameManager.Instance.score;
        int difficulty = GameManager.Instance.difficulty;
        float mood = GameManager.Instance.finalMood;
        scoreSubmitted = true;

        // submit score to DB
        Database.SaveData(name, score, mood, difficulty);
    }

    public void CycleFirstLetter()
    {
        btnTop_1.onClick.RemoveAllListeners();
        btnBot_1.onClick.RemoveAllListeners();

        // if top button pressed, cycle backwards through alphabet
        btnTop_1.onClick.AddListener(() =>
        {
            char currentChar = firstLetter.text[0];
            if (currentChar == 'A') // if A, wrap around to Z
            {
                firstLetter.text = "Z";
            }
            else // else increment through alphabet normally
            {
                firstLetter.text = ((char)(currentChar - 1)).ToString();
            }
        });

        // if bottom button pressed, cycle forwards through alphabet
        btnBot_1.onClick.AddListener(() =>
        {
            char currentChar = firstLetter.text[0];
            if (currentChar == 'Z') // if Z, wrap around to A
            {
                firstLetter.text = "A";
            }
            else // else increment through alphabet normally
            {
                firstLetter.text = ((char)(currentChar + 1)).ToString(); 
            }
        });
    }

    public void CycleSecondLetter()
    {
        btnTop_2.onClick.RemoveAllListeners();
        btnBot_2.onClick.RemoveAllListeners();

        // if top button pressed, cycle backwards through alphabet
        btnTop_2.onClick.AddListener(() =>
        {
            char currentChar = secondLetter.text[0];
            if (currentChar == 'A') // if A, wrap around to Z
            {
                secondLetter.text = "Z";
            }
            else // else increment through alphabet normally
            {
                secondLetter.text = ((char)(currentChar - 1)).ToString();
            }
        });

        // if bottom button pressed, cycle forwards through alphabet
        btnBot_2.onClick.AddListener(() =>
        {
            char currentChar = secondLetter.text[0];
            if (currentChar == 'Z') // if Z, wrap around to A
            {
                secondLetter.text = "A";
            }
            else // else increment through alphabet normally
            {
                secondLetter.text = ((char)(currentChar + 1)).ToString();
            }
        });
    }

    public void CycleThirdLetter()
    {
        btnTop_3.onClick.RemoveAllListeners();
        btnBot_3.onClick.RemoveAllListeners();

        // if top button pressed, cycle backwards through alphabet
        btnTop_3.onClick.AddListener(() =>
        {
            char currentChar = thirdLetter.text[0];
            if (currentChar == 'A') // if A, wrap around to Z
            {
                thirdLetter.text = "Z";
            }
            else // else increment through alphabet normally
            {
                thirdLetter.text = ((char)(currentChar - 1)).ToString();
            }
        });

        // if bottom button pressed, cycle forwards through alphabet
        btnBot_3.onClick.AddListener(() =>
        {
            char currentChar = thirdLetter.text[0];
            if (currentChar == 'Z') // if Z, wrap around to A
            {
                thirdLetter.text = "A";
            }
            else // else increment through alphabet normally
            {
               thirdLetter.text = ((char)(currentChar + 1)).ToString();
            }
        });
    }
}
