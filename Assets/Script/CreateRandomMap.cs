using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomMap : MonoBehaviour
{
    public Transform[] startingRandPos;
    public GameObject[] roomMode; //  TLR -> 4D -> BLR -> BL

    public int direction;
    public int moveAmount = 10;

    private float timeGenerate = 0.10f;
    private float timeCount = 0f;

    public int minX;
    public int maxX;
    public int maxY;

    private bool stopGenerate;
    
    public LayerMask whatIsRoom;
    private void Start()
    {
        int randStartIndex = Random.Range(0, startingRandPos.Length);
        transform.position = startingRandPos[randStartIndex].position;

        Instantiate(roomMode[Random.Range(0, 3)], transform.position, Quaternion.identity);
        stopGenerate = false;
        direction = Random.Range(0, 100);

    }

    private void Update()
    {
        if (timeCount < 0 && !stopGenerate)
        {
            Move();
            timeCount = timeGenerate;
        }
        else
            timeCount -= Time.deltaTime;
    }

    private void Move()
    {
       
        if(direction <= 40) // left
        {
            if(transform.position.x > minX) // beyond limit
            {
                transform.position = new Vector2(transform.position.x - moveAmount, transform.position.y);
                direction = Random.Range(0, 100);
                //prevent repeating collide
                if(direction <= 70 && direction > 40) // prevent go right next time
                {
                    direction = Random.Range(0, 10) > 5 ? 10 : 100;
                }
                Instantiate(roomMode[Random.Range(0, 3)], transform.position, Quaternion.identity);
            }
            else
            {
                direction = 100; // else go down
            }
            Debug.Log("Left Call");
            
        }
        else if(direction <= 70) // right
        {
            if (transform.position.x < maxX) // beyond limit
            {
                transform.position = new Vector2(transform.position.x + moveAmount, transform.position.y);
                direction = Random.Range(0, 100);
                if (direction >= 0 && direction <= 40) // prevent go left next time
                {
                    direction = Random.Range(0, 10) > 5 ? 60 : 100;
                }
                
                Instantiate(roomMode[Random.Range(0, 3)], transform.position, Quaternion.identity);
            }
            else
            {
                direction = 100; //else go down
            }
            Debug.Log("Right Call");
            
        }
        else // go top
        {
            if(transform.position.y < maxY)
            {
                Collider2D detectRoom = Physics2D.OverlapCircle(transform.position, 2, whatIsRoom); // to check type room
                if(detectRoom != null)
                {
                    InitMap refDetecor = detectRoom.GetComponent<InitMap>();

                    if (refDetecor.typeRoom >= 2)
                    {
                        refDetecor.DestroyRoom();
                        Debug.Log("Destroy");
                        Instantiate(roomMode[1], transform.position, Quaternion.identity);
                    }
                }
                
                //re init room at current position before update new room
                transform.position = new Vector2(transform.position.x, transform.position.y + moveAmount);
                direction = Random.Range(0, 100);
                Debug.Log("Top Call");
                Instantiate(roomMode[Random.Range(1, 3)], transform.position, Quaternion.identity);
            }
            else
                stopGenerate = true;
        }
            
        
    }
}
