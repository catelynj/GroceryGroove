using UnityEngine;
using SQLite;
using System.IO;

/**
 * Database.cs 
 * Author: Catelyn Jones
 * 
 * Purpose:
 * SQLite Database Handler
 * Creates DB and GameInfo table
 * Retrieves Top 3 Scores for use in LeaderboardManager.cs
 * Referenced in GameManager.cs, LeaderboardManager.cs, & GameWinScreen.cs
 * 
 * */

public class Database
{
    private static string dbName = "GG_DB.db";
    private static string dbPath;

    static Database()
    {
        #if UNITY_EDITOR
            dbPath = Path.Combine(Application.dataPath, dbName);
        #elif UNITY_IOS
            dbPath = Path.Combine(Application.persistentDataPath, dbName);
        #endif  
    }

    public static void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.CreateTable<GameInfo>();
        }
    }

    public static void SaveData(string name, int score, float mood, int difficulty)
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            var gameInfo = new GameInfo
            {   
                Name = name,
                Score = score,
                Mood = mood,
                Difficulty = difficulty,
                Timestamp = System.DateTime.UtcNow.ToString("o")
            };
            connection.Insert(gameInfo);
        }
    }

    public static GameInfo[] GetTopScores()
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            return connection.Query<GameInfo>(
                "SELECT Name, Score FROM GameInfo ORDER BY Score DESC LIMIT 3"
            ).ToArray();
        }
    }
}
