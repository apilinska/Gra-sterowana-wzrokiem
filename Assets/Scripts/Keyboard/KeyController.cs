using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    public KeyType type = KeyType.Char;

    private KeyboardController controller() 
    {
        return GetComponentInParent<KeyboardController>();
    }

    public void OnMouseEnter() 
    {
        string input = gameObject.GetComponentInChildren<Text>().text;
        var keyboardController = controller();
        if(keyboardController != null) 
        {
            keyboardController.MouseEnter(input, type);
        }
    }
    
    public void OnMouseExit() 
    {
        var keyboardController = controller();
        if(keyboardController != null) 
        {
            keyboardController.MouseExit();
        }
    }
}
