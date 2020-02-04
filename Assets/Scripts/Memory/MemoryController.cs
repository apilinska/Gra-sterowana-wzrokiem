using System;
using UnityEngine;

public static class MemoryController
{
    [Header("Variables")]
    private static string randomizedNumber = "";
    private static string selectedNumber = "";

    public static string RandomizeNumber
    {
        get
        {
            return randomizedNumber;
        }
        set {
            randomizedNumber = value;
            Debug.Log("randomized: [" + randomizedNumber + "]");
        }
    }

    public static string SelectNumber
    {
        get
        {
            return selectedNumber;
        }
        set {
            selectedNumber = value;
            Debug.Log("selected: [" + selectedNumber + "]");
        }
    }

    public static void CompareText() {
        Boolean result = String.Compare(randomizedNumber, selectedNumber) == 0;
        Debug.Log("same: " + result);
    }  
}
