using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static DBManager Instance { get; private set; }
    private string dbUri = "URI=file:mydb.sqlite";
    private string SQL_CREATE_TABLE_POSITIONS = "CREATE TABLE IF NOT EXISTS Posiciones(id INTEGER UNIQUE NOT NULL PRIMARY KEY," +
        "name TEXT,timestamp REAL,x REAL,y REAL,z REAL);";

    private IDbConnection dbConnection;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
            OpenDabase();
            InitializeDB();
        }
    }

    private void OpenDabase()
    {
        dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = "PRAGMA foreign_keys = ON";
        command.ExecuteNonQuery();
    }

    private void InitializeDB()
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = SQL_CREATE_TABLE_POSITIONS;
        command.ExecuteNonQuery();
    }

    public void AddPosition(Position pos)
    {
        string timestamp = pos.Timestamp.ToString("F7", CultureInfo.InvariantCulture);
        string x = pos.position.x.ToString("F7", CultureInfo.InvariantCulture);
        string y = pos.position.y.ToString("F7", CultureInfo.InvariantCulture);
        string z = pos.position.z.ToString("F7", CultureInfo.InvariantCulture);
        string commandText = $"INSERT INTO Posiciones (name, timestamp, x, y, z) VALUES (" +
            $"'{pos.name}',{timestamp},{x},{y},{z});";
        Debug.Log(commandText);
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = commandText;
        command.ExecuteNonQuery();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        dbConnection.Close();
    }
}
