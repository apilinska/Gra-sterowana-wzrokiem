using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : DbConnect
{
    public Button Logout;
    public Button Exit;
    public Text User;

    private float timeLeft;

    void Start()
    {
        Logout.onClick.AddListener(() => UserLogout());
        Exit.onClick.AddListener(() => ExitGame());
        SetUser();
    }

    private void UserLogout() 
    {
        SceneManager.LoadScene("Login");
    }

    private void ExitGame() 
    {
        Application.Quit();
        Debug.Log("Gra została zamknięta");
    }

    private void SetUser() 
    {
        User activeUser = GetActiveUser();
        if(activeUser != null) {
            User.text = activeUser.name.ToUpper();
        }
    }

    public void MouseEnter(GameObject button) 
    {
        EyeCursor.On();
        StartCoroutine(loadButton(button));
    }

    public void MouseExit(GameObject button) 
    {
        EyeCursor.Off();
        StopAllCoroutines();
    }

    private IEnumerator loadButton(GameObject button) 
    {
        timeLeft = EyeCursor.Time();
        yield return new WaitForSeconds(EyeCursor.Time());
        if(EyeCursor.IsFocused()) {
            EyeCursor.Off();
            if(button.tag == "exit") {
                ExitGame();
            } else if(button.tag == "logout") {
                UserLogout();
            }
        }
    }
}
