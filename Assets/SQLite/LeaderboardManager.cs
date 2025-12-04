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
    [SerializeField]
    private TMPro.TMP_Text[] _leaderboardEntries;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _leaderboardEntries = GetComponentsInChildren<TMPro.TMP_Text>();

        // grab top 3 highest scores from database
        var top3 = Database.GetTopScores();
        PopulateLeaderboard(top3);
    }

    public void PopulateLeaderboard(GameInfo[] entries)
    {
        for (int i = 0; i < _leaderboardEntries.Length; i++)
        {
            if (i < entries.Length)
            {
                var entry = entries[i];
                var entryText = _leaderboardEntries[i].GetComponent<TMPro.TMP_Text>();
                entryText.text = $"{i + 1}. {entry.Name} - {entry.Score}";
            }
            else
            {
                var entryText = _leaderboardEntries[i].GetComponent<TMPro.TMP_Text>();
                entryText.text = $"{i + 1}. ---";
            }
        }
    }

}
