using UnityEngine;
using UnityEngine.UI;

public class EyeCursor : MonoBehaviour
{
    [Header("Visualization")]
    public Image cursor_bar;
    public Image cursor_focus;

    [Header("Focus variables")]
    protected static float focusTime = 1.2f;
    private static bool focus = false;

    void Start()
    {
        cursor_bar.gameObject.SetActive(false);
        cursor_focus.gameObject.SetActive(false);
    }

    public static void On() 
    {
        focus = true;
    }

    public static void Off() 
    {
        focus = false;
    }

    public static bool IsFocused() 
    {
        return focus;
    }

    public static float Time() 
    {
        return focusTime;
    }

    private bool isPointerVisible() 
    {
        return cursor_focus.gameObject.activeSelf;
    }

    private void showPointer() 
    {
        cursor_focus.gameObject.SetActive(true);
        cursor_bar.gameObject.SetActive(true);
    }

    private void hidePointer() 
    {
        cursor_focus.gameObject.SetActive(false);
        cursor_bar.gameObject.SetActive(false);
    }

    void Update()
    {
        if(focus) {
            if(!isPointerVisible()) 
            {
                showPointer();
            }
        } 
        else 
        {
            hidePointer();
        }
    }
}