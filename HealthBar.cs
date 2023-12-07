using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public class HealthBar : MonoBehaviour
{
    // --------  Define all global variables here  --------

    public Slider slider;       // Defines the health bar as a slider


    // --------  Define all functions here  --------

    // This function is called by the player class at the start of the game
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;       // Sets the sliders maximum value to the players maximum health
        slider.value = health;          // Sets the sliders current value to the players maximum health
    }

    // This function is called when the player takes damage
    public void SetHealth(int health)
    {
        slider.value = health;      // Sets the sliders current value to the players current health
    } 
}
