using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*      Program Rules:
 *      
 * variable names begin with lower case and use camel case
 * function names begin with upper case and use camel case
 * class names being with upper case and use camel case
 * Comment off sections (defining variable, defining function, constructors etc)
 * Use meaningful varible and function names
 * Don't use long names
 * Use indentations
 * 
 */

public class PauseMenu : MonoBehaviour
{
    // --------  Define all global variables here  --------

    public static bool paused = false;     // Defines paused as a boolean - States whether the game is currently paused

    public GameObject optionsMenu;  // Defines options menu as a game object

    // --------  Define all functions here  --------

    // Called when game needs to be paused or unpaused
    public void Pause()
    {
        if (!paused)    // Checks if game is not paused
        {
            Debug.Log("paused");
            this.gameObject.SetActive(true);        // Unhides pause menu
            paused = true;                          // Sets paused variable to true
            Time.timeScale = 0;                     // Sets time scale to 0
            OptionsMenu.inGame = true;              // Flags that game is paused from in game
        }
        else
        {
            Debug.Log("unpaused");                  
            this.gameObject.SetActive(false);  // Hides pause menu
            paused = false;                         // Sets paused variable to false
            Time.timeScale = 1;                     // Sets time scale to 1
        }
    }

    // Called when the user presses the 'Options' button in the pause menu
    public void Options()
    {
        // Loads the options scene
        //OptionsMenu.fromMainMenu = false;
        optionsMenu.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    // Called when the user presses the 'Save and Quit' button in the pause menu
    public void SaveAndQuit()
    {
        // Loads the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
