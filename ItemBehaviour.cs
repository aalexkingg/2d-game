using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask ignoreCollision;
    

    public GameObject stone;
    public GameObject dirt;
    public GameObject grass;

    //private const float itemWidth = (RectTransform)this.gameObject.transform.rect.width;
    

    // Class needs to be in charge of creating/rendering items when they are destroyed


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Collider2D collider = Physics2D.OverlapBoxAll(groundCheck.position, new Vector2(this.transform., whatIsGround);

        if (GetComponent<Collider>())
        {
            // Item is touching ground
        }
    }

    // Called when dropped item needs to be rendered on floor
    public void dropItem(Vector3 pos, int itemType)
    {
        //Debug.Log("test");

        switch(itemType)
        {
            // Destroy stone
            case 1:

                //Instantiate(stonePrefab, position, Quaternion.identity).transform.parent = this.transform; ;
                GameObject stoneItem = GameObject.Instantiate(stone);
                stoneItem.transform.position = pos;
                //stoneItem.transform.position.z = 0;
                stoneItem.transform.parent = this.transform;

                //creature.gameObject.transform.parent = this.transform;
                break;

            // Destroy dirt
            case 2:

                Instantiate(dirt, pos, Quaternion.identity).transform.parent = this.transform; ;

                break;

            // Destroy grass
            case 3:

                Instantiate(grass, pos, Quaternion.identity).transform.parent = this.transform;

                break;

            // Destroy coal
            case 4:

                //Instantiate(myPrefab, position, Quaternion.identity);

                break;

            // Destroy iron
            case 5:

                //Instantiate(myPrefab, position, Quaternion.identity);

                break;

            // Destroy tree
            case 11:

                //Instantiate(myPrefab, position, Quaternion.identity);

                break;

        }

        /*
        Vector3 centralPosition = player.transform.position;

        GameObject creature = GameObject.Instantiate(hostileCreature);

        creature.gameObject.transform.parent = this.transform;

        float randPosition = Random.Range(-300, 300);

        creature.transform.position = new Vector3(centralPosition.x + randPosition, 800);

        creature.SetActive(true);
        */
        //Instantiate(myPrefab, position, Quaternion.identity);
    }







}
