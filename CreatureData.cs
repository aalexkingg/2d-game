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
public class CreatureData
{
    // -------- Define all global variables here  --------

    public Vector3 position;    // Position
    public int health;          // Health

    // --------  Constructor  --------

    public CreatureData (Creature npc)
    {
        // Stores creatures position in position variable
        position = new Vector3(npc.transform.position.x, npc.transform.position.y, npc.transform.position.z);

        // Stores creatures current health in health variable
        
    }
}
