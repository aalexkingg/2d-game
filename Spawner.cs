using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public TerrainGenerator terrain;
    public GameObject hostileCreature;
    public General general;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        if (general.timeOfDay > 600)
        {



            if(Random.Range(1, 1000) == 1)
            {

                Vector3 centralPosition = player.transform.position;

                GameObject creature = GameObject.Instantiate(hostileCreature);

                creature.gameObject.transform.parent = this.transform;

                float randPosition = Random.Range(-300, 300);

                creature.transform.position = new Vector3(centralPosition.x + randPosition, 800);

                creature.SetActive(true);


            }
        }

        if (general.timeOfDay < 600)
        {
            // Destroy all hostile mobs
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }

    }
}
