using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ButtonName 
{
	Initialize,
    MemoryStartMenu,
	VisionStartMenu,
	DexterityStartMenu,
	AttentionStartMenu,
    Menu,
    StartMemory,
	StartVision,
	StartDexterity,
	StartAttention,
	StartMath,
	MathStartMenu
}

public class SceneManagment : MonoBehaviour 
{
	public Buttons[] buttons;

	void Start() 
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

			case ButtonName.Menu:
				SceneManager.LoadScene("Menu");
				break;

            case ButtonName.MemoryStartMenu:
				SceneManager.LoadScene("MemoryStartMenu");
				break;

			case ButtonName.DexterityStartMenu:
				SceneManager.LoadScene("DexterityStartMenu");
				break;

			case ButtonName.VisionStartMenu:
				SceneManager.LoadScene("VisionStartMenu");
				break;

            case ButtonName.StartMemory:
				MemoryController.ClearResult();
				SceneManager.LoadScene("MemoryStart");
				break;

			case ButtonName.StartDexterity:
				MemoryController.ClearResult();
				SceneManager.LoadScene("DexterityGame");
				break;

			case ButtonName.MathStartMenu:
				SceneManager.LoadScene("MathStartMenu");
				break;

			case ButtonName.StartMath:
				SceneManager.LoadScene("MathGame");
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