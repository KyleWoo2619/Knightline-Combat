using System.Collections;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 60f; // Total game time in seconds
    private float timeRemaining;
    private bool gameEnded = false;

    [Header("UI Elements")]
    public TMP_Text timerText;
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Score Manager Reference")]
    public ScoreManager scoreManager;

    [Header("Game Objects to Disable on Timeout")]
    public GameObject player;
    public GameObject enemy;

    [Header("Audio Sources")]
    public AudioSource backgroundMusic;  // Assign Game BGM object
    public AudioSource winSound;         // Assign Win Sound
    public AudioSource loseSound;        // Assign Lose Sound

    void Start()
    {
        timeRemaining = gameDuration;
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
            yield return null;
        }

        EndGame();
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(timeRemaining).ToString(); // Display whole number
    }

    void EndGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        // Stop Player & Enemy Movement
        if (player != null) player.SetActive(false);
        if (enemy != null) enemy.SetActive(false);

        // Stop background music
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        // Determine Win/Lose
        if (scoreManager.playerScore > scoreManager.enemyScore)
        {
            winPanel.SetActive(true);
            if (winSound != null) winSound.Play();
        }
        else
        {
            losePanel.SetActive(true);
            if (loseSound != null) loseSound.Play();
        }

        // Pause the game
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
