using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Range(5, 7)]
    public int board_size = 5;
    public BoardRows[] board_rows;
    public Numbers[] number_rows;
    public Numbers[] number_cols;

    private Board board;
    private bool[] row_selected;
    private bool[] col_selected;
    private float timer = 0f;
    private bool timerStop = false;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if(!timerStop) {
            timer += Time.deltaTime;
        }
    }

    void Initialize() {
        MathController.ClearNumberOfMoves();
        board = MathController.GetBoard_5x5_2();

        for(int i = 0; i < board_rows.Length; i++) {
            for(int j = 0; j < board_rows[i].cols.Length; j++) {
                int num = board.numbers[i,j].number;
                GameObject obj = board_rows[i].cols[j];
                ButtonBoxController(obj).SetNumberAndCoordinates(num,i,j);
            }
        }
        SetColumns(board.col_numbers);
        SetRows(board.row_numbers);
    }

    void SetRows(int[] numbers) {
        row_selected = new bool[number_rows.Length];
        for(int i = 0; i < number_rows.Length; i++) {
            int num = numbers[i];
            row_selected[i] = false;
            SetNumberText(number_rows[i], num);
        }
    }

    void SetColumns(int[] numbers) {
        col_selected = new bool[number_cols.Length];
        for(int j = 0; j < number_cols.Length; j++) {
            int num = numbers[j];
            col_selected[j] = false;
            SetNumberText(number_cols[j], num);
        }
    }

    void SetNumberText(Numbers numbers, int number) {
        GameObject cell1 = numbers.cell_1;
        NumberBoxController(cell1).SetNumber(number);

        GameObject cell2 = numbers.cell_2;
        NumberBoxController(cell2).SetNumber(number);
    }

    int SumRow(int i) {
        int sum = 0;
        for(int j=0; j < board.numbers.GetLength(1); j++) {
            sum += (board.numbers[i,j].selected ? board.numbers[i,j].number : 0);
        }
        return sum;
    }

    int SumColumn(int j) {
        int sum = 0;
        for(int i=0; i < board.numbers.GetLength(0); i++) {
            sum += (board.numbers[i,j].selected ? board.numbers[i,j].number : 0);
        }
        return sum;
    }

    bool FindUnselected() {
        foreach(bool selected in row_selected) {
            if(!selected) return true;
        }
        foreach(bool selected in col_selected) {
            if(!selected) return true;
        }
        return false;
    }

    void Calculate() {
        if(!FindUnselected()) {
            timerStop = true;
            MathController.SetTimer(Math.Round(timer, 0));
            SceneManager.LoadScene("MathResult");
        }
    }

    public void ChangeState(int i, int j) {
        Cell cell = board.numbers[i,j];
        cell.selected = !cell.selected;
        int row_sum = SumRow(i);
        int col_sum = SumColumn(j);
        bool rowChangeState = ChangeStateOfRow(i, row_sum);
        bool colChangeState = ChangeStateOfColumn(j, col_sum);
        if(rowChangeState || colChangeState) {
            Calculate();
        }
    }

    bool ChangeStateOfRow(int i, int sum) {
        Numbers rows = number_rows[i];
        bool changedStatusCell1;
        bool changedStatusCell2;

        GameObject row1 = rows.cell_1;
        changedStatusCell1 = NumberBoxController(row1).ChangeState(sum);

        GameObject row2 = rows.cell_2;
        changedStatusCell2 = NumberBoxController(row2).ChangeState(sum);

        if(changedStatusCell1 || changedStatusCell2) {
            row_selected[i] = !row_selected[i];
            return true;
        }
        else return false;
    }

    bool ChangeStateOfColumn(int j, int sum) {
        Numbers cols = number_cols[j];
        bool changedStatusCell1;
        bool changedStatusCell2;

        GameObject col1 = cols.cell_1;
        changedStatusCell1 = NumberBoxController(col1).ChangeState(sum);

        GameObject col2 = cols.cell_2;
        changedStatusCell2 = NumberBoxController(col2).ChangeState(sum);

        if(changedStatusCell1 || changedStatusCell2) {
            col_selected[j] = !col_selected[j];
            return true;
        }
        else return false;
    }

    ButtonBoxController ButtonBoxController(GameObject obj) {
        return obj.GetComponent<ButtonBoxController>();
    }

    NumberBoxController NumberBoxController(GameObject obj) {
        return obj.GetComponent<NumberBoxController>();
    }
}
