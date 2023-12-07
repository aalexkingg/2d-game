using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public class Player : MonoBehaviour
{
    // -------- Define all global variables here  --------

    public CharacterControllerAPI controller;   // References the CharacterController script under 'controller'
    public TerrainGenerator terrain;            // References the TerrainGenerator class under 'terrain'
    public HealthBar healthBar;                 // References the HealthBar class under 'healthBar'
    public ItemBehaviour droppedItems;

    // --  Private variables  --

    [SerializeField] private float runSpeed = 400f;     // Horizontal speed of the player
    [SerializeField] private int maxHealth = 100;       // Sets the players maximum health
    [SerializeField] private bool limitMiningRange = true;  // Sets the player mining distance limit
    [SerializeField] private Transform miningCheckPivot;    // Transform object to determine position/distance of mining

    const float miningRadius = 10f;
    const float creatureCheckRadius = 1f;       // The radius used to check for creatures

    private float horizontalMove = 0f;          // Used to store direction and speed of the player
    private bool jump = false;                  // Used to state whether the player has pressed the jump button
    private float tickDamageTime = 0.5f;        // The amount of time between every damage dealt if the player is constantly taking damage
    private float damageTimer = 0;              // A timer used to check if the time has elapsed passed the tick damage threshold

    // --  Public variables  --

    public static bool terrainComplete = false;     // Signifies if the terrain generation has completed
    public int currentHealth;                       // Defines the current health of the player
    public static bool inventory = false;           // States whether the players inventory is open or not


    // --------  Define all functions here  --------

    // Called at the start of the game
    void Awake()
    {
        // Sets the player base position at approximately the centre of the world
        this.transform.position = new Vector2(terrain.terrainWidth*6, 1200);
        
    }

    // Called at the start of the game
    void Start()
    {
        currentHealth = maxHealth;          // On start sets the current health to max health
        healthBar.SetMaxHealth(maxHealth);  // Parses the max health to the HealthBar class

    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the terrain has been completed
        if (terrainComplete)
        {
            transform.position = GetStartPosition();    // Once the terrain has been generated, a start position is calculated 
            terrainComplete = false;                    // Sets terrain complete back to false so that the player isnt moved back to the beginning
        }

        // Checks if A is being held down, then move left
        if (Input.GetKey(KeyCode.A))
        {
            // Moves the player left
            horizontalMove = -1;
        }
        // If D is being held down then move right
        else if(Input.GetKey(KeyCode.D))
        {
            // Moves the player right
            horizontalMove = 1;
        }
        // If neither A or D are being held down then dont move
        else
        {
            // horizontalMove is zero, so the player does not move
            horizontalMove = 0;
        }

        horizontalMove *= runSpeed;     // Multiplies the direction by the speed
        
        // Detects when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !PauseMenu.paused)
        {
            jump = true;    // Sets jump to true
        }

        //When inventory is open character movement needs to be disabled
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory)      // Checks if the inventory is already open
            {
                inventory = false;  // Sets inventory to false
            }
            else
            {
                inventory = true;   // Sets inventory to true
            }
        }

        // Executes when left mouse button pressed
        if (Input.GetMouseButtonDown(0) && !PauseMenu.paused)
        {
            int x = Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / 12.8f);
            int y = Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / 12.8f);

            float x1 = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float y1 = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

            float x2 = miningCheckPivot.position.x;
            float y2 = miningCheckPivot.position.y;

            //Debug.Log("Distance: " + Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
            
            DestroyBlock(new Vector2Int(x, y));
            /*
            float destroyTime = CalculateDestroyTime(new Vector2Int(x, y));
            float time = 0;

            while ( (Input.GetMouseButtonDown(0)) && (Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / 12.8f) == x) && (Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / 12.8f) == y) && time < destroyTime) {

                time += Time.deltaTime;
            }

            Debug.Log("time " + time);
            Debug.Log("destroy time " + destroyTime);
            Debug.Log(Mathf.Approximately(time, destroyTime));

            if (Mathf.Approximately(time, destroyTime))
            {
                Debug.Log("test");
                DestroyBlock(new Vector2Int(x, y));
            }
            */
            
        }

        // Executes when right mouse button pressed
        if (Input.GetMouseButtonDown(1) && !PauseMenu.paused)
        {
            int x = Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / 12.8f);
            int y = Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / 12.8f);
            
            PlaceBlock(new Vector2Int(x, y));
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            for(int i = 0; i < terrain.modifiedBlocks.Count; i += 3)
            {
                Debug.Log("x: " + terrain.modifiedBlocks[i] + " y: " + terrain.modifiedBlocks[i + 1] + " block: " + terrain.modifiedBlocks[i + 2]);
            }
        }

    }

    // Function called once every frame - Used for calculations
    void FixedUpdate()
    {
        //faceMouse();
        CheckHealth();          // First checks health of player so that no unnecessary calculation are done if the player is already dead


        Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);     // Calculates the mouse's position in the world

        if (!inventory)     // Only allows the player to move if they are not in their inventory
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);    // Moves the player by calling the Move function
        }
        else
        {
            controller.Move(0f, false);     // Doesnt move the player because their inventory is open
        }

        jump = false;   // Sets jump back to false so that the player only jumps once


    }

    // Called when player presses right mouse button
    private void PlaceBlock(Vector2Int blockPosition)
    {
        terrain.UpdateMap(blockPosition, true);
    }

    // Called when player presses left mouse button
    private void DestroyBlock(Vector2Int blockPosition)
    {
        // Call TerrainGenerator function which places and destroys tiles in world
        terrain.UpdateMap(blockPosition, false);
        


    }

    private float CalculateDestroyTime(Vector2Int position)
    {
        
        
        int block = terrain.mapArray[position.x, position.y];

        switch(block)
        {
            // Destroy stone
            case 1:

               return 2f;

            // Destroy dirt
            case 2:

                return 0.5f;

            // Destroy grass
            case 3:

                return 0.5f;

            // Destroy coal
            case 4:

                return 3f;

            // Destroy iron
            case 5:

                return 4f;

            // Destroy tree
            case 11:

                return 1f;

            default:

                return Mathf.Infinity;
        }
        
    }

    // Called every frame to make player face mouse
    private void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        //playerHead.transform.right = direction;
        //transform.up = direction;
    }

    // Called when the player takes damage
    public void TakeDamage()
    {
        damageTimer += Time.fixedDeltaTime;     // Incrememnts the timer

        if (damageTimer >= tickDamageTime)      // Checks if the timer has exceed the tick damage time
        {
            currentHealth -= 10;                // Decreases the players health by 10 (liable to change)
            healthBar.SetHealth(currentHealth); // Updates the health bar
            damageTimer = 0;                    // Resets the damage timer back to 0
        }

    }

    // Called once every frame by the fixed update function
    private void CheckHealth()
    {
        if (currentHealth <= 0)     // Checks if the players current health is less than or equal to 0
        {
            Debug.Log("Dead");                  // Logs 'Dead!' to the console
            SceneManager.LoadScene("Respawn");  // Loads the respawn scene
        }
    }

    // Called at the start of the game when calculating the half way point of the map
    public Vector2 GetStartPosition()
    {
        float playerX = (terrain.terrainWidth / 2) * 12f;   // Defines the players x position as the midpoint of the map width
        float playerY = TerrainGenerator.seaLevel * 12f;    // Defines the players y position as the midpoint of the map height

        // x and y use a multiplier of 12 as this is the scale of the game (10 * 1.2), with 10 being the scale, and 1.2 being the block size
            
        for (int y = TerrainGenerator.seaLevel; y < terrain.mapArray.GetUpperBound(1); y++)     // Loops from sea level (half the height of the array) to the top of the array
        {
            if (terrain.mapArray[terrain.terrainWidth / 2, y] == 0)     // Checks if the current block is air
            {
                playerY = (y + 8) * 12f;    // sets the players Y value 8 pixels above the first air bloack
                break;                      // Exits the for loop so that it does not unnecessarily loop through the rest of the map array
            }
        }

        return new Vector2(playerX, playerY);
        
    }

    // Function called when saving the game
    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);    // Calls the save player function in the save system class
    }

    // Function called when loading the game
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();      // Calls the load player function from the save system calls and stores it in a player data object

        Vector3 position;   // Defines a temporary position variable

        position.x = data.position[0];      // Sets the saved x value to the variable 
        position.y = data.position[1];      // Sets the saved y value to the variable
        position.z = data.position[2];      // Sets the saved z value to the variable
        
        transform.position = position;      // Applies the temporary variable to the players position

    }
    
}
