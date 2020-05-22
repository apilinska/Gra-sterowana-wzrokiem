using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DbConnect : DbCommands
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

    User activeUser = null;

    void Start()
    {
        SetConnection();
        SetActiveUser();
    }

    /* Connection */

    public void SetConnection()
    {
        SetConnectionString();
        connection = new MySqlConnection(connectionString);
    }

    private void CloseConnection() {
        if(connection.State != ConnectionState.Closed) {
            connection.Close();  
        }
    }

    private void OpenConnection() 
    {
        command = null;
        commandString = null;
        if(connection.State != ConnectionState.Open) {
            connection.Open();  
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

    /* Users */

    public User GetActiveUser() {
        return activeUser;
    }

    public void SetActiveUser()
    {
        SetConnection();
        try
        {
            OpenConnection();
            commandString = GetLastSessionCmd();
            command = new MySqlCommand(commandString, connection);
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                User user = new User(reader);
                activeUser = user != null ? user : new User(1, "");
            }
            CloseConnection();
        }
        catch (Exception ex)
        {
            SaveLog(ex.Message, command);
        }
    }

    public User GetUserByName(string name)
    {
        User user = null;
        try
        {
            OpenConnection();
            commandString = GetUserByNameCmd();
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
            commandString = GetUsersCmd();
            command = new MySqlCommand(commandString, connection);
            reader = command.ExecuteReader();
            //Debug.Log(reader.GetName(0) + " : " + reader.GetName(1));
            
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

    /* Memory game */

    public int GetLastMemoryScore() 
    {
        SetActiveUser();
        return GetLastScoreId(memory_results);
    }

    public List<RankingBoard> GetMemoryRankingBoard(int limit)
    {
        SetActiveUser();
        return GetRankingBoard(memory_results, limit);
    }

    public void MemoryInsertScore(int score)
    {
        SetActiveUser();
        InsertGameScore(memory_results, score);
    }

    /* Math game */

    public int GetLastMathScore() 
    {
        SetActiveUser();
        return GetLastScoreId(math_results);
    }

    public List<RankingBoard> GetMathRankingBoard(int limit)
    {
        SetActiveUser();
        return GetRankingBoard(math_results, limit);
    }

    public void MathInsertScore(int score)
    {
        SetActiveUser();
        InsertGameScore(math_results, score);
    }

    /* Dexterity game */

    public int GetLastDexterityScore() 
    {
        SetActiveUser();
        return GetLastScoreId(dexterity_results);
    }

    public List<RankingBoard> GetDexterityRankingBoard(int limit)
    {
        SetActiveUser();
        return GetRankingBoard(dexterity_results, limit);
    }

    public void DexterityInsertScore(int score)
    {
        SetActiveUser();
        InsertGameScore(dexterity_results, score);
    }

    /* Game operations */

    public void InsertGameScore(string game, int score) 
    {
        SetConnection();
        try
        {
            OpenConnection();
            commandString = InsertGameScoreCmd(game);
            command = new MySqlCommand(commandString, connection);
            command.Parameters.AddWithValue("userId", activeUser.id);
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
            commandString = GetLastScoreIdCmd(game);
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
            commandString = GetRankingBoardCmd(game);
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
            commandString = InsertLogCmd();
            command = new MySqlCommand(commandString, connection);
            command.Parameters.AddWithValue("exception", exceptionMessage);
            command.Parameters.AddWithValue("command", sqlCommand);
            command.Parameters.AddWithValue("userId", activeUser.id);
            command.ExecuteNonQuery();
            CloseConnection();
        }
        catch (Exception ex)
        {
            Debug.Log("Dodac do pliku: " + ex.Message);
            Log.Save(ex.Message);
        }
    }
}
