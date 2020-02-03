using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject memorizePanel;
    public GameObject selectPanel;

    [Header("Buttons")]
    public Button[] keyboard;

    [Header("Text")]
    public Text randomizedNumberText;
    public Text[] selectedNumbers;
    public Text timerText;

    [Header("Others")]
    public float timerStop = 3f;

    private int index = 0;
    private int count = 5;

    private int[] randomizedNumbers = new int[5];
    float timer = 0f;

    void Start()
    {
        randomizeNumber();
        //addCLickListeners();
    }

    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = Math.Round(timer, 1).ToString();
        if(timer >= timerStop) {
            randomizeNumber();
            timer = 0f;
        }
    }

    void randomizeNumber() {
        showPanel(memorizePanel);
        hidePanel(selectPanel);

        timerText.text = "";
        timer = 0f;
        randomizedNumberText.text = "";
        int random = 0;
        int lastRandom = 0;

        for(int n = 0; n < 5; n++) {
            random = getRandomNumber(9); 
            if(lastRandom != 0 && random == lastRandom) {
                do {
                    random = getRandomNumber(9);
                } while(random == lastRandom);
            }
            randomizedNumbers[n] = random;
            randomizedNumberText.text += " " + random.ToString();
            lastRandom = random;
        }
    }

    int getRandomNumber(int max) {
        return UnityEngine.Random.Range(0, max) + 1; /* [ 1 - max ] */
    }

    void selectNumber() {
        clearNumbers();
    }

    void showPanel(GameObject panel) {
        panel.gameObject.SetActive(true);
    }

    void hidePanel(GameObject panel) {
        panel.gameObject.SetActive(false);
    }

    void addCLickListeners() {
        foreach(Button button in keyboard) {
            Text number = button.GetComponentInChildren<Text>();
            button.onClick.AddListener(() => selectNumber(number));
        }
    }

    private void selectNumber(Text number) {
        if(index == count) {
            clearNumbers();
            index = 0;
        }

        selectedNumbers[index].text = number.text;
        index++;
    }

    private void clearNumbers() {
        foreach(Text number in selectedNumbers) {
            number.text = "";
        }
    }
}
