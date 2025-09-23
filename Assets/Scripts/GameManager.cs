using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance = null;
    public ItemMovement itemMov;
    private bool started = false;
    public AudioSource levelOneMusic;
    public int score;

    public static GameManager Instance
    {
        get
        {
            return _Instance;
        }
    }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        score = 0;
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
    }

    public void NoteHit()
    {
        Debug.Log("hit");
    }

    public void NoteMissed()
    {
        Debug.Log("missed");
    }
}
