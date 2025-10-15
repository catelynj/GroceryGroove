using UnityEngine;
using SQLite;
using System.IO;

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

    public static void SaveData(int score, int difficulty)
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            var gameInfo = new GameInfo
            {
                Score = score,
                Difficulty = difficulty,
                Timestamp = System.DateTime.UtcNow.ToString("o")
            };
            connection.Insert(gameInfo);
        }
    }
}
