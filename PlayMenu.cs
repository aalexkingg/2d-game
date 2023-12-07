using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class PlayMenu : MonoBehaviour
{
    // -------- Define all global variables here  --------

    public GameObject playMenu;     // Defines play menu as a game object
    public GameObject mainMenu;     // Defines main menu as a game object

    // --------  Define all functions here  --------

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // Checks if the user has pressed the escape key
        {
            mainMenu.gameObject.SetActive(true);    // Unhides the main menu
            playMenu.gameObject.SetActive(false);   // Hides the play menu

        }
    }
}
