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

    public static void SaveData(string name, int score, int difficulty)
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            var gameInfo = new GameInfo
            {   
                Name = name,
                Score = score,
                Difficulty = difficulty,
                Timestamp = System.DateTime.UtcNow.ToString("o")
            };
            connection.Insert(gameInfo);
        }
    }

    //public static GameInfo[] GetTopScores()
    //{
    //    using (var connection = new SQLiteConnection(dbPath))
    //    {
    //        // order by descending score, take top 3 (only Name and Score), add them to an array and return it
            
    //    }
    //}
}
