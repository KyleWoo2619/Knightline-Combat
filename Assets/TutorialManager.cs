using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialUI; // Assign the tutorial UI panel in Inspector

    void Start()
    {
        // Pause the game at the beginning
        Time.timeScale = 0;
        tutorialUI.SetActive(true); // Show tutorial UI
    }

    public void StartGame()
    {
        tutorialUI.SetActive(false); // Hide tutorial UI
        Time.timeScale = 1; // Resume the game
    }
}
