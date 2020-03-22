using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BoardRows
{
    public GameObject[] cols;
}

[System.Serializable]
public class Numbers
{
    public GameObject cell_1;
    public GameObject cell_2;
}
 
public class BoardController : MonoBehaviour
{
    [Header("Board")]
    public BoardRows[] board_rows;
    public Numbers[] number_rows;
    public Numbers[] number_cols;


    void Start()
    {
        for(int i=0; i < board_rows.Length; i++) {
            for(int j=0; j < board_rows[i].cols.Length; j++) {
                int rand = GetRandomNumber();
                GameObject obj = board_rows[i].cols[j];
                ButtonBoxController(obj).SetNumber(rand);
                ButtonBoxController(obj).SetCoordinates(i,j);
            }
        }

        SumRows();
        SumColumns();
    }

    int GetRandomNumber(int max = 9) {
        return UnityEngine.Random.Range(0, max) + 1; /* [ 1 - max ] */
    }

    void SumColumns() {
        for(int i=0; i < number_cols.Length; i++) {
            int col_sum = 0;
            for(int j=0; j < board_rows.Length; j++) {
                col_sum += ButtonBoxController(board_rows[j].cols[i]).GetNumber();
            }
            SetText(number_cols[i].cell_1, col_sum);
            SetText(number_cols[i].cell_2, col_sum);
        }
    }

    void SumRows() {
        for(int i=0; i < number_rows.Length; i++) {
            int row_sum = 0;
            for(int j=0; j < board_rows[i].cols.Length; j++) {
                row_sum += ButtonBoxController(board_rows[i].cols[j]).GetNumber();
            }
            SetText(number_rows[i].cell_1, row_sum);
            SetText(number_rows[i].cell_2, row_sum);
        }
    }

    void SetText(GameObject obj, int number) {
        obj.GetComponentInChildren<Text>().text = number.ToString();
    }

    public void ChangeState(int i, int j) {
        Debug.Log(i + "," + j);
    }

    ButtonBoxController ButtonBoxController(GameObject obj) {
        return obj.GetComponent<ButtonBoxController>();
    }

    void crossRefresh() {
        
    }
}
