public static class DbCommands
{
    public static string GetRankingBoardCmd(string gameTable) 
    {
        return "select us.name as user, res.score as score, res.id as id from " + gameTable + " res join users us on us.id = res.userId where score > 0 order by score desc, date desc limit @limit;";
    }

    public static string GetUserByNameCmd() 
    {
        return "select * from users where name like @name;";
    }

    public static string GetLastScoreIdCmd(string gameTable) 
    {
        return "select id from " + gameTable + " order by id desc limit 1;";
    }

    public static string GetLastSessionCmd() 
    {
        return "select u.* from user_sessions us join users u on u.id = us.userId order by us.id desc limit 1;";
    }

    public static string GetUsersCmd() 
    {
        return "select * from users;";
    }

    public static string InsertGameScoreCmd(string gameTable) 
    {
        return "insert into " + gameTable + " (userId, score, date) values ((select userId from user_sessions order by id desc limit 1), @score, now());";
    }

    public static string InsertLogCmd() 
    {
        return "insert into application_logs (exception_message, sql_command, userId, created) values (@exception, @command, (select id from users order by id desc limit 1), now());";
    }

    public static string InsertUserCmd() 
    {
        return "insert into users (name) values (@userName);";
    }

    public static string InsertUserSessionCmd() 
    {
        return "insert into user_sessions (userId, created) values (@userId, now());";
    }

    public static string InsertNewUserSessionCmd() 
    {
        return "insert into user_sessions (userId, created) values ((select id from users order by id desc limit 1), now());";
    }
}