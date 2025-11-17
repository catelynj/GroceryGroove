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
    private bool _started = false;
    public AudioSource levelOneMusic;
    public float timer;
    private float _elapsedTime;

    // Persistence
    public bool levelOver = false;
    private bool _dbInit = false;
    private bool _sceneReady = false;


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


        if (!_dbInit)
        {
            Database.InitializeDatabase();
            Debug.Log("Init DB Called");
            _dbInit = true;
            Debug.Log(_dbInit);
        }
        else if (_dbInit)
        {
            return;
        }
    }

    private void Update()
    {
        if (!_sceneReady) return; // skip until scene objects are assigned

        if (!_started && itemMov != null && levelOneMusic != null)
        {
            if (Input.touchCount > 0) // wait for first touch to start music and movement
            {
                _started = true;
                levelOneMusic.Play();
                itemMov.started = true;
            }
        }

        if (_started)
        {
            timer += Time.deltaTime;
            _elapsedTime += levelOneMusic.isPlaying ? Time.deltaTime : 0f; // keep track of elapsed time of music for CheckWin()

        }
        CheckWin();
    }

    // Game Win Condition
    private void CheckWin()
    {
        if ( difficulty == 0 && _elapsedTime >= 140f) // normal mode
        {
            finalMood = moodSlider.value; // save this so we can pass it to the SaveData function on win screen
            SceneManager.LoadScene("GameWin");
            levelOver = true;
        }
        else if( difficulty == 1 && _elapsedTime >= 105f) // hard mode
        {
            finalMood = moodSlider.value;
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
    #endregion

    #region Scanning Functions
    public void GoodScan()
    {
        streak += 1;

        if (difficulty == 1) // hard mode - higher base score and higher streak bonus
        {
            if(streak > 0)
                score += 200 + (streak * 75);
            
            else
                score += 200;
        }
        else
        {
            if (streak > 0)
                score += 100 + (streak * 50); // add bonus score for streak
            
            else
                score += 100; // base score
            
        }
    }

    public void BadScan()
    {
        // hard mode adjustments
        if(difficulty == 1)
        {
            streak = 0;
            moodSlider.value -= 5;
            mood -= 5;
        }
        else
        {
            streak = 0;
            moodSlider.value -= 2;
            mood -= 2;
        }
    }
    #endregion

    #region Persistence Fixes
    private void OnEnable()
    {
        // subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // unsubscribe from scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _sceneReady = false;

        if (scene.name == "Level1")
        {
            // assign scene objects
            itemMov = FindAnyObjectByType<ItemMovement>();
            levelOneMusic = GameObject.Find("LevelMusic")?.GetComponent<AudioSource>();
            scoreText = GameObject.Find("txtScore")?.GetComponent<TMPro.TMP_Text>();
            streakText = GameObject.Find("txtStreak")?.GetComponent<TMPro.TMP_Text>();
            moodSlider = GameObject.Find("MoodBar")?.GetComponent<Slider>();

            _started = false;
            score = 0;
            _elapsedTime = 0f;
            levelOver = false;

            // set flag
            _sceneReady = true;
        }
        else
        {
            // when not on Level1 -> clear to avoid null reference exceptions
            itemMov = null;
            levelOneMusic = null;
            scoreText = null;
            streakText = null;
            moodSlider = null;
        }


        if (scene.name == "MainMenu") // reset game state 
        {
            score = 0;
            difficulty = 0;
            streak = 0;
            mood = 100;
            timer = 0f;
            _started = false;
            _elapsedTime = 0f;
        }
    }

    #endregion
}
