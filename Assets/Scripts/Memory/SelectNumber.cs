using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectNumber : DbConnect
{
    [Header("Buttons")]
    public Button[] keyboard;

    [Header("Text")]
    public Text[] selectedNumbers;

    private int index = 0;
    private int count = 5;
    private string selected = "";
    private bool end_game = false;

    void Start()
    {
        addClickListeners();
    }

    void addClickListeners() {
        foreach(Button button in keyboard) {
            Text number = button.GetComponentInChildren<Text>();
            button.onClick.AddListener(() => selectNumber(number.text));
        }
    }

    private void setNumber(string number) 
    {
        selectedNumbers[index].text = number;
        selected += number;
        index++;
    }

    private void selectNumber(string number) 
    {
        if(index < count) {
            setNumber(number);
            if(index == count) {
                end_game = true;
                StartCoroutine(endGame());
            }
        }
    }

    private IEnumerator endGame() 
    {
        MemoryController.SelectNumber = selected;
        yield return new WaitForSeconds(1);
        if(MemoryController.IsLastGame()) {
            AddScore();
            SceneManager.LoadScene("MemoryResult");
        } else {
            MemoryController.NextGame();
            SceneManager.LoadScene("MemoryStart");
        }
    }

    private void AddScore() 
    {
        SetConnection();
        int score = MemoryController.CalculateScore();
        MemoryInsertScore(score);
    }

    public void MouseEnter(GameObject button) 
    {
        if(!end_game) {
            EyeCursor.On();
            StartCoroutine(loadButton(button));
        }
    }

    public void MouseExit() 
    {
        if(!end_game) {
            EyeCursor.Off();
            StopAllCoroutines();
        }
    }

    private IEnumerator loadButton(GameObject button) 
    {
        yield return new WaitForSeconds(EyeCursor.Time());
        if(EyeCursor.IsFocused()) {
            EyeCursor.Off();
            Text number = button.GetComponentInChildren<Text>();
            selectNumber(number.text);
        }
    }
}
