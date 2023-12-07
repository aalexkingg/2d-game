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
public class PlayerData
{
    // --------  All player data defined here  --------

    public Vector3 position;    // Position
    public int health;          // Health


    // --------  Constrcutor  --------

    public PlayerData (Player player)
    {
        // Stores players position in position variable
        position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        // Stores players current health in health variable
        health = player.currentHealth;
    }
    
}
