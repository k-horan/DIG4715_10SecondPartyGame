using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public GameObject prefab;
    GameObject crusherObject;
    GameObject can;
    public Sprite newSprite;

    SpriteRenderer spriteRenderer;
    Animator crusherAnimator;

    AudioSource audioSource;
    public AudioClip hit;
    
    Rigidbody2D r2D;

    private bool insideCrushZone;
    public bool crushed;

    public float speed = 1f;

    float spawnTimer = 2;

    System.Random rand = new System.Random();
    float randSpawn;

    // Start is called before the first frame update
    void Start()
    {

        r2D = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

        can = GameObject.Find("can");

        crusherObject = GameObject.Find("crusher");
        crusherAnimator = crusherObject.GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        // when object is in crush zone, and player activates crusher, crush it
        if (insideCrushZone && crusherAnimator.GetBool("State"))
        {
            Debug.Log("Item crushed");
            audioSource.clip = hit;
            audioSource.Play();            
            
            ChangeSprite();
            Destroy(this);
        }

        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            randSpawn = rand.Next();
        }

        // chance to spawn item every second as child of can
        if (spawnTimer == 1f || spawnTimer == 1.5f)
        {
            if (randSpawn % 2 == 0)
            {
                Instantiate(prefab, can.transform);

            }
        }
        
        // spawn item every 2 seconds as child of can
        if (spawnTimer < 0)
        {
            Instantiate(prefab, can.transform);
            spawnTimer = 2;
        }
    }

    // check if items enter the crush zone of the crusher
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Item in Crush Zone");
        
        if (other.CompareTag("Player"))
        {
            insideCrushZone = true;

        }
    }

    // check if items exit the crush zone of the crusher
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Item Left Crush Zone");
        
        if (other.CompareTag("Player"))
        {
            insideCrushZone = false;
        }

        if (!crushed)
        {
            Debug.Log("Item not crushed. Ending Game");
            //Destroy(can);
            timer.gameTimerIsRunning = false;
            timer.endConditionIsRunning = true;
            timer.playerLost = true;
            
        }
    }

    void FixedUpdate()
    {   
        // moves the item along the conveyor belt
        r2D.AddForce(transform.right * speed);
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }
}
