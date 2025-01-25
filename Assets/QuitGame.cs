using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // quit game
    public void Quit()
    {
        // cheking if the game really quits in editor
        Debug.Log("game quitting...");
        Application.Quit();
    }
}
