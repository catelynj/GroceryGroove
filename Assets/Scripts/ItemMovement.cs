using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float tempo;
    public bool started;
    private bool _isPaused = false;

    void Start()
    {
        tempo = tempo / 60f; // BPM to unit movement per second
    }

    void Update()
    {
        if (!started)
        {
            //  start item movement with first touch -- moved to GameManager.cs
        }
        else if (!_isPaused)
        {
            transform.position -= new Vector3(0f, tempo * Time.deltaTime, 0f); // move items downwards to tempo
        }
    }

    // 9/19/25 - Claude AI Fix (referenced in Swipe.cs) -- Added methods to pause and resume downwards movement
    public void PauseMovement()
    {
        _isPaused = true;
    }

    public void ResumeMovement()
    {
        _isPaused = false;
    }
}
