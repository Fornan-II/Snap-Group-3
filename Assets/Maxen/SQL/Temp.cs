using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class Temp : MonoBehaviour
{
    [System.Serializable]
    public struct TableItem
    {
        public string MSG;
        public int VAL;
    }

    static SqliteConnection sqlite_conn;

    public List<TableItem> items;
    public bool readDB = false;

    static SqliteConnection CreateConnection()
    {

        SqliteConnection sqlite_conn;
        // Create a new database connection:
        sqlite_conn = new SqliteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
        // Open the connection:
        try
        {
            sqlite_conn.Open();
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
        return sqlite_conn;
    }

    // Start is called before the first frame update
    void Start()
    {
        sqlite_conn = CreateConnection();
        CreateTable(sqlite_conn);
        InsertData(sqlite_conn);
    }

    private void Update()
    {
        if(readDB)
        {
            ReadData();
            readDB = false;
        }
    }

    static void CreateTable(SqliteConnection conn)
    {
        try
        {
            SqliteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE SampleTable(Col1 VARCHAR(20), Col2 INT)";
            string Createsql1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Createsql1;
            sqlite_cmd.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    static void InsertData(SqliteConnection conn)
    {
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();
        sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test Text ', 1); ";
        sqlite_cmd.ExecuteNonQuery();
        sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test1 Text1 ', 2); ";
        sqlite_cmd.ExecuteNonQuery();
        sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test2 Text2 ', 3); ";
        sqlite_cmd.ExecuteNonQuery();


        sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES('Test3 Text3 ', 3); ";
        sqlite_cmd.ExecuteNonQuery();

    }

    void ReadData()
    {
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = sqlite_conn.CreateCommand();
        sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

        sqlite_datareader = sqlite_cmd.ExecuteReader();

        items.Clear();
        while (sqlite_datareader.Read())
        {
            string myreader = sqlite_datareader.GetString(0);
            int value = sqlite_datareader.GetInt32(1);
            items.Add(new TableItem { MSG = myreader, VAL = value });
        }
    }
}