using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserController : DbConnect
{
    public GameObject userPrefab = null;
    public GameObject newUserPrefab = null;

    private string value = "";
    private List<User> users = new List<User>();

    void Start()
    {
        if(userPrefab && newUserPrefab) 
        {
            GetAllUsers();
            SetUsersData();
        }
    }

    private void GetAllUsers() 
    {
        users = GetUsers();
    }

    private void AddUser() 
    {
        SceneManager.LoadScene("AddUser");
    }

    private void SelectUser(User user) 
    {
        InsertUserSession(user.id);
        SceneManager.LoadScene("Menu");
    }

    public void MouseEnter(string userName = null) 
    {
        EyeCursor.On();
        StartCoroutine(loadButton(userName));
    }

    public void MouseExit() 
    {
        EyeCursor.Off();
        StopAllCoroutines();
    }

    private void CreateNewUserButton() 
    {
        GameObject newObject = Instantiate(newUserPrefab);
        newObject.transform.SetParent(this.transform, false);
        newObject.GetComponent<Button>().onClick.AddListener(() => AddUser());
    }

    private void CreateUserButton(User user) 
    {
        GameObject newObject = Instantiate(userPrefab);
        newObject.transform.SetParent(this.transform, false);
        string userName = user.name.ToUpper();
        newObject.GetComponentInChildren<Text>().text = userName;
        newObject.GetComponent<Button>().onClick.AddListener(() => SelectUser(user));
    }

    private void SetUsersData() 
    {
        if(users != null && users.Count > 0) 
        {
            foreach(var user in users) 
            {
                CreateUserButton(user);
            }
        }
        CreateNewUserButton();
    }

	private IEnumerator loadButton(string userName) 
    {
        yield return new WaitForSeconds(EyeCursor.Time());
		if(EyeCursor.IsFocused())
        {
			EyeCursor.Off();
            if(userName != null) 
            {
                User selectedUser = users.Find(x => x.name.ToLower() == userName.ToLower());
                if(selectedUser != null) 
                {
                    InsertUserSession(selectedUser.id);
                    SceneManager.LoadScene("Menu");
                }
            }
            else 
            {
                AddUser();
            }
		}
    }
}