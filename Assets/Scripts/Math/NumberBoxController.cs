using UnityEngine;
using UnityEngine.UI;

public class NumberBoxController : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite box_selected;
    public Sprite box;

    private bool selected;
    private int number;
    private State state;
    private int i = 0;

    void Start()
    {
        UnselectButton();
    }

    public void SetCoordinates(State state, int i) 
    {
        this.state = state;
        this.i = i;
    }

    public void SetNumber(int number) 
    {
        this.number = number;
        GetComponentInChildren<Text>().text = number.ToString();
    }

    public int GetNumber() 
    {
        return this.number;
    }

    void SelectButton() 
    {
        selected = true;
        GetComponent<Image>().sprite = box_selected;
    }

    void UnselectButton() 
    {
        selected = false;
        GetComponent<Image>().sprite = box;
    }

    public bool ChangeState(int sum) 
    {
        bool previousState = selected;
        if(sum == number) 
        {
            SelectButton();
        } 
        else 
        {
            UnselectButton();
        }

        return (previousState ^ selected);
    }
}
