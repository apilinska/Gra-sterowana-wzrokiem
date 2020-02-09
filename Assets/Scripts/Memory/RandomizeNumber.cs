using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomizeNumber : MonoBehaviour
{
    [Header("Text")]
    public Text randomizedNumberText;
    public Text timerText;

    [Header("Others")]
    public float timerStop = 3f;
    public int numberLength = 5;

    private int index = 0;
    private int count = 5;
    private float timer = 0f;

    void Start()
    {
        randomizeNumber();
    }

    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = Math.Round(timer, 1).ToString();
        if(timer >= timerStop) {
            SceneManager.LoadScene("MemorySelect");
        }
    }

    void randomizeNumber() {

        timerText.text = "";
        timer = 0f;
        randomizedNumberText.text = "";
        string randomText = "";
        int random = 0;
        int lastRandom = 0;

        for(int n = 0; n < numberLength; n++) {
            random = getRandomNumber(); 
            if(lastRandom != 0 && random == lastRandom) {
                do {
                    random = getRandomNumber();
                } while(random == lastRandom);
            }
            lastRandom = random;
            randomizedNumberText.text += " " + random.ToString();
            randomText += random.ToString();
        }
        MemoryController.RandomizeNumber = randomText;
    }

    int getRandomNumber(int max = 9) {
        return UnityEngine.Random.Range(0, max) + 1; /* [ 1 - max ] */
    }
}
