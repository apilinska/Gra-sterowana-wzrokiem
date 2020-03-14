using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Game 
{
	Memory,
    Dexterity,
    Attention,
    Vision
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

            default:
                return "";
        }
    }

    void SetResult() {
        Result.text = GetResult();;
    }
}