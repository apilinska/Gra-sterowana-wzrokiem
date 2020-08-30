using System.Collections;
using System.Collections.Generic;
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

        foreach(Button button in buttons) 
        {
            Text number = button.GetComponentInChildren<Text>();
            button.onClick.AddListener(() => SelectNumber(number.text));
        }
    }

    private void SetText() 
    {
        input.text = this.value;
    }

    private void GetUsersFromDatabase() 
    {
        users = GetUsers();
    }

    private bool dialogIsHidden() 
    {
        return !dialog.gameObject.activeSelf;
    }

    private void Clear() 
    {
        this.value = "";
        SetText();
    }

    private void Delete() 
    {
        if(this.value.Length > 0) 
        {
            this.value = this.value.Substring(0, this.value.Length - 1);
            SetText();
        }
    }

    public void MouseEnter(string input, KeyType type) 
    {
        if(dialogIsHidden()) {
            EyeCursor.On();
            StartCoroutine(loadButton(input, type));
        }
    }

    public void MouseExit() 
    {
        if(dialogIsHidden()) 
        {
            EyeCursor.Off();
            StopAllCoroutines();
        }
    }

    private IEnumerator ShowDialog() 
    {
        dialog.GetComponentInChildren<Text>().text = "Istnieje już użytkownik o podanej nazwie";
        dialog.SetActive(true);
        yield return new WaitForSeconds(dialogCloseTimer);
        dialog.SetActive(false);
    }

    private bool ValidateNewUser(string userName) 
    {
        if(users != null && users.Count > 0) 
        {
            return (users.Find(x => x.name.ToLower() == userName) == null);
        } else {
            return true;
        }
    }

    private void SelectNumber(string keyboardInput) 
    {
        string newValue = this.value + keyboardInput;
        if(newValue.Length <= 20) 
        {
            this.value = newValue;
            SetText();
        }
    }

    private void Submit() {
        if(this.value != null && this.value.Length > 0) 
        {
            string userValue = this.value.ToLower();
            if(ValidateNewUser(userValue)) 
            {
                bool result = InsertUser(userValue);
                if(result) {
                    SceneManager.LoadScene("Menu");
                }
            } 
            else 
            {
                StartCoroutine(ShowDialog());
            }
        }
    }
    
	private IEnumerator loadButton(string input, KeyType type) 
    {
        yield return new WaitForSeconds(EyeCursor.Time());
		if(EyeCursor.IsFocused())
        {
			EyeCursor.Off();
            switch(type) 
            {
                case KeyType.Char:
                    SelectNumber(input);
                    break;

                case KeyType.Enter:
                    Submit();
                    break;

                case KeyType.Delete:
                    Delete();
                    break;

                case KeyType.Clear:
                    Clear();
                    break;
            }
		}
    }
}