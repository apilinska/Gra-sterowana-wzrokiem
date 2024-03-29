﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DbConnect : MonoBehaviour
{
    MySqlConnection connection = null;
    MySqlDataReader reader = null;
    MySqlCommand command = null;

    string commandString = null;
    string connectionString = null;
    string databaseName = "gra_sterowana_wzrokiem";

    string math_results = "math_results";
    string memory_results = "memory_results";
    string dexterity_results = "dexterity_results";

    void Start()
    {
        SetConnection();
    }

    /* Connection */

    public void SetConnection()
    {
        SetConnectionString();
        connection = new MySqlConnection(connectionString);
    }

    private void CloseConnection() 
    {
        if(connection.State != ConnectionState.Closed) 
        {
            connection.Close();  
        }
    }

    private void OpenConnection() 
    {
        command = null;
        commandString = null;
        if(connection.State != ConnectionState.Open) 
        {
            connection.Open();  
        }
    }

    private void SetConnectionString()
    {
        MySqlConnectionStringBuilder connectionBuilder = new MySqlConnectionStringBuilder();
        connectionBuilder.Server = "127.0.0.1";
        connectionBuilder.UserID = "root";
        connectionBuilder.Password = "";
        connectionBuilder.Database = databaseName;
        connectionString = connectionBuilder.ToString();
    }

    /* Users */

    public User GetActiveUser()
    {
        User user = null;
        SetConnection();
        try
        {
            OpenConnection();
            commandString = DbCommands.GetLastSessionCmd();
            command = new MySqlCommand(commandString, connection);
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                user = new User(reader);
            }
            CloseConnection();
            return user;
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
            return null;
        }
    }

    public User GetUserByName(string name)
    {
        User user = null;
        try
        {
            OpenConnection();
            commandString = DbCommands.GetUserByNameCmd();
            command = new MySqlCommand(commandString, connection);
            command.Parameters.AddWithValue("name", name);
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                user = new User(reader);
            }
            CloseConnection();
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
        }
        return user;
    }

    public List<User> GetUsers()
    {
        SetConnection();
        List<User> users = new List<User>();
        try
        {
            OpenConnection();
            commandString = DbCommands.GetUsersCmd();
            command = new MySqlCommand(commandString, connection);
            reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                users.Add(new User(reader));
            }
            CloseConnection();
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
        }
        return users;
    }

    public bool InsertUser(string userName) 
    {
        SetConnection();
        try
        {
            OpenConnection();
            commandString = DbCommands.InsertUserCmd();
            command = new MySqlCommand(commandString, connection);
            command.Parameters.AddWithValue("userName", userName);
            command.ExecuteNonQuery();

            commandString = DbCommands.InsertNewUserSessionCmd();
            command = new MySqlCommand(commandString, connection);
            command.ExecuteNonQuery();

            CloseConnection();
            return true;
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
            return false;
        }
    }

    /* User sessions */

    public bool InsertUserSession(int userId) 
    {
        SetConnection();
        try
        {
            OpenConnection();
            commandString = DbCommands.InsertUserSessionCmd();
            command = new MySqlCommand(commandString, connection);
            command.Parameters.AddWithValue("userId", userId);
            command.ExecuteNonQuery();
            CloseConnection();
            return true;
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
            return false;
        }
    }

    public bool InsertNewUserSession() 
    {
        SetConnection();
        try
        {
            OpenConnection();
            commandString = DbCommands.InsertNewUserSessionCmd();
            command = new MySqlCommand(commandString, connection);
            command.ExecuteNonQuery();
            CloseConnection();
            return true;
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
            return false;
        }
    }

    /* Memory game */

    public int GetLastMemoryScore() 
    {
        return GetLastScoreId(memory_results);
    }

    public List<RankingBoard> GetMemoryRankingBoard(int limit)
    {
        return GetRankingBoard(memory_results, limit);
    }

    public void MemoryInsertScore(int score)
    {
        InsertGameScore(memory_results, score);
    }

    /* Math game */

    public int GetLastMathScore() 
    {
        return GetLastScoreId(math_results);
    }

    public List<RankingBoard> GetMathRankingBoard(int limit)
    {
        return GetRankingBoard(math_results, limit);
    }

    public void MathInsertScore(int score)
    {
        InsertGameScore(math_results, score);
    }

    /* Dexterity game */

    public int GetLastDexterityScore() 
    {
        return GetLastScoreId(dexterity_results);
    }

    public List<RankingBoard> GetDexterityRankingBoard(int limit)
    {
        return GetRankingBoard(dexterity_results, limit);
    }

    public void DexterityInsertScore(int score)
    {
        InsertGameScore(dexterity_results, score);
    }

    /* Game operations */

    public void InsertGameScore(string game, int score) 
    {
        SetConnection();
        try
        {
            OpenConnection();
            commandString = DbCommands.InsertGameScoreCmd(game);
            command = new MySqlCommand(commandString, connection);
            command.Parameters.AddWithValue("score", score);
            command.ExecuteNonQuery();
            CloseConnection();
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
        }
    }

    public int GetLastScoreId(string game) 
    {
        SetConnection();
        int scoreId = 0;
        try
        {
            OpenConnection();
            commandString = DbCommands.GetLastScoreIdCmd(game);
            command = new MySqlCommand(commandString, connection);
            scoreId = Convert.ToInt32(command.ExecuteScalar());
            CloseConnection();
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
        }
        return scoreId;
    }

    public List<RankingBoard> GetRankingBoard(string game, int limit)
    {
        SetConnection();
        List<RankingBoard> rankingBoard = new List<RankingBoard>();
        try
        {
            OpenConnection();
            commandString = DbCommands.GetRankingBoardCmd(game);
            command = new MySqlCommand(commandString, connection);
            command.Parameters.AddWithValue("limit", limit);
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                rankingBoard.Add(new RankingBoard(reader));
            }
            CloseConnection();
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
        }
        return rankingBoard;
    }

    /* Log */

    public void SaveLog(string exceptionMessage, MySqlCommand command = null) 
    {
        string sqlCommand = command != null ? command.CommandText : null;
        try
        {
            OpenConnection();
            commandString = DbCommands.InsertLogCmd();
            command = new MySqlCommand(commandString, connection);
            command.Parameters.AddWithValue("exception", exceptionMessage);
            command.Parameters.AddWithValue("command", sqlCommand);
            command.ExecuteNonQuery();
            CloseConnection();
        }
        catch (Exception ex)
        {
            Log.Save(ex.Message);
        }
    }
}
