using UnityEngine;
using UnityEngine.UI;

public class SelectNumber : MonoBehaviour
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

    void Update()
    {

    }

    void selectNumber() {
        clearNumbers();
    }

    void addCLickListeners() {
        foreach(Button button in keyboard) {
            Text number = button.GetComponentInChildren<Text>();
            button.onClick.AddListener(() => selectNumber(number));
        }
    }

    private void selectNumber(Text number) {
        if(index == count) {
            MemoryController.SelectNumber = selected;
            MemoryController.CompareText();
            clearNumbers();
            index = 0;
        }

        selectedNumbers[index].text = number.text;
        selected += number.text;
        index++;
    }

    private void clearNumbers() {
        foreach(Text number in selectedNumbers) {
            number.text = "";
        }
        selected = "";
    }
}
