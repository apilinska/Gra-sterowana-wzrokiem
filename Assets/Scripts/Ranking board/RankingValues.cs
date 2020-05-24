using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingValues : MonoBehaviour
{
    public Text player;
    public Text score;

    private Color active_color = Color.cyan;

    public void SetValues(string player, int score, bool active = false)
    {
        if(active) {
            this.player.color = active_color;
            this.score.color = active_color;
        }

        this.player.text = player;
        this.score.text = score.ToString();
    }

}
