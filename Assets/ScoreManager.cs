using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // use TMP

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0; // player score
    public int enemyScore = 0; // enemy score

    public TMP_Text playerScoreTMP; // put player score here
    public TMP_Text enemyScoreTMP; // put enemy score here

    public GameObject winPanel;
    public AudioSource backgroundMusic;
    public AudioSource winSound;


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
    public void WinGame()
    {
        Debug.Log("Boss Defeated! You Win!");
        // Activate Win UI (if you have one)
        winPanel.SetActive(true);
        winSound.Play();
        backgroundMusic.Stop();
        Time.timeScale = 0; // Pause game
    }

}
