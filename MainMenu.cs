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

public class MainMenu : MonoBehaviour
{
    // --------  Define all global variables here  --------



    // --------  Define all functions here  --------

    // This function is called when the user presses the 'Options' button in the main menu
    public void options()
    {
        OptionsMenu.inGame = false;             // Sets this variable to true so that the options menu knows the user has enetered it from the main menu
        SceneManager.LoadScene("OptionsMenu");  // Loads the options menu scene
    }

    // This function is called when the user presses the 'Quit' button in the main menu
    public void quitGame()
    {
        Debug.Log("Quit!");     // Logs 'Quit!' to the console
        Application.Quit();     // Quits the game
    }
}
