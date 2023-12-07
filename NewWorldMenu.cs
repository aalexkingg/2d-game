using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;

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

public class NewWorldMenu : MonoBehaviour
{
    // -------- Define all global variables here  --------

    public GameObject playMenu;         // Defines the player menu as a ame object
    public GameObject newWorldMenu;     // Defines new world menu as a game object
    public GameObject background;       // Defines background as a game object
    public GameObject errorText;        // Defines error text as a game object

    public InputField inputWorld;       // References the world name input field in the mneu
    public InputField inputSeed;        // Referneces the seed number input field in the menu

    public Button easy;     // References the easy difficulty button in the menu
    public Button normal;   // References the nornal difficulty button in the menu
    public Button hard;     // Referneces the hard difficulty button in the menu

    // --  Private variables  --

    private string worldName;       // Defines the world name (default as empty until it is fetched from input field)
    private int seed = 0;           // Defines the seed number (default as 0)
    private int difficulty = 2;     // defines the difficulty (default as 0)

    // -- Public variables  --



    // --------  Define all functions here  --------

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // Checks if the escape key has been pressed
        {
            newWorldMenu.gameObject.SetActive(false);   // Hides the new world menu
            background.gameObject.SetActive(false);     // Hides the menu background
            playMenu.gameObject.SetActive(true);        // Unhides the play menu

        }

        
    }

    // Called when getting the world name from the input field
    public string GetWorldName()
    {
        return inputWorld.text; // Returns the text currently inside the input field
    }

    // Called when getting the seed number from the input field
    public int GetSeed()
    {
        if (inputSeed.text != "")   // Checks if input field is empty
        {
            return int.Parse(inputSeed.text);   // Returns value currently in the input field
        }
        else
        {
            return 0;   // Returns 0
        }
        
        
    }

    // Called when setting difficlty to easy
    public void setEasy() { difficulty = 1; }   // Sets difficulty to 1 (easy)

    // Called whe s etting difficulty to normal
    public void setNormal() { difficulty = 2; } // Sets difficulty to 2 (normal)

    // Called when setting difficulty to hard
    public void setHard() { difficulty = 3; }   // Sets difficulty to 3 (hard)

    // Called when the user presses the 'Create World' button
    public void CreateWorld()
    {
        worldName = GetWorldName(); // gets the world name from the input field
        seed = GetSeed();           // gets the seed number from the input field
        Debug.Log("w "+worldName+" s "+seed+" d "+difficulty);

        if (ValidateWorldName(worldName))   // Validates world name before sending it off
        {
            General.SetData(worldName, seed, difficulty);   // Parses world name, seed number and difficulty into the general class

            SceneManager.LoadScene("Playing");  // Loads the playing scene

        }
        else
        {
            errorText.SetActive(true);    // Unhides error text due to invalid world name
        }

        
    }

    // Called when validating world name
    private bool ValidateWorldName(string name)
    {
        // Defines the save path of possible pre-existing file
        string savePath = "C:/Users/alex/Documents/Unity Projects/Project 1/Assets/Saves/" + name + "/player.json";

        // Name CANNOT contain symbols, only number and letters (alpha numeric)

        if (!File.Exists(savePath))     // checks if file doesnt exist
        {
            return true;    // Returns true (file is valid)
        }
        else
        {
            return false;   // Returns false (file is invalid)
        }
    }
}
