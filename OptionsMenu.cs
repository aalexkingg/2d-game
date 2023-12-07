using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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

    // Options menu needsto overlay game rather than switch to seperate game state otherwise world is lost

public class OptionsMenu : MonoBehaviour
{
    // -------- Define all global variables here  --------

    public GameObject pauseMenu;        // Defines pause menu overlay as game object
    public AudioMixer audioMixer;       // Defines variable as an AudioMixer object - Allows the program to change the volume level of the game

    public static bool inGame;    // Boolean variable which stores whether the player has entered the options menu from the main menu or not

    // --------  Define all functions here  --------

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // Checks if the user has presses the escape key
        {
            BackButton();   // Calls back button method
        }
    }

    // This function is called when the user changes the volume level in the options menu
    public void SetVolume(float volume)
    {
        // Changes the volume level of h=the audio mixer (audio mixer goes from -80 to 20, whereas volume bar goes from 0 to 100, so -80 is the conversion)
        audioMixer.SetFloat("volumeLevel", volume - 80); 
    }


    // This function is called when the user presses the back button
    public void BackButton()
    {
        if (inGame)
        {
            this.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");     // Loads the main menu scene
        }
        
        
    }
}
