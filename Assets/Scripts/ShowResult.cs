using UnityEngine;
using UnityEngine.UI;

public class ShowResult : MonoBehaviour
{
    public Game Game;
    public Text Result;

    void Start()
    {
        SetResult();
    }

    private void SetResult() 
    {  
        Result.text = GetResult();;
    }

    private string GetResult() 
    {
        switch(Game) 
        {
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
}