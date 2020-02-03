using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ButtonName 
{
	Initialize,
    StartGame,
    Menu,
    Memory
}

public class SceneManagment : MonoBehaviour 
{
	public Buttons[] buttons;

	void Start () 
	{
		foreach(Buttons b in buttons)
		{
			b.buttonItem.onClick.AddListener(() => { OpenScene(b.buttonName); });
		}
	}

	void OpenScene(ButtonName name)
	{
		switch(name)
		{
			case ButtonName.Initialize:
				SceneManager.LoadScene("Initialize");
				break;

            case ButtonName.StartGame:
				SceneManager.LoadScene("Start game");
				break;

            case ButtonName.Menu:
				SceneManager.LoadScene("Menu");
				break;

            case ButtonName.Memory:
				SceneManager.LoadScene("Memory");
				break;

			default:
				Debug.Log("Error");
				break;
		}
	}
}

[Serializable]
public class Buttons
{
	//public string name = "Button";
	public ButtonName buttonName;
	public Button buttonItem;
}