using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using SQLite;
using System.IO;

/**
 * GameManager.cs 
 * Author: Catelyn Jones
 * 
 * Purpose:
 * Core Game Logic Manager
 * Handles score, mood, difficulty, timing, win condition, etc.
 * Referenced in multiple scripts
 * 
 * */

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance = null;
    public ItemMovement itemMov;

    // UI
    public int score;
    public TMPro.TMP_Text scoreText;
    public int streak;
    public TMPro.TMP_Text streakText;
    // Mood Bar
    public int mood;
    public Slider moodSlider;
    public float finalMood;

    public int difficulty = 0; // 0 = default 1 = hard

    // Beat & Music
    private bool started = false;
    public AudioSource levelOneMusic;
    public float timer;
    private float elapsedTime;

    // Persistence
    public bool levelOver = false;
    private bool dbInit = false;
    private bool sceneReady = false;


    #region Singleton Pattern
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
    }
    #endregion
    private void Start()
    {
        score = 0;
        difficulty = 0;
        streak = 0;
        mood = 100;
        timer = 0f;

        //get swipe script
        Swipe swipe = FindAnyObjectByType<Swipe>();


        if (!dbInit)
        {
            Database.InitializeDatabase();
            Debug.Log("Init DB Called");
            dbInit = true;
            Debug.Log(dbInit);
        }
        else if (dbInit)
        {
            return;
        }
    }

    private void Update()
    {

        if (!sceneReady) return; // skip until scene objects are assigned

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
            elapsedTime += levelOneMusic.isPlaying ? Time.deltaTime : 0f; // keep track of elapsed time of music for CheckWin()

        }
        CheckWin();
    }

    // Game Win Condition
    private void CheckWin()
    {
        //if song has finished playing
        if (elapsedTime >= 140f) // length of level one music 
        {
            finalMood = moodSlider.value; // save this so we can pass it to the SaveData function on win screen
            SceneManager.LoadScene("GameWin");
            levelOver = true;
        }

    }

    #region Music Controls
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
    #endregion

    #region Difficulty Controls
    public void SetDifficulty(int sliderValue)
    {
        difficulty = sliderValue;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    // hard mode -> decrease mood twice as much, increase item speed or something similar
    #endregion

    #region Scanning Functions
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
        moodSlider.value -= 1;
        mood -= 1;
    }
    #endregion

    #region Persistence Fixes
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
        sceneReady = false;

        if (scene.name == "Level1")
        {
            itemMov = FindAnyObjectByType<ItemMovement>();
            levelOneMusic = GameObject.Find("LevelMusic")?.GetComponent<AudioSource>();
            scoreText = GameObject.Find("txtScore")?.GetComponent<TMPro.TMP_Text>();
            streakText = GameObject.Find("txtStreak")?.GetComponent<TMPro.TMP_Text>();
            moodSlider = GameObject.Find("MoodBar")?.GetComponent<Slider>();

            started = false;
            score = 0;
            elapsedTime = 0f;
            levelOver = false;

            sceneReady = true;
        }
        else
        {
            itemMov = null;
            levelOneMusic = null;
            scoreText = null;
            streakText = null;
            moodSlider = null;
        }


        if (scene.name == "MainMenu")
        {
            score = 0;
            streak = 0;
            mood = 100;
            timer = 0f;
            started = false;
            elapsedTime = 0f;
        }
    }

    #endregion
}
