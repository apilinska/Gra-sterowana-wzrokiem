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

    //User activeUser = null;

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

    // public User GetActiveUser() {
    //     return activeUser;
    // }

    // public void SetActiveUser(User user) {
    //     activeUser = user;
    //     Debug.Log("Active user: " + activeUser.name);
    // }

    // public void CheckActiveUser() {
    //     Debug.Log("CheckActiveUser");
    //     if(activeUser == null) {
    //         Debug.Log("user is null");
    //         SetConnection();
    //         commandString = GetLastSessionCmd();
    //         command = new MySqlCommand(commandString, connection);
    //         reader = command.ExecuteReader();
    //         if(reader.Read())
    //         {
    //             activeUser = new User(reader);
    //         }
    //         CloseConnection();
    //     }
    // }

    // public void SetActiveUser()
    // {
    //     SetConnection();
    //     try
    //     {
    //         OpenConnection();
    //         commandString = GetLastSessionCmd();
    //         command = new MySqlCommand(commandString, connection);
    //         reader = command.ExecuteReader();
    //         if(reader.Read())
    //         {
    //             User user = new User(reader);
    //             activeUser = user;
    //         }
    //         CloseConnection();
    //     }
    //     catch (Exception ex)
    //     {
    //         SaveLog(ex.Message, command);
    //     }
    // }

    public User GetActiveUser()
    {
        User user = null;
        SetConnection();
        try
        {
            OpenConnection();
            commandString = GetLastSessionCmd();
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

    public bool InsertUser(string userName) 
    {
        SetConnection();
        try
        {
            OpenConnection();
            commandString = InsertUserCmd();
            command = new MySqlCommand(commandString, connection);
            command.Parameters.AddWithValue("userName", userName);
            command.ExecuteNonQuery();

            commandString = InsertNewUserSessionCmd();
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

    // public User GetLoggedUser()
    // {
    //     User loggedUser = null;
    //     try
    //     {
    //         OpenConnection();
    //         commandString = GetLoggedUserCmd();
    //         command = new MySqlCommand(commandString, connection);
    //         reader = command.ExecuteReader();
    //         if(reader.Read())
    //         {
    //             loggedUser = new User(reader);
    //         }
    //         CloseConnection();
    //     }
    //     catch (Exception ex)
    //     {
    //         SaveLog(ex.Message, command);
    //     }
    //     return loggedUser;
    // }

    public bool InsertUserSession(int userId) 
    {
        SetConnection();
        try
        {
            OpenConnection();
            commandString = InsertUserSessionCmd();
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
            commandString = InsertNewUserSessionCmd();
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
            commandString = InsertGameScoreCmd(game);
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
            //command.Parameters.AddWithValue("userId", activeUser != null ? activeUser.id : 1);
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
