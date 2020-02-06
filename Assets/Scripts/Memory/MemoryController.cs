using System;
using UnityEngine;

public static class MemoryController
{
    [Header("Variables")]
    private static int currentGameNumber = 1;
    private static string randomizedNumber = "";
    private static string selectedNumber = "";

    private static int goodAnswers = 0;
    private static int numberOfGames = 3;

    public static void NextGame() {
        currentGameNumber++;  
    }

    public static int GetGameNumber() {
        return currentGameNumber;
    }

    public static bool IsLastGame() {
        return currentGameNumber == numberOfGames;
    }

    public static string RandomizeNumber {
        get {
            return randomizedNumber;
        }
        set {
            randomizedNumber = value;
        }
    }

    public static string SelectNumber {
        get {
            return selectedNumber;
        }
        set {
            selectedNumber = value;
            CompareText();
        }
    }

    public static void CompareText() {
        Boolean result = String.Compare(randomizedNumber, selectedNumber) == 0;
        if(result) goodAnswers++;   
    } 

    public static string GetResult() {
        return goodAnswers + " / " + numberOfGames;
    } 

    public static void ClearResult() {
        goodAnswers = 0;
        currentGameNumber = 1;
    }
}
