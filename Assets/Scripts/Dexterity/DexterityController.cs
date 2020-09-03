using UnityEngine;

public class DexterityController : MonoBehaviour
{
    [Header("Variables")]
    private static int destroyedObstacles = 0;
    private static int catchedBonuses = 0;

    private static int obstacleWeight = 2;
    private static int bonusWeight = 10;

    public static void DestroyObstacle() 
    {
        destroyedObstacles++;
    }

    public static void CatchBonus() 
    {
        catchedBonuses++;
    }

    public static int CalculateResult() 
    {
        return destroyedObstacles * obstacleWeight + catchedBonuses * bonusWeight;
    }

    public static string GetResult() 
    {
        return CalculateResult().ToString();
    } 

    public static void ClearPoints()
    {
        destroyedObstacles = 0;
        catchedBonuses = 0;
    }
}
