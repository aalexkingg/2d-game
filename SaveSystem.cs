using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
 [Serializable]
public static class SaveSystem
{
    // -------- Define all global variables here  --------

    // Each save path needs to have world name entered before .json file

        // This save path needs to be made generic

    // Defines the basic save path
    private static string savePath = "C:/Users/alex/Documents/Unity Projects/Project 1/Assets/Saves/" + General.worldName;

    private static string PlayerSavePath = savePath + "/player.json";       // Defines player save path
    private static string CreatureSavePath = savePath + "/creature.json";   // Defines creature save path
    private static string TerrainSavePath = savePath + "/terrain.json";     // Defines terrain save path
    private static string GameGeneralSavePath = savePath + "/general.json"; // Defines game general save path


    // --------  Define all functions here  --------

    // ------------- Save Player ---------------------------------------------

    // Called when creating and saving player data to a .json file
    public static void SavePlayer(Player player)
    {
        System.IO.Directory.CreateDirectory(savePath);      // Creates the file with the world name as the name

        PlayerData playerData = new PlayerData(player);     // Creates a new player data object which stores the players current data

        string json = JsonUtility.ToJson(playerData);       // Converts the player data to a json formatted string

        File.WriteAllText(PlayerSavePath, json);            // Writes the string to a file
    }

    // Called when Reading the saved data from the file
    public static PlayerData LoadPlayer()
    {
        if (File.Exists(PlayerSavePath))    // Checks if the file exists
        {
            string json = File.ReadAllText(PlayerSavePath);     // Reads from the file and stores it in a json formatted string

            PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);   // Converts string to player data object

            return loadedPlayerData;    // Returns player data object
        }
        else
        {
            Debug.LogError("Player file not found");    // Logs error to the console
            return null;    // Returns null
        }

    }

    // ---------------------- Save Creature ---------------------------------------------

    // Called when creating and saving creature data to a .json file
    public static void SaveCreature (Creature creature)
    {
        CreatureData creatureData = new CreatureData(creature); // Creates a new creature data object which stores the creatures current data

        string json = JsonUtility.ToJson(creatureData);         // Converts the creature data to a json formatted string

        File.WriteAllText(CreatureSavePath, json);              // Writes the string to a file
    }

    // Called when Reading the saved data from the file
    public static CreatureData LoadCreature ()
    {
        if (File.Exists(CreatureSavePath))  // Checks if the file exists
        {
            string json = File.ReadAllText(CreatureSavePath);   // Reads from the file and stores it in a json formatted string

            CreatureData loadedCreatureData = JsonUtility.FromJson<CreatureData>(json); // Converts string to creature data object

            return loadedCreatureData;  // Returns creature data object
        }
        else
        {
            Debug.LogError("NPC file not found");   // Logs error to the console
            return null;    // Returns null
        }
    }

    // --------------------- Save Terrain -------------------------------------------------
    
    // Called when creating and saving terrain data to a .json file
    public static void SaveTerrain (TerrainGenerator terrain)
    {
        TerrainData terrainData = new TerrainData(terrain); // Creates a new terrain data object which stores the terrain current data

        // JsonUtility does not support arrays. Need to find linear storage method

        // Use 2 for loops to traverse each element, each time storing the element at that
        // position in possibly a list

        // Store seed, and seperate array/list which stores user modified blocks (store position of modified block and integer value of block)
        /*
        string json = "{\"seed\":"+ General.seed + ",\"mapArray\":{";      // Converts the terrain data to a json formatted string

        for (int i = 0; i < terrain.modifiedBlocks.Count-3; i += 3)
        {
            json += "{\"x\":" + terrain.modifiedBlocks[i] + ",\"y\":" + terrain.modifiedBlocks[i+1] + ",\"block\":" + terrain.modifiedBlocks[i+2] + "},";
        }

        json += "{\"x\":" + terrain.modifiedBlocks[terrain.modifiedBlocks.Count-3] + ",\"y\":" + terrain.modifiedBlocks[terrain.modifiedBlocks.Count - 2] + ",\"block\":" + terrain.modifiedBlocks[terrain.modifiedBlocks.Count - 1] + "}}}";

            //Debug.Log(json);
        */
        List<int> temp = new List<int>();

        temp.Add(0);
        temp.Add(1);
        temp.Add(2);

        string json = JsonUtility.ToJson(temp);

        Debug.Log(json);
        //string temp;

        File.WriteAllText(TerrainSavePath, json);           // Writes the string to a file
    }

    // Called when Reading the saved data from the file
    public static TerrainData LoadTerrain ()
    {
        if (File.Exists(TerrainSavePath))   // Checks if the file exists
        {
            string json = File.ReadAllText(TerrainSavePath);    // Reads from the file and stores it in a json formatted string

            Debug.Log(json);



            TerrainData loadedTerrainData = JsonUtility.FromJson<TerrainData>(json);    // Converts string to terrain data object

            return loadedTerrainData;   // Returns terrain data object
        }
        else
        {
            Debug.LogError("Terrain file not found.");  // Logs error to the console
            return null;    // Returns null
        }
    }

    // --------------------- Save Game General ------------------------------------------

    // Called when creating and saving general data to a .json file
    public static void SaveGeneral (General generalData)
    {
        GameData gameData = new GameData(generalData);  // Creates a new creature data object which stores the general current data

        string json = JsonUtility.ToJson(gameData);     // Converts the general data to a json formatted string

        File.WriteAllText(GameGeneralSavePath, json);   // Writes the string to a file
    }

    // Called when Reading the saved data from the file
    public static GameData LoadGeneral ()
    {
        if (File.Exists(GameGeneralSavePath))   // Checks if the file exists
        {
            string json = File.ReadAllText(GameGeneralSavePath);    // Reads from the file and stores it in a json formatted string

            GameData loadedGameData = JsonUtility.FromJson<GameData>(json); // Converts string to general data object

            return loadedGameData;  // Returns general data object
        }
        else
        {
            Debug.LogError("General game file not found.");     // Logs error to the console
            return null;    // Returns null
        }
    }

    // ----------------------
}
