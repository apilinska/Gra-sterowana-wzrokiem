using UnityEngine;
using UnityEngine.UI;

public class BoardGenerator : MonoBehaviour
{
    [Header("Board sprites")]
    public GameObject number_box;
    public GameObject button_box_selected;
    public GameObject empty_box;

    [Header("Board settings")]
    [Range(5, 7)]
    public int board_size = 5;

    private GameObject[][] board;
    private int grid_size;

    void Start()
    {
        SetBoardSize();
        GenerateGrid();
    }

    bool IsEmptyBox(int i, int j) 
    {
        return ((i == 0 || i == grid_size-1) && (j == 0 || j == grid_size-1));
    }

    bool IsNumberBox(int i, int j) 
    {
        return ((i == 0 || i == grid_size-1 || j == 0 || j == grid_size-1) && !IsEmptyBox(i, j));
    }

    void AddEmptyBox(int i, int j) 
    {
        board[i][j] = CreateNewObject(empty_box);
    }

    void AddButtonBox(int i, int j) 
    {
        board[i][j] = CreateNewObject(button_box_selected);
    }

    void AddNumberBox(int i, int j) 
    {
        board[i][j] = CreateNewObject(number_box);
    }

    GameObject CreateNewObject(GameObject objectPrefab) 
    {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.transform.SetParent(this.transform, false);
        return newObject;
    }

    void SetBoardSize() 
    {
        grid_size = board_size + 2;
        float width = GetComponent<RectTransform>().rect.width;
        float height = GetComponent<RectTransform>().rect.height;        
        Vector2 cell_size = new Vector2(width/grid_size, height/grid_size);
        GetComponent<GridLayoutGroup>().cellSize = cell_size;

    }

    void GenerateGrid() 
    {
        for(int i = 0; i < grid_size; i++) 
        {
            for(int j = 0; j < grid_size; j++) 
            {
                if(IsEmptyBox(i,j)) 
                {
                    AddEmptyBox(i,j);
                }
                else if(IsNumberBox(i,j)) 
                {
                    AddNumberBox(i,j);
                }
                else 
                {
                    AddButtonBox(i,j);
                }
            }
        }
    }
}
