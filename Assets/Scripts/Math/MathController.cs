using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathController : MonoBehaviour
{
   
   public static int[,] GetGameNumbers_5x5() {
       int[,] board = new int[5,5];
       return board;
   }

   public static void Sum() {
       int[,] board = GetGameNumbers_5x5();
    //    for(int i = 0; i < board.Length; i++) {
    //        for(int j = 0; j < board[0].Length; j++) {

    //        }
    //    }
   }

}
