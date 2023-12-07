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

[System.Serializable]
public class GameData
{
    // --------  All game data defined here  --------

    public float timeOfDay;     // Time of day
    public int dayNumber;       // Day number
    public string worldName;    // World name         
    public int difficulty;      // Difficulty level


    // --------  Constrcutor  --------

    public GameData(General general)
    {
        // Stores the current time of day in variable
        timeOfDay = general.timeOfDay;

        // Stores the day number in variable
        dayNumber = general.dayNumber;

        //worldName = general.worldName;
        //difficulty = general.difficulty;

    }

}
