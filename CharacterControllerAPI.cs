using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

public class CharacterControllerAPI : MonoBehaviour
{
    // -------- Define all global variables here  --------

    // --  Private variables  --

    [SerializeField] private float jumpForce = 8000f;               // Amount of force added when the player jumps.
    [SerializeField] private float movementSmoothing = .05f;        // How much to smooth out the movement
    [SerializeField] private LayerMask whatIsGround;                // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;                 // A position marking where to check if the player is grounded.
    [SerializeField] private bool standing;                         // Tells whether or not the player is standing still
    [SerializeField] private float standingVelocity = 0.1f;         // Velocity at which the player is interpeted as standing

    private Rigidbody2D rb2D;                           // Controls the sprite using physics engine
    private Animator animator;                          // Allows the program to change which animation is attached to the player
    private BoxCollider2D boxGrip;                      // Allows a grippy box collider to be turned on/off when the player is standing

    private float groundedRadius = 0.2f;                // Radius of the overlap circle to determine if grounded
    private bool grounded;                              // Whether or not the player is grounded.
    private bool facingRight = true;                    // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;            // 3D vector with x, y, z components - .zero is empty vector
    private bool airControl = true;                     // Allows the player to move in the air
    public float jumpCheckWidth = 11f;                  // Specifies the width of the collision box which checks if the player is able to jump 


    // -- Public variables  --

    public bool isPlayer = false;                       // Defines whether an object is a player or not


    // --------  Define all functions here  --------

    // Called at the start of the game
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();         // Attaches the Rb2D variable to the rigid body component of the player
        animator = GetComponent<Animator>();        // Attaches the animator variable to the animator component of the player
        boxGrip = GetComponent<BoxCollider2D>();    // Attaches the boxGrip variable to the box collider component of the player
    }

    // Called once every frame
    private void FixedUpdate()
    {
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // Physics2D.OverlapCircleAll Gets a list of all colliders that fall within a circular area and returns it into a Collider2D array
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, new Vector2(jumpCheckWidth, 0.2f), whatIsGround);

        

        // Loops through collider array
        for (int i = 0; i < colliders.Length; i++)
        {
            //Debug.Log(colliders[i]);

            if (colliders[i].gameObject != gameObject)  // If the collider is not a game object then player is on the ground (ground is not a game object)
            {
                grounded = true;    // Sets grounded to true
                //Debug.Log("r "+colliders[i].gameObject);
            }
        }

        // If the player presses an input to go left or write the value will be 1 or -1, therefore if the player is standing still it will be 0
        if ( ( (rb2D.velocity.x >= -standingVelocity) && (rb2D.velocity.x <= standingVelocity) ) || Player.inventory)
        {
            standing = true;        // Sets standing to true
        }
        else
        {
             standing = false;     // Sets standing to false
        }

        animator.SetBool("Standing", standing);     // Sets the transitions in the animator to true or false depending on whther the player is moving

        boxGrip.enabled = standing;                 // Enables the box grip component which causes the player to grip the floor
    }
 
    // Called by the player and creature class when moving the player
    public void Move(float move, bool jump)
    {

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, rb2D.velocity.y);

        // And then smoothing it out and applying it to the character
        //Vector3.SmoothDamp gradually changes a vector towards a specified parameter
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        // If the input is moving the player right and the player is facing left
        if (move > 0 && !facingRight)
        {
            // Flips the player.
            Flip();
        }

        // If the input is moving the player left and the player is facing right
        else if (move < 0 && facingRight)
        {
            // Flips the player.
            Flip();
        }
        
        // Detects if player is grounded and jump = true (jump button pressed)
        if (grounded && jump)
        {
            // Sets grounded to fasle and adds a vertical force to the player
            grounded = false;
            rb2D.AddForce(new Vector2(0f, jumpForce));

        }
    }

    // Called by the Move function when flipping the player
    private void Flip()
    {
        facingRight = !facingRight;     // Switch the way the player is labelled as facing.

        Vector3 theScale = transform.localScale;    // Creates a new scale variable (Vector3 as scale has x, y, z)
        theScale.x *= -1;                           // Multiply players x local scale by -1 to make the character look in the other direction
        transform.localScale = theScale;            // Sets the players current scale to the one just calculated
    }
}


