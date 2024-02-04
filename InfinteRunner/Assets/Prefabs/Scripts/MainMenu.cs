using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Allows the player to select "play" in the main menu.
    public void PlayGame()
    {
        //Ensure that MainMenu is set as 0 in project build settings and game is set as 1.
        //Using Unity's Scene manager to change scene from menus to gameplay.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //In the MainMenu allows player to quit the game.
    public void QuitGame()
    {
        //For testing when in editor "Quit Game" will appear in log when button is pressed.
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

