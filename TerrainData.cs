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
public class TerrainData
{
    // --------  All terrain data defined here  --------

    public int seed;        // Seed
    public List<int> mapArray = new List<int>();      // Map list
    public int x;
    public int y;


    // --------  Constructor  --------

    public TerrainData (TerrainGenerator terrain)
    {
        // Stores terrain seed in seed variable
        seed = terrain.seed;

        // Stores terrain map array in map variable
        mapArray = terrain.modifiedBlocks;

        
    }

}
