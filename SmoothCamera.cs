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

public class SmoothCamera : MonoBehaviour
{
    // -------- Define all global variables here  --------

    public float smoothSpeed = 1f;      // Defines the transition time for the .Lerp function to move the camera position
    public Transform target;            // Defines the target as a transofmr object
    public Vector3 offset;              // defines the offset as a vector 3


    // --------  Define all functions here  --------

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)     // Checks if there is a target - Prevents null error
        {
            Vector3 desiredPosition = target.position + offset;     // Defines desired position by summing the target position and offset 
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);      // Uses .Lerp to create a smooth movement
            transform.position = smoothedPosition;      // Applies the smooth position to the camera position
        }
    }
}
