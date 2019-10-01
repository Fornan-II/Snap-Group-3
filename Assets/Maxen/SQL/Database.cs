using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public static class Database
{
    private static string DBPath = Application.persistentDataPath + "/papparaziDatabase.db"; //"Assets/Maxen/SQL/papparaziDatabase.db";
    private static SqliteConnection _activeConnection;
    
    private static SqliteConnection CreateConnection()
    {
        SqliteConnection connection = new SqliteConnection("Data Source=" + DBPath + "; Version = 3; Compress = True; ");

        try
        {
            connection.Open();

            SqliteCommand command;
            command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Celebrities(celeb_id INT, name VARCHAR(20), UNIQUE(celeb_id))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Photos(img_path VARCHAR(20), celeb_id INT, round_number INT)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT OR IGNORE INTO Celebrities(celeb_id, name) VALUES(0, 'Bob Bobson')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT OR IGNORE INTO Celebrities(celeb_id, name) VALUES(1, 'Maxen McCoy')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT OR IGNORE INTO Celebrities(celeb_id, name) VALUES(2, 'Nathan Dalessio')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT OR IGNORE INTO Celebrities(celeb_id, name) VALUES(3, 'Mathew Woods')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT OR IGNORE INTO Celebrities(celeb_id, name) VALUES(4, 'Rob Robinson')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT OR IGNORE INTO Celebrities(celeb_id, name) VALUES(5, 'John Johanson')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT OR IGNORE INTO Celebrities(celeb_id, name) VALUES(6, 'noodle')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT OR IGNORE INTO Celebrities(celeb_id, name) VALUES(7, 'Mark Markson')";
            command.ExecuteNonQuery();

        }
        catch(Exception ex)
        {
            Debug.LogWarning("Error creating connection: " + ex.Message + " {\n" + ex.StackTrace + "\n}");
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
            command.CommandText = "CREATE TABLE Celebrities(celeb_id INT, name VARCHAR(20))";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE Photos(img_path VARCHAR(20), celeb_id INT, round_number INT)";
            command.ExecuteNonQuery();
            Debug.Log("Tables created successfully");
        }
        catch(Exception ex)
        {
            Debug.LogWarning("Error creating tables: " + ex.Message);
        }

        EndConnection();
    }

    [UnityEditor.MenuItem("Database/Log All Celebrities")]
    private static void LogAllCelebrities()
    {
        string msg = "Celebrities: {";
        CharacterInformation.Character[] allCelebs = GetAllCharacters();
        foreach (CharacterInformation.Character celeb in allCelebs)
        {
            msg += "\n\tID: \"" + celeb.CharID + "\", Name: \"" + celeb.Name + "\"";
        }
        msg += "\n}";
        Debug.Log(msg);
    }

    [UnityEditor.MenuItem("Database/Log All Photos")]
    private static void LogAllPhotos()
    {
        string msg = "Photos: {";
        foreach (Photo p in GetAllPhotos())
        {
            msg += "\n\timgPath: \"" + p.Path + "\", Round #: \"" + p.RoundTakenDuring + "\", Celebrities: {";
            foreach (CharacterInformation.Character celeb in p.CelebritiesInPhoto)
            {
                msg += "\n\t\tID: \"" + celeb.CharID + "\", Name: \"" + celeb.Name + "\"";
            }
        }
        msg += "\n\t}\n}";
        Debug.Log(msg);
    }

    [UnityEditor.MenuItem("Database/Dangerous/Drop Tables")]
    private static void DropTables()
    {
        if (_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }

        try
        {
            SqliteCommand command;
            command = _activeConnection.CreateCommand();
            command.CommandText = "DROP TABLE Celebrities";
            command.ExecuteNonQuery();
            command.CommandText = "DROP TABLE Photos";
            command.ExecuteNonQuery();

            EndConnection();

            Debug.Log("Tables dropped");
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error dropping database: " + ex.Message);
        }
    }

    [UnityEditor.MenuItem("Database/Dangerous/Empty Celebrity Table")]
    private static void EmptyCelebrityTable()
    {
        if (_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }
        try
        {
            SqliteCommand command = _activeConnection.CreateCommand();
            command.CommandText = "DELETE FROM Celebrities";
            command.ExecuteNonQuery();

            EndConnection();
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error emptying celebrity table: " + ex.Message);
        }
    }

    [UnityEditor.MenuItem("Database/Dangerous/Empty Photo Table")]
    private static void EmptyPhotoTable()
    {
        if (_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }
        try
        {
            SqliteCommand command = _activeConnection.CreateCommand();
            command.CommandText = "DELETE FROM Photos";
            command.ExecuteNonQuery();

            EndConnection();
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error emptying celebrity table: " + ex.Message);
        }
    }
    
    [UnityEditor.MenuItem("Database/Close Connection")]
#endif
    private static void EndConnection()
    {
        _activeConnection?.Close();
        _activeConnection = null;
    }

    #region Add to database
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
            command.CommandText = "INSERT INTO Celebrities(celeb_id, name) VALUES(" + newChar.CharID + ", '" + newChar.Name + "');";
            command.ExecuteNonQuery();
            success = true;

            EndConnection();
        }
        catch(Exception ex)
        {
            Debug.LogWarning("Error inserting character " + newChar.Name + ": " + ex.Message);
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
            foreach (CharacterInformation.Character celebrity in newPhoto.CelebritiesInPhoto)
            {
                command.CommandText = "INSERT INTO Photos(img_path, celeb_id, round_number) VALUES('" + newPhoto.Path + "', " + celebrity.CharID + ", " + newPhoto.RoundTakenDuring + ");";
                command.ExecuteNonQuery();
            }
            success = true;

            EndConnection();
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error inserting photo: " + ex.Message);
        }
        return success;
    }
    #endregion

    #region Get all from Database
    public static CharacterInformation.Character[] GetAllCharacters()
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

            List<CharacterInformation.Character> allCelebs = new List<CharacterInformation.Character>();
            while (dataReader.Read())
            {
                allCelebs.Add(new CharacterInformation.Character { CharID = dataReader.GetInt32(0), Name = dataReader.GetString(1) });
            }

            EndConnection();

            return allCelebs.ToArray();
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error getting all celebrities: " + ex.Message);
            return null;
        }
    }
    
    public static Photo[] GetAllPhotos()
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
            command.CommandText = "SELECT P.img_path, P.round_number, P.celeb_id, (SELECT C.name FROM Celebrities C WHERE C.celeb_id = P.celeb_id) FROM Photos P";

            dataReader = command.ExecuteReader();

            Dictionary<string, Photo> photoDict = new Dictionary<string, Photo>();
            while (dataReader.Read())
            {
                string photoPath = dataReader.GetString(0);
                if (photoDict.ContainsKey(photoPath))
                {
                    photoDict[photoPath].CelebritiesInPhoto.Add(new CharacterInformation.Character { CharID = dataReader.GetInt32(2), Name = dataReader.GetString(3) });
                }
                else
                {
                    Photo newPhoto = new Photo(photoPath, dataReader.GetInt32(1));
                    newPhoto.CelebritiesInPhoto.Add(new CharacterInformation.Character { CharID = dataReader.GetInt32(2), Name = dataReader.GetString(3) });
                    photoDict.Add(photoPath, newPhoto);
                }
            }

            EndConnection();

            Photo[] allPhotos = new Photo[photoDict.Count];
            photoDict.Values.CopyTo(allPhotos, 0);
            return allPhotos;
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error getting all celebrities: " + ex.Message);
            return null;
        }
    }
    #endregion

    #region Update Database
    public static bool UpdateCharacter(CharacterInformation.Character existingChar)
    {
        if (_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }

        bool success = false;
        try
        {
            SqliteCommand command = _activeConnection.CreateCommand();
            command.CommandText = "UPDATE Celebrities SET name = \'" + existingChar.Name + "\' WHERE celeb_id = " + existingChar.CharID + ";";
            command.ExecuteNonQuery();
            success = true;

            EndConnection();
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error updating character " + existingChar.Name + ": " + ex.Message);
        }
        return success;
    }
    #endregion

    #region Delete from Database
    public static bool DeleteCharacter(CharacterInformation.Character existingChar)
    {
        if (_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }

        bool success = false;
        try
        {
            SqliteCommand command = _activeConnection.CreateCommand();
            command.CommandText = "DELETE FROM Celebrities WHERE celeb_id = '" + existingChar.CharID + "'";
            command.ExecuteNonQuery();
            success = true;

            EndConnection();
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error deleting character " + existingChar.Name + ": " + ex.Message);
        }
        return success;
    }

    public static bool DeletePhoto(string photoPath)
    {
        if (_activeConnection == null)
        {
            _activeConnection = CreateConnection();
        }

        bool success = false;
        try
        {
            SqliteCommand command = _activeConnection.CreateCommand();
            command.CommandText = "DELETE FROM Photos WHERE img_path = '" + photoPath + "'";
            command.ExecuteNonQuery();
            success = true;

            EndConnection();
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error deleting photo \"" + photoPath + "\": " + ex.Message);
        }
        return success;
    }
    #endregion
}
