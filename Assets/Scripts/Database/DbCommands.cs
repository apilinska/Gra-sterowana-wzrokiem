using UnityEngine;

public class DbCommands : MonoBehaviour
{
    public string GetRankingBoardCmd(string gameTable) {
        return "select us.name as user, res.score as score, res.id as id from " + gameTable + " res join users us on us.id = res.userId where score > 0 order by score desc limit @limit;";
    }

    public string GetUserByNameCmd() {
        return "select * from users where name like @name;";
    }

    public string GetLastScoreIdCmd(string gameTable) {
        return "select id from " + gameTable + " order by id desc limit 1;";
    }

    public string GetLastSessionCmd() {
        return "select u.* from user_sessions us join users u on u.id = us.userId order by us.id desc limit 1;";
    }

    public string GetUsersCmd() {
        return "select * from users;";
    }

    public string InsertGameScoreCmd(string gameTable) {
        return "insert into " + gameTable + " (userId, score, date) values (@userId, @score, now());";
    }

    public string InsertLogCmd() {
        return "insert into application_logs (exception_message, sql_command, userId, created) values (@exception, @command, @userId, now());";
    }

}