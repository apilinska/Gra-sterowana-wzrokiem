using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyboardController : DbConnect
{
    public Text input;
    public Button deleteBtn;
    public Button enterBtn;
    public Button[] buttons;

    private string value = "";


    void Start()
    {
        deleteBtn.onClick.AddListener(() => Delete());
        enterBtn.onClick.AddListener(() => Submit());

        foreach(Button button in buttons) {
            Text number = button.GetComponentInChildren<Text>();
            button.onClick.AddListener(() => SelectNumber(number.text));
        }
    }

    private void SetText() {
        input.text = value;
    }

    private void Delete() {
        if(value.Length > 0) {
            value = value.Substring(0,value.Length - 1);
            SetText();
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
            value = value.ToLower();
            bool result = InsertUser(value);
            if(result) {
                //SetActiveUser();
                SceneManager.LoadScene("Menu");
            }
        }
    }
}