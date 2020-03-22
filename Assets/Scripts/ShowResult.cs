using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Game 
{
	Memory,
    Dexterity,
    Attention,
    Vision,
    Math
}

public class ShowResult : MonoBehaviour
{
    public Game Game;
    public Text Result;

    void Start()
    {
        SetResult();
    }

    string GetResult() {
        switch(Game) {
            case Game.Memory:
                return MemoryController.GetResult();

            case Game.Dexterity:
                return DexterityController.GetResult();

            case Game.Math:
                return MathController.GetResult();

            default:
                return "";
        }
    }

    void SetResult() {
        Result.text = GetResult();;
    }
}