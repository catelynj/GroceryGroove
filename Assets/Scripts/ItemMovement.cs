using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float tempo;
    public bool started;
    private bool isPaused = false; // 9/19/25 - Claude AI Fix: when the item is being dragged, pause left to right scrolling movement 

    void Start()
    {
        tempo = tempo / 60f; // BPM to unit movement per second
    }

    void Update()
    {
        if (!started)
        {
            //moved functionality to gamemanager

            //if(Input.touchCount > 0) // wait for first touch to start music and movement
            //{
            //    started = true;
            //}
        }
        else if (!isPaused)
        {
            transform.position -= new Vector3(0f, tempo * Time.deltaTime, 0f); // move items downwards to tempo
        }
    }

    // 9/19/25 - Claude AI Fix: Added methods to pause and resume movement
    public void PauseMovement()
    {
        isPaused = true;
    }

    public void ResumeMovement()
    {
        isPaused = false;
    }
}
