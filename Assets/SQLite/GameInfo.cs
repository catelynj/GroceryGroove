using UnityEngine;
using SQLite;

public class GameInfo
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public float Mood { get; set; }
    public int Difficulty { get; set; }
    public string Timestamp { get; set; }
}
