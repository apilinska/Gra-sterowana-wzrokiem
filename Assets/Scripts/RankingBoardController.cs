using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingBoardController : DbConnect
{
    public GameObject rank;
    public int count = 5;

    private List<RankingBoard> rankingData = new List<RankingBoard>();
    int scoreId = 0;

    void Start() {
        GetRankingData();
        CreateBoard();
    } 

    void GetRankingData() {
        this.scoreId = GetLastDexterityScore();
        rankingData = GetDexterityRankingBoard(count);
    }

    void CreateBoard() {
        for(int i=0; i < count; i++) {
            RankingBoard data = rankingData[i];
            if(data != null) {
                GameObject newObject = Instantiate(rank);
                newObject.transform.SetParent(this.transform, false);
                newObject.GetComponent<RankingValues>().SetValues(RankPlayer(i,data.user), data.score, this.scoreId == data.id);
            }
        }
    }

    private string RankPlayer(int index, string player) {
        return (index + 1).ToString() + ". " + player.ToUpper();
    }
}


