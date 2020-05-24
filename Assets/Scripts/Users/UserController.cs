using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserController : DbConnect
{
    public GameObject userPrefab;
    public GameObject newUserPrefab;

    private string value = "";
    private List<User> users = new List<User>();

    void Start()
    {
        GetAllUsers();
        SetUsersData();
    }

    private void GetAllUsers() {
        users = GetUsers();
    }

    private void SetUsersData() {
        if(users.Count > 0) {
            foreach(var user in users) {
                CreateUserButton(user);
            }
        }
        CreateNewUserButton();
    }

    private void CreateUserButton(User user) {
        GameObject newObject = Instantiate(userPrefab);
        newObject.transform.SetParent(this.transform, false);
        string userName = user.name.ToUpper();
        newObject.GetComponentInChildren<Text>().text = userName;
        newObject.GetComponent<Button>().onClick.AddListener(() => SelectUser(user));
    }

    private void CreateNewUserButton() {
        GameObject newObject = Instantiate(newUserPrefab);
        newObject.transform.SetParent(this.transform, false);
        newObject.GetComponent<Button>().onClick.AddListener(() => AddUser());
    }

    private void AddUser() {
        SceneManager.LoadScene("AddUser");
    }

    private void SelectUser(User user) {
        InsertUserSession(user.id);
        //SetActiveUser(user);
        SceneManager.LoadScene("Menu");
    }
}