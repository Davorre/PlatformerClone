using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int Lives = 3;

    public float speed = 10f;

    public float jumpForce = 10f;

    private Rigidbody rigidbodyRef;

    public int totalWumpaFruit;

    public float deathYLevel = -3f;

    private Vector3 startPos;

    




    // Start is called before the first frame update
    void Start()
    {
        // gets rigidbody component off of the object and starts a reference to it
        rigidbodyRef = GetComponent<Rigidbody>();

        // set the starting position
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // side to side movement
        if (Input.GetKey(KeyCode.S))
        {
            // moves player backwards
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            // moves player frowards
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // moves player leftwards
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            // moves players rightwards
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // executes the jump function when pressing space
            Jump();
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.3f, Color.red);


        if (transform.position.y <= deathYLevel)
        {
            // if player dies, player respawns
            Respawn();
        }

        CheckForDamage();
    }
    /// <summary>
    /// hitting space bar mkaes the player jump
    /// </summary>
    private void Jump()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.3f))
        {
            Debug.Log("player is hitting the ground so jump");
            rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("player is not touching the ground so they  cannot jump");
        }


    }

    /// <summary>
    /// collects wumpa fruits
    /// </summary>
    /// <param name="other">the object being collided with</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wumpa Fruit")
        {
            totalWumpaFruit++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Enemy")
        {
            Respawn();
        }

        if (other.gameObject.tag == "Spike")
        {
            Respawn();
        }

       


    }

    

    /// <summary>
    /// if player dies, player loses a life and respawns 
    /// </summary>
    private void Respawn()
    {
        // teleport player to starting position and cause the player to lose a life
        Lives--;
        transform.position = startPos;

        if (Lives == 0)
        {
            // add code that end the game, by loading the end screen
            Debug.Log("Game Ends");
        }
    }

    /// <summary>
    /// check to see if the player hits the  womp hits the player from the top
    /// </summary>
    private void CheckForDamage()
    {
        RaycastHit hit;
        // raycast upwards, the raycast will return true if it hits an object
        // raycast (startPos, direction, output iht, distance for ray
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1))
        {
            // check to see if the object hitting the player is tagged Womp
            if (hit.collider.tag == "Enemy")
            {
                Respawn();
            }
        }
    }


}
