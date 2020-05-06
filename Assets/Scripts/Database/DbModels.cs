using System;
using MySql.Data.MySqlClient;

[System.Serializable]
public class User 
{
    public int id;
    public string name;

    public User(int id, string name) 
    {
        this.id = id;
        this.name = name;
    }

    public User(MySqlDataReader reader) 
    {
        this.id = Convert.ToInt32(reader["id"]);
        this.name = reader["name"].ToString();
    }
}

[System.Serializable]
public class RankingBoard 
{
    public int id;
    public string user;
    public int score;

    public RankingBoard(int id, int score, string user) 
    {
        this.id = id;
        this.score = score;
        this.user = user;
    }

    public RankingBoard(MySqlDataReader reader) 
    {
        this.id = Convert.ToInt32(reader["id"]);
        this.score = Convert.ToInt32(reader["score"]);
        this.user = reader["user"].ToString();
    }
}