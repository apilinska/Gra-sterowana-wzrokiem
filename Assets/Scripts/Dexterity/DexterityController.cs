using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexterityController : MonoBehaviour
{
    [Header("Variables")]
    private static int destroyedObstacles = 0;
    private static int catchedBonuses = 0;

    private static int obstacleWeight = 1;
    private static int bonusWeight = 5;

    public static void DestroyObstacle() {
        destroyedObstacles++;
    }

    public static void CatchBonus() {
        catchedBonuses++;
    }

    public static int CalculateResult() {
        return destroyedObstacles * obstacleWeight + catchedBonuses * bonusWeight;
    }

    public static string GetResult() {
        return CalculateResult().ToString();
    } 
}
