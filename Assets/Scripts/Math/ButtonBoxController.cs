using UnityEngine;
using UnityEngine.UI;

public class ButtonBoxController : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite box_selected;
    public Sprite box;

    private bool selected;
    private int i = 0;
    private int j = 0;
    private int number;

    void Start()
    {
        SelectButton();
        GetComponent<Button>().onClick.AddListener(() => ChangeState());
    }

    public int GetNumber() 
    {
        return this.number;
    }

    private BoardController GetController() 
    {
        return GetComponentInParent<BoardController>();
    }

    public int GetNumberIfSelected() 
    {
        if(selected) return number;
        else return 0;
    }

    public void SetCoordinates(int i, int j) 
    {
        this.i = i;
        this.j = j;
    }

    public void SetNumber(int number) 
    {
        this.number = number;
        GetComponentInChildren<Text>().text = number.ToString();
    }

    public void SetNumberAndCoordinates(int number, int i, int j) 
    {
        SetCoordinates(i, j);
        SetNumber(number);
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

    public void ChangeState() 
    {
        MathController.NewMove();
        if(selected) UnselectButton();
        else SelectButton();
        GetComponentInParent<BoardController>().ChangeState(i,j);
    }

    public void MouseEnter() 
    {
        var boardController = GetController();
        if(boardController != null) 
        {
            boardController.MouseEnter(this.i, this.j);
        }
    }

    public void MouseExit() 
    {
        var boardController = GetController();
        if(boardController != null) 
        {
            boardController.MouseExit();
        }
    }
}
