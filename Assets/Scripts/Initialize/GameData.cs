public static class GameData
{
    public static GameMode gameMode = GameMode.Simulation;

    public static void SetMode(GameMode mode) 
    {
        gameMode = mode;
    }

    public static GameMode GetMode() 
    {
        return gameMode;
    }

    public static bool IsSimulationMode() 
    {
        return gameMode == GameMode.Simulation;
    }

    public static bool IsDeviceMode() 
    {
        return gameMode == GameMode.Device;
    }
}
