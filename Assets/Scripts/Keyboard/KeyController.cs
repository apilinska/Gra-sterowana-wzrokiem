﻿using UnityEngine;
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
        var textComponent = gameObject.GetComponentInChildren<Text>();
        string input = textComponent != null ? textComponent.text : "Menu";

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
