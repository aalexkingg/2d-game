using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public class General : MonoBehaviour
{
    // -------- Define all global variables here  --------

    public Text dayCountShadow;
    public Text dayCount;             // Defines text game object
    public GameObject backgroundDay;    // Defines day background game object
    public GameObject backgroundNight;  // Defines night background game object
    public GameObject nightOverlay;     // Defines night overlay game object

    public PauseMenu pauseMenu;         // References pause menu class under the variable 'pauseMenu'
    public TerrainGenerator terrain;    // Referneces terrain generator class under the variable 'terrain'

    public GameObject temp;

    // -- Private variables  --
    
    private float maxTime = 1200f;  // 1200s = 20mins


    // --  Public variables  --

    public float timeOfDay;             // Defines the current time of day
    public int dayNumber;               // Defines the day number
    [SerializeField] public static string worldName;     // Defines the world name
    [SerializeField] public static int seed;             // Defines the seed number
    [SerializeField] public static int difficulty;       // Defines the difficulty level


    // --------  Define all functions here  --------

    // Start is called before the first frame update
    void Start()
    {
        timeOfDay = 0;  // sets the current time at 0
        dayNumber = 1;  // Sets the day number as 1

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))   // Checks if the escape key as been pressed
        {
            pauseMenu.Pause();  // Pauses the game by calling the pause function
        }

        if (timeOfDay >= maxTime)   // Checks if the time of day is greater than or equal to the day length
        {
            // Transition from night to day
            timeOfDay = 0;                                  // Sets the time of day back to 0
            dayNumber++;                                    // Increments the day count
            backgroundDay.gameObject.SetActive(true);       // Unhides the day time background
            backgroundNight.gameObject.SetActive(false);    // Hides the night time background
            nightOverlay.gameObject.SetActive(false);       // Hides the night time overlay

        }
        else if (timeOfDay >= (maxTime / 2))
        {
            // Transition from day to night
            backgroundNight.gameObject.SetActive(true);     // Unhides the night time background
            nightOverlay.gameObject.SetActive(true);        // Unhides the night time overlay
            backgroundDay.gameObject.SetActive(false);      // Hides the dya time background
        }

        timeOfDay += Time.deltaTime;    // Increments time of the day
        dayCount.text = "Day: " + dayNumber;
        dayCountShadow.text = dayCount.text;


        if (Input.GetKeyDown(KeyCode.R))
        {
            CreateCreature();
        }

    }

    public void CreateCreature()
    {
        GameObject test = GameObject.Instantiate(temp);
    }

    // Called when the player creates a new world
    public static void SetData(string newName, int newSeed, int newDifficulty)
    {
        
        worldName = newName;        // Sets the world name
        seed = newSeed;             // Sets the seed number
        difficulty = newDifficulty; // Sets the difficulty level

        //ApplyData();


    }

    // Called when sending the data to other classes
    public void ApplyData()
    {
        //SaveSystem.SetName(worldName);
        terrain.SetSeed(seed);  // Sends the seed number to the terrain generator class
    }

    // Called when saving the general state
    public void SaveGeneral()
    {
        SaveSystem.SaveGeneral(this);   // Sends the data in this class to the save system class 
    }

    // Called when loading the data back into the class
    public void LoadGeneral()
    {
        GameData data = SaveSystem.LoadGeneral();   // Calls the load general function in the save system class and stores it in a game data object

        timeOfDay = data.timeOfDay;     // Sets the saved time of day to the current time of day
        dayNumber = data.dayNumber;     // Sets the saved day number to the current day number
        //seed = data.seed;
        //worldName = data.worldName;
        //difficulty = data.difficulty;

    }
}
