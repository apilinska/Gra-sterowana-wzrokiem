using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : DbConnect
{
    public Button Logout;
    public Button Exit;
    public Text User;

    void Start()
    {
        Logout.onClick.AddListener(() => UserLogout());
        Exit.onClick.AddListener(() => ExitGame());
        SetUser();
    }

    private void UserLogout() {
        SceneManager.LoadScene("Login");
    }

    private void ExitGame() {
        Application.Quit();
        Debug.Log("Gra została zamknięta");
    }

    private void SetUser() {
        User activeUser = GetActiveUser();
        if(activeUser != null) {
            User.text = activeUser.name.ToUpper();
        }
    }
}
