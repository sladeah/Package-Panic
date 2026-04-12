using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }
    
    private string dbName = "URI=file:PackagePanic_Stats.sqlite";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        CreateTable();
    }

    private void CreateTable()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS MatchHistory (matchID INTEGER PRIMARY KEY AUTOINCREMENT, score INTEGER);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void SaveMatchScore(int finalScore)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO MatchHistory (score) VALUES (" + finalScore + ");";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        Debug.Log("Score of " + finalScore + " saved to SQLite Database!");
    }

    public int GetHighScore()
    {
        int highScore = 0;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT MAX(score) FROM MatchHistory;";
                
                var result = command.ExecuteScalar();
                if (result != System.DBNull.Value && result != null)
                {
                    highScore = System.Convert.ToInt32(result);
                }
            }
            connection.Close();
        }
        return highScore;
    }
}