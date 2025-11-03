using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using SQLite;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance = null;
    public ItemMovement itemMov;

    private bool started = false;
    public AudioSource levelOneMusic;

    public int score;
    public TMPro.TMP_Text scoreText;
    public int streak;
    public TMPro.TMP_Text streakText;

    public int mood;
    public Slider moodSlider;
    public float finalMood;

    public int difficulty = 0; // 0 = default 1 = hard

    public float timer;
    private float elapsedTime;

    public static GameManager Instance
    {
        get
        {
            return _Instance;
        }
    }

    private void Awake()
    {
        if (_Instance == null)
            _Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Database.InitializeDatabase(); // call InitializeDatabase from Database.cs
    }

    private void Start()
    {
        score = 0;
        difficulty = 0;
        streak = 0;
        mood = 100;
        timer = 0f;

        //get swipe script
        Swipe swipe = FindAnyObjectByType<Swipe>();
    }

    private void Update()
    {
        if (!started && itemMov != null && levelOneMusic != null)
        {
            if (Input.touchCount > 0) // wait for first touch to start music and movement
            {
                started = true;
                levelOneMusic.Play();
                itemMov.started = true;
            }
        }

        if (started)
        {
            timer += Time.deltaTime;
            elapsedTime += levelOneMusic.isPlaying ? Time.deltaTime : 0f;

        }
        CheckWin();
    }

    public void PauseMusic()
    {
        if(levelOneMusic != null)
        levelOneMusic.Pause();
    }
    public void UnpauseMusic()
    {
        if (levelOneMusic != null)
            levelOneMusic.UnPause();
    }

    public void SetDifficulty(int sliderValue)
    {
        difficulty = sliderValue;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }


    public void GoodScan()
    {
        streak += 1;
        if(streak > 0)
        {
            score += 100 + (streak * 50); // add bonus score for streak
        }
        else
        {
            score += 100; // base score
        }
    }

    public void BadScan()
    {
        streak = 0;
        moodSlider.value -= 5;
        mood -= 5;
    }


    // hard mode -> decrease mood twice as much, increase item speed or something similar


    // persistence fixes
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            itemMov = FindAnyObjectByType<ItemMovement>();
            levelOneMusic = GameObject.Find("LevelMusic")?.GetComponent<AudioSource>();
            scoreText = GameObject.Find("txtScore")?.GetComponent<TMPro.TMP_Text>();
            streakText = GameObject.Find("txtStreak")?.GetComponent<TMPro.TMP_Text>();
            moodSlider = GameObject.Find("MoodBar")?.GetComponent<Slider>();
            started = false;
            score = 0;
        }
    }

    // Game Win Condition
    private void CheckWin()
    {
        //if song has finished playing
        if(elapsedTime >= 140f) // length of level one music 
        {
            finalMood = moodSlider.value; // save this so we can pass it to the SaveData function on win screen
            SceneManager.LoadScene("GameWin");
        }

    }
}
