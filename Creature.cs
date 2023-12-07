using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System.Windows;
using System.Threading;

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

public class Creature : MonoBehaviour
{
    // -------- Define all global variables here  --------

    public CharacterControllerAPI controller;   // References character controller class using the variable 'controller'
    public Player player;                    // References player class using the variable 'player'

    public GameObject playerObject;

    // -- Private variables  --

    [SerializeField] private Transform playerCheck;     // Creates radius around creature that can check if player comes close
    [SerializeField] private LayerMask whatIsPlayer;    // Creates a layer mas kso that creature can identify player layer
    [SerializeField] private LayerMask whatIsGround;    // Creates a layer mask so that the over lap collider knows what to look for
    [SerializeField] private Transform jumpCheck;       // Creates a transform object so that the position of the check can be obtained
    [SerializeField] private bool Hostile = false;      // States whether th ecreature is hostile or passive

    private const float playerCheckRadius = 10f;

    private float jumpCheckRadius = 0.2f;       // The check radius of the jump check object
    private bool jump = false;                  // States whether the creature needs to jump
    private float xRange = 300f;                // X tracking range of the player
    private float yRange = 50f;                 // Y tracking range of the player
    private float timer = 0;                    // 
    private int direction = 0;                  // Stores the current direction the creature is moving in
    private float patrolInterval;               // 

    // Creature needs to despawn when the player is 3500 pixels away

    // --------  Define all functions here  --------

    // Called at the start of the game
    private void Start()
    {
        patrolInterval = Random.Range(1f, 4f);  // creates a random interval at the beginning of the game
        
    }

    // Called once evry frame
    void FixedUpdate()
    {

        CheckForPlayer();

        // Stores the ground collider, if ground comes within the overlap circle, in the colliders variable
        Collider2D colliders = Physics2D.OverlapCircle(jumpCheck.position, jumpCheckRadius, whatIsGround);

        // Checks if the colliders variable is empty (no ground detected)
        if (!colliders)
        {
            jump = false;   // Sets jump to false
        }
        else
        {
            jump = true;    // Sets jump to true
        }

        if (Hostile)    // Checks if the creature is hostile
        {
            // Checks if the player is within range
            if ( (Mathf.Abs(this.transform.position.x - playerObject.transform.position.x) < xRange) && (Mathf.Abs(this.transform.position.y - playerObject.transform.position.y) < yRange))
            {
                HostileMovement();      // Creature moves as a hostile (tracking player)
            }
            else
            {
                PassiveMovement();      // Creature moves passively (not tracking player)
                CheckDespawnRange();    // Checks if creature is outside of spawn range
                                        // Increases optimisation by destroying irrelavnt objects
            }
        }
        else
        {
            PassiveMovement();          // Creature moves passively (not tracking player)
            CheckDespawnRange();        // Checks if creature is out of range
        }
        
    
    }

    // Called when creature needs to track player
    void HostileMovement ()
    {
        // Calls the 'Move' function from the character controller class and moves the creature in the direction of the player
        controller.Move((WhereIsPlayer() * 200) * Time.fixedDeltaTime, jump);

    }

    // Called when
    void PassiveMovement ()
    {
        // Increments timer
        timer += Time.fixedDeltaTime;

        if (timer >= patrolInterval)    // Checks if timer has exceed interval
        {
            direction = Random.Range(-1, 2);            // Changes direction to random value
            patrolInterval = Random.Range(1f, 4f);      // Changes next interval to random value
            timer = 0;                                  // Resets timer
        }

        // Calls the 'Move' function from the character controller class making the creature move passively
        controller.Move((direction * 200) * Time.fixedDeltaTime, jump); 


    }

    // Called every frame to check if creature has collided with player
    private void CheckForPlayer()
    {
        // Checks if object with specified layer mask is with in a circular radius
        Collider2D collider = Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);

        if (collider)
        {
            player.TakeDamage();    // Player takes damage if collider contains player object
        }
    }

    // Called when the player comes in range of the creature
    private int WhereIsPlayer ()
    {
        // Stores the players current position in this variabe
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        // stores the creatures current position in this variable
        Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        // Decides if the players x value is less than the creatures'
        if (playerPosition.x < position.x)
        {
            // Move left
            return -1;
        }
        // Decides if the players x value is greater than the creatures'
        else if (playerPosition.x > position.x)
        {
            // Move right
            return 1;
        }
        else
        {
            // Above or below
            return 0;
        }
    }

    // Called to check if creature is too far from player
    private void CheckDespawnRange()
    {

        //Debug.Log(Mathf.Abs(this.transform.position.x - playerObject.transform.position.x));

        if (Mathf.Abs(this.transform.position.x - playerObject.transform.position.x) > 600)
        {
            Debug.Log("Destroyed");
            GameObject.Destroy(gameObject);
        }
    }

    // Called by the save system class when saving the creature
    public void SaveCreature ()
    {
        SaveSystem.SaveCreature(this);      // Sends the current state of the creature to the save system class
    }

    // Called when loading the creature into the gane
    public void LoadCreature ()
    {
        CreatureData data = SaveSystem.LoadCreature();  // Calls the load creature function and stores the creatures loaded data in a creature data object

        Vector3 position;   // Creates a temporary position variable

        position.x = data.position[0];      // Stores the creatures saved x position in the variable
        position.y = data.position[1];      // Stores the creatures saved y position in the variable
        position.z = data.position[2];      // Stores the creatures saved z position in the variable

        transform.position = position;      // Applies the values in the temporary variable to the creatures position
    }
}
