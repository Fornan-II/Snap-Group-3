using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public static class Database
{
    private const string DBPath = "Assets/Maxen/SQL/papparaziDatabase.db";
    private static SqliteConnection _activeConnection;
    
    private static SqliteConnection CreateConnection()
    {
        SqliteConnection connection = new SqliteConnection("Data Source=" + DBPath + "; Version = 3; Compress = True; ");

        try
        {
            connection.Open();
        }
        catch(Exception ex)
        {
            Debug.LogWarning("Error creating connection: " + ex.Message);
        }

        return connection;
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Database/Create tables")]
    private static void CreateTables()
    {
        if(_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }

        try
        {
            SqliteCommand command;
            command = _activeConnection.CreateCommand();
            command.CommandText = "CREATE TABLE Celebrities(Col1 INT, Col2 VARCHAR(20))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE Photos(Col1 VARCHAR(20), Col2 INT, Col3 INT)";
            command.ExecuteNonQuery();
            Debug.Log("Tables created successfully");
        }
        catch(Exception ex)
        {
            Debug.LogWarning("Error creating tables: " + ex.Message);
        }

        _activeConnection.Close();
        _activeConnection = null;
    }

    [UnityEditor.MenuItem("Database/Log All Celebrities")]
    private static void LogAllCelebrities()
    {
        if (_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }

        try
        {
            SqliteDataReader dataReader;
            SqliteCommand command;
            command = _activeConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Celebrities";

            dataReader = command.ExecuteReader();

            string msg = "Celebrities: {";
            while(dataReader.Read())
            {
                CharacterInformation.Character dbChar = new CharacterInformation.Character { CharID = dataReader.GetInt32(0), Name = dataReader.GetString(1) };
                msg += "\n\tID: \"" + dbChar.CharID + "\", Name: \"" + dbChar.Name + "\"";
            }
            msg += "\n}";
            Debug.Log(msg);

            _activeConnection.Close();
            _activeConnection = null;
        }
        catch(Exception ex)
        {
            Debug.LogWarning("Error logging celebrities: " + ex.Message);
        }
    }
#endif

    public static bool AddCharacter(CharacterInformation.Character newChar)
    {
        if(_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }

        bool success = false;
        try
        {
            SqliteCommand command = _activeConnection.CreateCommand();
            command.CommandText = "INSERT INTO Celebrities(Col1, Col2) VALUES(" + newChar.CharID + ", '" + newChar.Name + "');";
            command.ExecuteNonQuery();
            success = true;
        }
        catch(Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
        return success;
    }

    public static bool AddPhoto(Photo newPhoto)
    {
        if (_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }

        bool success = false;
        try
        {
            SqliteCommand command = _activeConnection.CreateCommand();
            foreach (CharacterInformation.Character celebrity in newPhoto.celebritiesInPhoto)
            {
                command.CommandText = "INSERT INTO Photos(Col1, Col2, Col3) VALUES('" + newPhoto.path + "', " + celebrity.CharID + ", " + newPhoto.roundTakenDuring + ");";
                command.ExecuteNonQuery();
            }
            success = true;
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
        return success;
    }
}
