using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DbConnect : MonoBehaviour
{
    MySqlConnection connection = null;
    MySqlDataReader reader = null;
    MySqlCommand command = null;

    string connectionString = null;
    string databaseName = "gra_sterowana_wzrokiem";

    void Start()
    {
        SetConnectionString();
        GetSqlVersion();
        GetUsers();
    }

    private void GetUsers()
    {
        connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            command = new MySqlCommand("select * from users;", connection);
            reader = command.ExecuteReader();
            Debug.Log(reader.GetName(0) + " : " + reader.GetName(1));
            while (reader.Read())
            {
                Debug.Log("[" + reader.GetInt32(0) + "] : " + reader.GetString(1));
            }
            connection.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void GetSqlVersion()
    {
        connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            command = new MySqlCommand("select version();", connection);
            Debug.Log("SQL version : " + command.ExecuteScalar().ToString());
            connection.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void SetConnectionString()
    {
        MySqlConnectionStringBuilder connectionBuilder = new MySqlConnectionStringBuilder();
        connectionBuilder.Server = "127.0.0.1";
        connectionBuilder.UserID = "root";
        connectionBuilder.Password = "agata";
        connectionBuilder.Database = databaseName;

        connectionString = connectionBuilder.ToString();
    }

}
