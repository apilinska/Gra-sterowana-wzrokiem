using UnityEngine;

[System.Serializable]
public class Cell 
{
    public int number;
    public bool selected;

    public Cell(int n) 
    {
        number = n;
        selected = true;
    }
}

[System.Serializable]
public class Board
{
    public static int size;
    public Cell[,] numbers;
    public int[] row_numbers;
    public int[] col_numbers;

    public Board(int board_size) 
    {
        size = board_size;
    }
}

public class MathController : MonoBehaviour
{
    public static int number_of_moves = 0;
    public static int game_timer = 0;

    private static int movesWeight = 5;
    private static int timeWeight = 3;

    public static void NewMove() 
    {
        number_of_moves++;
    }

    public static int NumberOfMoves() 
    {
        return number_of_moves;
    }

    public static void ClearNumberOfMoves() 
    {
        number_of_moves = 0;
    }

    public static void SetTimer(double timer) 
    {
        game_timer = (int)timer;
    }

    public static string GetResult() 
    {
        return CalculateResult().ToString();
    } 

    public static int CalculateResult() 
    {
        return (number_of_moves != 0 && game_timer != 0) ? (100 / number_of_moves + 100 / game_timer) : 0;
    }

    private static int GetRandomGame(int max = 5) 
    {
        return UnityEngine.Random.Range(0, max) + 1; /* [ 1 - max ] */
    }

    private static Cell[,] GetBoardNumbers(int[,] numbers, int size) 
    {
        Cell[,] cellNumbers = new Cell[size, size]; 
        for(int i = 0; i < size; i++) 
        {
            for(int j = 0; j < size; j++) 
            {
                cellNumbers[i,j] = new Cell(numbers[i,j]);
            }
        }
        return cellNumbers;
    }

    public static Board GetBoard_5x5() 
    {
        Board board = new Board(5);
        int[,] numbers = new int[5,5];

        int gameNumber = GetRandomGame(5);
        switch(gameNumber) 
        {
            case 1:
                numbers = new int[5,5] 
                {
                    { 4,6,2,6,9 },
                    { 7,8,8,1,6 },
                    { 4,1,8,9,5 },
                    { 5,9,2,4,6 },
                    { 1,9,9,4,3 }
                }; 
                board.row_numbers = new int[5]{ 18,9,26,14,14 };
                board.col_numbers = new int[5]{ 14,15,27,10,5 };
                break;

            case 2:
                numbers = new int[5,5] 
                {
                    { 7,4,6,2,7 },
                    { 8,8,2,5,6 },
                    { 5,1,9,1,5 },
                    { 3,3,1,7,9 },
                    { 2,3,3,2,3 }
                }; 
                board.row_numbers = new int[5]{ 10,23,14,19,10 };
                board.col_numbers = new int[5]{ 15,18,17,14,12 };
                break;

            case 3:
                numbers = new int[5,5] 
                {
                    { 8,8,5,8,4 },
                    { 9,5,1,1,1 },
                    { 2,5,3,7,8 },
                    { 6,8,6,5,8 },
                    { 2,6,8,3,4 }
                }; 
                board.row_numbers = new int[5]{ 8,12,20,19,15 };
                board.col_numbers = new int[5]{ 19,0,18,16,21 };
                break;

            case 4:
                numbers = new int[5,5] 
                {
                    { 1,7,1,2,9 },
                    { 3,1,8,2,3 },
                    { 7,6,3,7,2 },
                    { 9,9,4,2,5 },
                    { 5,7,2,1,1 }
                }; 
                board.row_numbers = new int[5]{ 17,16,14,7,15 };
                board.col_numbers = new int[5]{ 16,14,10,12,17 };
                break;

            case 5:
                numbers = new int[5,5] 
                {
                    { 8,3,6,3,6 },
                    { 3,9,4,4,2 },
                    { 4,8,5,5,1 },
                    { 4,4,8,8,7 },
                    { 8,6,1,8,9 }
                }; 
                board.row_numbers = new int[5]{ 14,11,13,23,23 };
                board.col_numbers = new int[5]{ 15,21,9,23,16 };
                break;
        }

        board.numbers = GetBoardNumbers(numbers, 5);
        return board;
    }
}
