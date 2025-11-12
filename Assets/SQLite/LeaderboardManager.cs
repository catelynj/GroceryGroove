using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/**
 * LeaderboardManager.cs 
 * Author: Catelyn Jones
 * 
 * Purpose:
 * Fetches and displays leaderboard information on Main Menu scene
 * 
 * */

public class LeaderboardManager : MonoBehaviour
{
    // add leaderboard entries from Database to UI panel

    [SerializeField]
    private TMPro.TMP_Text[] leaderboardEntries;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leaderboardEntries = GetComponentsInChildren<TMPro.TMP_Text>();

        // grab top 3 highest scores from database and add them to the leaderboardEntries array
        var top3 = Database.GetTopScores();
        Debug.Log(top3.ToString());
        PopulateLeaderboard(top3);
        Debug.Log("Leaderboard: " + top3.ToString());
    }

    public void PopulateLeaderboard(GameInfo[] entries)
    {
        for (int i = 0; i < leaderboardEntries.Length; i++)
        {
            if (i < entries.Length)
            {
                var entry = entries[i];
                var entryText = leaderboardEntries[i].GetComponent<TMPro.TMP_Text>();
                entryText.text = $"{i + 1}. {entry.Name} - {entry.Score}";
            }
            else
            {
                var entryText = leaderboardEntries[i].GetComponent<TMPro.TMP_Text>();
                entryText.text = $"{i + 1}. ---";
            }
        }
    }
}
