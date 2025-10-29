using System;
using System.Linq;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    // add leaderboard entries from Database to UI panel

    [SerializeField]
    private GameObject[] leaderboardEntries;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // grab top 3 highest scores from database and add them to the leaderboardEntries array
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateLeaderboard(GameInfo[] entries)
    {
    }
}
