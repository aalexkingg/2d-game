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

public class RespawnMenu : MonoBehaviour
{
    // -------- Define all global variables here  --------



    // --------  Define all functions here  --------

    // This function is called when the user presses the 'Respawn' button in the respawn menu
    public void Respawn()
    {
        // Loads the playing scene
        SceneManager.LoadScene("Playing");
    }

    // This function is called when the user presses the 'MainMenu' button in the respawn menu
    public void MainMenu()
    {
        // Loads the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
