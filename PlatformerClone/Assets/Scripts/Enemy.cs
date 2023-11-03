using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float travelDistanceRight = 0;
    public float travelDistanceLeft = 0;
    public float speed;

    private float startingX;
    private bool movingRight = true;
    
    



    // Start is called before the first frame update
    void Start()
    {
        // when the scene starts, stores the initial x value of the object
        startingX = transform.position.x;



    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            // if the object is not farther than the start position plus the right travel distance, it can move right
            if (transform.position.x <= startingX + travelDistanceRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                movingRight = false;
                
            }
        }
        else
        {
            // if the object is not farther than the start position + left travel distance, it can move left
            if (transform.position.x >= startingX + travelDistanceLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            // if the object goes too far left, move right
            else
            {
                movingRight = true;
            }
        }
        
        

        

    }

    

}
