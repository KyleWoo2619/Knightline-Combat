using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // use TMP

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0; // player score
    public int enemyScore = 0; // enemy score

    [Header("Player ")]
    public TMP_Text playerScoreTMP; // put player score here
    public TMP_Text enemyScoreTMP; // put enemy score here

    // increase player score
    public void AddPlayerScore()
    {
        playerScore++;
        UpdateScoreUI();
    }

    // increase enemy score
    public void AddEnemyScore()
    {
        enemyScore++;
        UpdateScoreUI();
    }

    // update score
    void UpdateScoreUI()
    {
        if (playerScoreTMP != null)
        {
            playerScoreTMP.text = "Player: " + playerScore;
        }

        if (enemyScoreTMP != null)
        {
            enemyScoreTMP.text = "Enemy: " + enemyScore;
        }
    }
}
