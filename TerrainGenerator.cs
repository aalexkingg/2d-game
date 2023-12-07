using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

public class TerrainGenerator : MonoBehaviour
{
    // -------- Define all global variables here  --------

    // --------------------   FIGURE OUT SLOPES  -----------------------

    

    public Tilemap tilemap;             // Tilemap of the terrain
    public TileBase dirtTilebase;       // Tile texture for dirt object
    public TileBase stoneTilebase;      // Tile texture for stone object
    public TileBase coalTilebase;       // Tile texture for coal object
    public TileBase ironTileBase;       // Tile texture for iron object
    public TileBase treeTileBase;       // Tile texture for tree object
    public TileBase grassTilebase;      // Tile texture for grass object
    public TileBase solidTilebase;

    public ItemBehaviour droppedItems;

    // --  Private variables  --


    // --  Public variables  --

    public static int biome = 0;                // Defines biome as 0 (plains = 0, hills = 1)
    public static int seaLevel = 50;            // Defines sea level as 50
    public static float currHeight = seaLevel;  // Defines the current height as sea level
    public int terrainWidth;                    // Terrain Width MUST be multiples of 50
    public int terrainHeight;                   // Defines the terrain height
    public int[,] mapArray;                     // Defines the mpa array
    public int seed;                            // Seed must be float else Mathf.PerlinNoise returns same value
    public List<int> modifiedBlocks = new List<int>();

    // --------  Define all functions here  --------

    // Called at the start of the game
    void Awake()
    {
        GetSeed();  // Generates a seed
        GenerateTerrain();  // generates the terrain
    }

    // Start is called before the first frame update
    void Start()
    {

        tilemap = GetComponent<Tilemap>();  // gets the tilemap component from the game

    }

    // Update is called once per frame
    void Update()
    {


    }

    // Called by the general class when setting the seed
    public void SetSeed(int newSeed)
    {
        seed = newSeed; // Sets the seed
        Debug.Log("s " + seed);

    }
    
    // Gets seed from general class, or creates random seed
    private void GetSeed()
    {
        // Seed can be retrieved from file or specified on world creation or random
        seed = General.seed;

        if (seed == 0)  // Checks if seed is 0
        {
            Debug.Log("Random seed generated"); // Logs that a random seed has been created
            seed = Random.Range(1, 10000);      // Sleects a random seed between 1 and 10,000, this range can be much larger
        }

        Debug.Log("Seed: " + seed);
    }

    // Called at start of game (or by load terrain function) which connects all the different functions
    void GenerateTerrain()
    {

        Random.InitState(seed); // Initializes the random number generator

        mapArray = new int[terrainWidth, terrainHeight];    // Creates an empty array

        int chunkWidth = terrainWidth / 50;     // Scales done world into 50 chunks
        int chunkCount = 0;     // Defines current chunk count
        
        while (chunkCount < chunkWidth)     // Loops while chunk count is less than chunk width
        {
            int width = Random.Range(2, 6);     // Selects a random width (scaled up by 50: 100 - 300 wide)

            if ((chunkCount + width) > chunkWidth)      // Checks if the sum of chunk count and width is greater than the chunk width
            {

                width = chunkWidth - chunkCount;        // Gets the remainder
            }

            int sectionStart = chunkCount * 50;     // Scales the chunk count back uo
            int sectionWidth = width * 50;          // Scales the width back up

            mapArray = HeightMap(mapArray, sectionStart, sectionWidth);     // Randomly generates section using perlin noise

            chunkCount += width;    // Increments chunk count with the width

        }

        // Renders the terrain using the randomly generated points
        RenderMap(mapArray, tilemap, dirtTilebase, stoneTilebase, coalTilebase, ironTileBase, treeTileBase, grassTilebase, solidTilebase);

        //Player.terrainComplete = true;  // Tells the player class that the terrain has been generated

    }
    
    // Called when rendering terrain
    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase dirtTile, TileBase stoneTile, TileBase coalTile, TileBase ironTile, TileBase treeTile, TileBase grassTile, TileBase solidTile)
    {

        // 0 = air, 1 = stone, 2 = dirt, 3 = grass, 4 = coal, 5 = iron, 11 = tree

        // Removes all individually sticking out blocks - makes the terrain seem smoother
        for (int x = 0; x < map.GetUpperBound(0)-2; x++)
        {
            for (int y = 0; y <= map.GetUpperBound(1); y++)
            {
                if ((map[x,y] == 0) && (map[x+1, y] == 1) && (map[x+2, y] == 0))
                {
                    map[x + 1, y] = 0;
                }
            }
        }

        map[0, map.GetUpperBound(1)] = 1;   // Sets top left hand corner block to stone to see observe terrain outline

        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles();

        //Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            bool grassDone = false;     // Defines grass done boolean as false
            int rand;                   // Creates a random variable

            //Loop through the height of the map
            for (int y = 0; y <= map.GetUpperBound(1); y++)
            {                    
                
                // 1 = stone, 0 = no tile
                if (map[x, y] == 1)
                {
                    rand = Random.Range(1, 40);     // Selects a random number between 1 and 40

                    if ( (rand == 1) || (rand == 2))    // Checks if random number equals 1 or 2 (1 in 20 chance)
                    {
                        map[x, y] = 4;  // Updates map value
                        tilemap.SetTile(new Vector3Int(x, y, 0), coalTile);     // Renders coal block
                    }
                    else if(rand == 3)      // Checks if value equals 3 (1 in 40 chance)
                    {
                        map[x, y] = 5;  // Updates map value
                        tilemap.SetTile(new Vector3Int(x, y, 0), ironTile);     // Renders coal block
                    }
                    else    // (37 in 40 chance)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), stoneTile);    // renders stone block
                    }
                }
                else
                {
                    if (!grassDone)     // Checks if grass/dirt layers have been generated
                    {
                        map[x, y] = 2;          // Updates map as dirt
                        map[x, y + 1] = 2;      // Updates map as dirt
                        map[x, y + 2] = 3;      // Updates map as grass
                        tilemap.SetTile(new Vector3Int(x, y, 0), dirtTile);     // Renders dirt block
                        tilemap.SetTile(new Vector3Int(x, y+1, 0), dirtTile);   // Renders dirt block
                        tilemap.SetTile(new Vector3Int(x, y+2, 0), grassTile);  // Renders grass block
                        grassDone = true;   // Sets grass done as true

                        // generates a tree
                        y += 3;
                        if (x > 1)      // Keeps tree on map
                        {

                            if ( map[x-2, y] == 0 && map[x-1, y] == 0           // Air either side
                                && map[x-2, y-1] == 3 && map[x-1, y-1] == 3)    // Dirt either side
                            {

                                if ( Random.Range(1, 5) == 1)       // (1 in 5 chance)
                                {
                                    map[x - 1, y] = 11;         // Updates map as tree
                                    map[x - 1, y + 1] = 11;     // Updates map as tree
                                    map[x - 1, y + 2] = 11;     // Updates map as tree
                                    tilemap.SetTile(new Vector3Int(x - 1, y, 0), treeTile);     // Renders tree block 
                                    tilemap.SetTile(new Vector3Int(x - 1, y+1, 0), treeTile);   // Renders tree block
                                    tilemap.SetTile(new Vector3Int(x - 1, y+2, 0), treeTile);   // Renders tree block
                                }

                            }

              
                        }
                    }  
                }
                

                if (y == 0)
                {
                    map[x, y] = -1;
                    tilemap.SetTile(new Vector3Int(x, y, 0), solidTile);
                }
            }
        }

        Player.terrainComplete = true;  // Tells the player class that the terrain has been generated

    }

    // Called when updating the map
    public void UpdateMap(Vector2Int arrayPosition, bool placeItem)
    {

        // When destroying tile, tile should be set to null and array position set to 0
        // When placing tile, tile should be set to tilebase and array position to corresponding number

        int x = arrayPosition.x;
        int y = arrayPosition.y;

        // If bool value is true, player is trying to palce block
        if (placeItem)
        {
            // Only place block if current position is air
            if (mapArray[x, y] == 0)
            {

                TempSetStone(x, y);

                modifiedBlocks.Add(x);
                modifiedBlocks.Add(y);
                modifiedBlocks.Add(1);

                
            }
        }
        // Player is trying to destroy block
        else
        {
            // Only destroy block if current position isnt air
            if (mapArray[x, y] != 0)
            {

                droppedItems.dropItem(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1), mapArray[x, y]);


                /*
                float destroyTime;

                switch(mapArray[arrayPosition.x, arrayPosition.y])
                {
                    // Destroy stone
                    case 1:

                        destroyTime = 2f;

                        break;

                    // Destroy dirt
                    case 2:

                        destroyTime = 0.5f;

                        break;

                    // Destroy grass
                    case 3:

                        destroytime = 0.5f;

                        break;

                    // Destroy coal
                    case 4:

                        destroyTime = 3f;

                        break;

                    // Destroy iron
                    case 5:

                        destroyTime = 4f;

                        break;

                    // Destroy tree
                    case 11:

                        destroyTime = 1f;

                        break;


                }
                */


                // Stops player destroying lowest layer of terrain
                if (mapArray[x, y] == -1)
                {
                    return;
                }

                // When tree is destroyed, entire tree is removed by checking 2 tiles above and below destroyed tile
                if (mapArray[x, y] == 11)
                {
                    if (mapArray[x, y-2] == 11) { mapArray[x, y - 2] = 0; tilemap.SetTile(new Vector3Int(x, y-2, 0), null); }

                    if (mapArray[x, y-1] == 11) { mapArray[x, y - 1] = 0; tilemap.SetTile(new Vector3Int(x, y-1, 0), null); }

                    if (mapArray[x, y+1] == 11) { mapArray[x, y + 1] = 0; tilemap.SetTile(new Vector3Int(x, y+1, 0), null); }

                    if (mapArray[x, y+2] == 11) { mapArray[x, y + 2] = 0; tilemap.SetTile(new Vector3Int(x, y+2, 0), null); }
                }
                
                

                tilemap.SetTile(new Vector3Int(x, y, 0), null);
                mapArray[x, y] = 0;

                modifiedBlocks.Add(x);
                modifiedBlocks.Add(y);
                modifiedBlocks.Add(0);


            }
        }
    }

    // Called when generating random terrain
    public static int[,] HeightMap(int[,] map, int start, int width)    // In final version rename to addTerrain
    {
        
        int pInterval, pheight;
        float pmin, pmax, pred;
        // Toggle biome id
        biome = 1 - biome;
        
        if (biome == 1)
        {
            // hills
            pInterval = 2;
            pmin = 0.5f;
            pmax = 20f;
            pred = 0.6f;
            pheight = 30;
        }
        else
        {
            // plains
            pInterval = 10;
            pmin = 0.5f;
            pmax = 10f;
            pred = 0.5f;
            pheight = 10; 
        }

        float perlRand = Random.Range(pmin, pmax);

        //Smooth the noise and store it in the int array

        int newPoint, points;
        //Used to reduced the position of the Perlin point
        float reduction = pred;

        //Used in the smoothing process
        Vector2Int currentPos, lastPos;
        //The corresponding points of the smoothing. One list for x and one for y
        List<int> noiseX = new List<int>();
        List<int> noiseY = new List<int>();

        //Generate the noise
        for (int x = 0; x <= width; x += pInterval)
        {
            // Creates a random, but gradual set of y-coordinates
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, (perlRand * reduction))) * pheight);
            noiseY.Add(newPoint);   // Y-coordinate added to Y list
            noiseX.Add(x);          // X-coordinate added to X list
        }

        points = noiseY.Count;

        //Start at 1 so we have a previous position already
        for (int i = 1; i < points; i++)
        {
            //Get the current position
            currentPos = new Vector2Int(noiseX[i], noiseY[i]);
            //Also get the last position
            lastPos = new Vector2Int(noiseX[i - 1], noiseY[i - 1]);

            //Find the difference between the two
            Vector2 diff = currentPos - lastPos;

            //Set up what the height change value will be 
            float heightChange = diff.y / pInterval;

            // Force biome to end at sea level
            if (i == (points-1))
            {
                heightChange = (seaLevel - currHeight) / diff.x;
            }
            
            //Work our way through from the last x to the current x
            for (int x = lastPos.x; x < currentPos.x; x++)
            {

                for (int y = Mathf.FloorToInt(currHeight); y >= 0; y--)
                {
                    map[start + x, y] = 1;
                }
                currHeight += heightChange;
            }
        }
        return map;
        
    }

    // Testing
    public void TempSetStone(int x, int y)
    {
        if (mapArray[x, y] != 1)
        {
            mapArray[x, y] = 1;
            tilemap.SetTile(new Vector3Int(x, y, 0), stoneTilebase);
        }
    }

    // Testing
    private void PrintArray()
    {
        for (int x = 0; x < mapArray.GetUpperBound(0); x++)
        {
            for (int y = 0; y < mapArray.GetUpperBound(1); y++)
            {
                Debug.Log(mapArray[x, y]);
            }
        }
    }

    // Called when saving terrain data
    public void SaveTerrain ()
    {
        SaveSystem.SaveTerrain(this);   // Saves terrain state in save terrain function
    }

    // Called when loading terrain from saved data
    public void LoadTerrain()
    {
        TerrainData data = SaveSystem.LoadTerrain();    // Calls load terrain function in save system and stores in terrain data object

        seed = data.seed;       // Applies saved seed to current seed
        //mapArray = data.map;    // Applies saved map to current map

        GenerateTerrain();      // Regenerates terrain

    }

}
