using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyboardController : DbConnect
{
    public Text input;
    public GameObject dialog;
    public int dialogCloseTimer = 3;
    public Button deleteBtn;
    public Button enterBtn;
    public Button clearBtn;
    public Button[] buttons;

    private string value = "";
    private List<User> users;

    void Start()
    {
        dialog.SetActive(false);
        GetUsersFromDatabase();
        deleteBtn.onClick.AddListener(() => Delete());
        enterBtn.onClick.AddListener(() => Submit());
        clearBtn.onClick.AddListener(() => Clear());

        foreach(Button button in buttons) {
            Text number = button.GetComponentInChildren<Text>();
            button.onClick.AddListener(() => SelectNumber(number.text));
        }
    }

    private void SetText() {
        input.text = value;
    }

    private void GetUsersFromDatabase() {
        users = GetUsers();
    }

    private void Clear() {
        value = "";
        SetText();
    }

    private void Delete() {
        if(value.Length > 0) {
            value = value.Substring(0,value.Length - 1);
            SetText();
        }
    }
    private IEnumerator ShowDialog() {
        dialog.GetComponentInChildren<Text>().text = "Istnieje już użytkownik o podanej nazwie";
        dialog.SetActive(true);
        yield return new WaitForSeconds(dialogCloseTimer);
        dialog.SetActive(false);
    }

    private bool ValidateNewUser(string value) {
        if(users != null && users.Count > 0) {
            return (users.Find(x => x.name.ToLower() == value) == null);
        } else {
            return true;
        }
    }

    private void SelectNumber(string keyboardInput) {
        string newValue = this.value + keyboardInput;
        if(newValue.Length <= 20) {
            value = newValue;
            SetText();
        }
    }

    private void Submit() {
        if(value != null && value.Length > 0) {
            string userValue = value.ToLower();
            if(ValidateNewUser(userValue)) {
                bool result = InsertUser(userValue);
                if(result) {
                    SceneManager.LoadScene("Menu");
                }
            } else {
                StartCoroutine(ShowDialog());
            }
        }
    }
}