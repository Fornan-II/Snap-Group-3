using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public static class Database
{
    private const string DBPath = "Assets/Maxen/SQL";
    private static SqliteConnection _activeConnection;
    
    private static SqliteConnection CreateConnection()
    {
        SqliteConnection connection = new SqliteConnection("Data Source=" + DBPath + "/testDatabase.db; Version = 3; Compress = True; ");

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

    private static void CreateTables(SqliteConnection connection)
    {
        try
        {
            SqliteCommand command;
            command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE Celebrities(Col1 INT, Col2 VARCHAR(20))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE Photos(Col1 INT, Col2 VARCHAR(20))";
            command.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            Debug.LogWarning("Error creating tables: " + ex.Message);
        }
    }

    public static bool AddCharacter(CharacterInformation.Character newChar)
    {
        if(_activeConnection == null)
        {
            CreateConnection();
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
}
