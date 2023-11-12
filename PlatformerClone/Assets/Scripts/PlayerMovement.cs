using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Player values
    public int Lives = 3;
    public float speed = 10f;    
    public float jumpForce = 10f;
    private Rigidbody rigidbodyRef;
    private Vector3 startPos;
    public int maxHealth = 3;
    public int healthPoints;
    public float bounceForce;

    // Score value
    public int totalWumpaFruit;

    // Death from falling out of bounds
    public float deathYLevel = -10f;

    // Enemy Value
    public int Damage = 1;
  
    // Teleport Point
    private Vector3 TeleportPoint;

    // Spin attack values
    public float spinDuration;
    public Collider spinAttackCollider;
    public bool isSpinning = false;

    // Start is called before the first frame update
    void Start()
    {
        // gets rigidbody component off of the object and starts a reference to it
        rigidbodyRef = GetComponent<Rigidbody>();

        // set the starting position
        startPos = transform.position;

        //Sets your health to max health at the start
        healthPoints = maxHealth;
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

        // Press E to spin attack
        if (Input.GetKeyDown(KeyCode.E) && !isSpinning)
        {
            StartCoroutine(SpinAttack());
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
        // Collects fruit and scores it
        if (other.gameObject.tag == "WumpaFruit")
        {
            totalWumpaFruit++;
            other.gameObject.SetActive(false);
        }

        // Enemy damage
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage();
        }

        if (other.gameObject.tag == "SpikedEnemyTurtle")
        {
            TakeDamage();
        }

        if(other.gameObject.tag == "ShieldEnemy")
        {
            TakeDamage();
        }
        if (isSpinning && other.gameObject.tag == "ShieldEnemy")
        {
            // Handle the defeat of the ShieldEnemy here
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Spike")
        {
            TakeDamage();
        }

        // If player stands on teleporter, move to the next area
        if (other.gameObject.tag == "Teleporter")
        {
            TeleportPoint = other.gameObject.GetComponent<Teleporter>().spawnPoint.transform.position;
            transform.position = TeleportPoint;
        }

        // if touches the eath plane, respawn
        if (other.gameObject.tag == "Death Plane")
        {

            Respawn();
        }

        // if enemy is within spinning range kill enemy
        if (isSpinning && other.gameObject.tag == "Enemy")
        {
            // Handle enemy defeat or damage here
            other.gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// Spin attack function, checks if an enemy is within the spin attack collider and will kill it
    /// </summary>
    /// <returns>continues spin attack for x seconds as stated in editor</returns>
    private IEnumerator SpinAttack()
    {
        // sets spinning to true
        isSpinning = true;
        // enables spin attack collider
        spinAttackCollider.enabled = true;

        // continues spin attack for x seconds as stated in editor
        yield return new WaitForSeconds(spinDuration);

        // disables after timer
        spinAttackCollider.enabled = false;
        isSpinning = false;
    }



    /// <summary>
    /// if player dies, player loses a life and respawns 
    /// </summary>
    private void Respawn()
    {
        // teleport player to starting position and cause the player to lose a life
        Lives--;
        transform.position = startPos;
        healthPoints = maxHealth;

        if (Lives == 0)
        {
            // add code that end the game, by loading the end screen
            Debug.Log("Game Ends");
            SceneManager.LoadScene(2);
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
                // 
                
                TakeDamage();
            }
        }
    }

    /// <summary>
    ///  has player take damage
    /// </summary>
    private void TakeDamage()
    {
        if (isSpinning) return;
        // sets HP to new value
        healthPoints -= Damage;
        // if health is <= 0, triggers respawn
        if(healthPoints <= 0 )
        {
            Respawn();
        }

    }

    /// <summary>
    /// kills game object
    /// </summary>
    public void Kill()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Checks for the enemy that cannot be damaged by jumping on it
    /// </summary>

    private void CheckForShieldEnemy()
    {
        RaycastHit hit;
        // raycast upwards, the raycast will return true if it hits an object
        // raycast (startPos, direction, output iht, distance for ray
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            // check to see if the object hitting the player is tagged Womp
            if (hit.collider.tag == "ShieldEnemy")
            {

                Kill();

            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ShieldEnemy")
        {
            // Check if the player is above the enemy
            if (IsPlayerAboveEnemy(collision.transform))
            {
                // Bounce off the enemy without taking damage
                BounceOff();
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            // Check if the player is above the enemy
            if (IsPlayerAboveEnemy(collision.transform))
            {
                // Defeat the enemy and bounce off
                Destroy(collision.gameObject);
                BounceOff();
            }
            else
            {
                // Take damage if not landing on top of the enemy
                TakeDamage();
            }
        }
    }


    bool IsPlayerAboveEnemy(Transform enemyTransform)
    {
        // Raycast logic or position comparison to check if the player is above
        RaycastHit hit;
        // Cast a ray straight down from the player
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Check if the ray hits the enemy GameObject
            return hit.collider.transform == enemyTransform;
        }

        return false;
    }

    public bool IsPlayerAbove(Transform otherTransform)
    {
        return transform.position.y > otherTransform.position.y;
    }


    public void BounceOff()
    {
        // Add a force upwards to make the player bounce off
        rigidbodyRef.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }

}
