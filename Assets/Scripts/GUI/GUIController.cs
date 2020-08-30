using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIController : DbConnect
{
    public Text activeUser;
    public Button homeBtn;
    
    void Start()
    {
        homeBtn.onClick.AddListener(() => BackToMenu());
        SetUser();
    }

    private void BackToMenu()
     {
        SceneManager.LoadScene("Menu");
    }

    private void SetUser() {
        User user = GetActiveUser();
        if(user != null) 
        {
            activeUser.text = user.name.ToUpper();
        }
    }

    public void MouseEnter() 
    {
        EyeCursor.On();
        StartCoroutine(loadButton());
    }

    public void MouseExit() 
    {
        EyeCursor.Off();
        StopAllCoroutines();
    }

    private IEnumerator loadButton() 
    {
        yield return new WaitForSeconds(EyeCursor.Time());
        if(EyeCursor.IsFocused()) {
            EyeCursor.Off();
            SceneManager.LoadScene("Menu");
        }
    }
}
