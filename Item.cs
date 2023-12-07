using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private LayerMask playerOrGround;
    [SerializeField] private Transform playerCheck;


    private const float playerCheckRadius = 1f;

    

    


    // Item need to stack on ground to help optimise performance
    // If items on top of eachother, same iblocks should collapse in


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(playerCheck.position, GetObjectSize(), playerOrGround);

        for (int i = 0; i < colliders.Length; i++)
        {

            Debug.Log(colliders[i]);

            if (colliders[i].gameObject.name == "")
            {
                // Player picks up item
                Debug.Log("pickup item");
            }
            
            if (colliders[i].gameObject.name == "")
            {
                // Item lands on ground
                Debug.Log("On ground");
            }
        }


    }


    private Vector2 GetObjectSize()
    {
        var p1 = gameObject.transform.TransformPoint(0, 0, 0);
        var p2 = gameObject.transform.TransformPoint(1, 1, 0);

        var w = p2.x - p1.x;
        var h = p2.y - p1.y;

        return new Vector2(w, h);
    }
}
