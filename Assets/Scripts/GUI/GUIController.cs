using System.Collections;
using System.Collections.Generic;
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

    private void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    private void SetUser() {
        User user = GetActiveUser();
        if(user != null) {
            activeUser.text = user.name.ToUpper();
        }
    }
}
