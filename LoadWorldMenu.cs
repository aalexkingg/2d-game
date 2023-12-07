using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
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

public class LoadWorldMenu : MonoBehaviour
{
    // -------- Define all global variables here  --------

    public GameObject playMenu;         // Defines play menu as a game object
    public GameObject loadWorldMenu;    // Defines load world menu as a game object
    public GameObject background;       // Defines load world background as a gameobject
    public GameObject worldTemplate;    // Defines the wolrd template button
    public Text text;
    public GameObject contentParent;

    // --------  Define all functions here  --------

    void Start()
    {
        string[] names = FetchWorlds();

        //text = GetComponent<Text>();
        //worldTemplate = GetComponent<GameObject>();

        //text = worldTemplate.gameObject.GetComponentInChildren<Text>();

        // Position of world button (x = 0, y starts at 40 and decreases by 80)

        if (names != null)
        {
            for (int i = 0; i < names.Length; i++)
            {

                Vector3 pos = new Vector3(0f, (40 - 80 * (i)), 0f);


                GameObject test = GameObject.Instantiate(worldTemplate);
                test.gameObject.transform.parent = contentParent.transform;
                //Text text = test.transform.GetChild(0).text;
            
                test.gameObject.transform.localPosition = pos;
                text.text = names[i];

            }
        }


        

        //text.text = names[3];
        //worldTemplate.transform.localPosition = pos;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // if the user presses the escape key then return to play menu
        {
            loadWorldMenu.gameObject.SetActive(false);      // Hides load world menu
            background.gameObject.SetActive(false);         // Hides load world menu background
            playMenu.gameObject.SetActive(true);            // Unhides play menu

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FetchWorlds();
        }
    }


    private string[] FetchWorlds()
    {
        string targetDirectory = "C:/Users/alex/Documents/Unity Projects/Project 1/Assets/Saves/";

        string[] folders = Directory.GetDirectories(targetDirectory);
        string[] names = new string[folders.Length];

        for (int i = 0; i < folders.Length; i++)
        {

            string word = folders[i];

            names[i] = word.Substring(word.LastIndexOf('/') + 1, word.Length - word.LastIndexOf('/') - 1);

            Debug.Log(names[i]);
        }

        return names;

    }

    public void Play()
    {

    }

    public void Edit()
    {

    }

    public void Revert()
    {

    }

    public void Delete()
    {

    }
}
