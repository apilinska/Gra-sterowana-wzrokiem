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

    void Start()
    {
        addCLickListeners();
    }

    void addCLickListeners() {
        foreach(Button button in keyboard) {
            Text number = button.GetComponentInChildren<Text>();
            button.onClick.AddListener(() => selectNumber(number.text));
        }
    }

    private void setNumber(string number) {
        selectedNumbers[index].text = number;
        selected += number;
        index++;
    }

    private void selectNumber(string number) {
        if(index < count) {
            setNumber(number);
            if(index == count) {
                StartCoroutine(endGame());
            }
        }
    }

    private IEnumerator endGame() {
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

    private void AddScore() {
        SetConnection();
        int score = MemoryController.CalculateScore();
        MemoryInsertScore(score);
    }
}
