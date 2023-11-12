using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject wumpaFruitPrefab; // Assign in inspector

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            // Check if player hits the crate from below or is jumping/spinning
            if (player != null)
            {
                if (player.IsPlayerAbove(transform) || player.isSpinning)
                {
                    player.BounceOff();
                    BreakCrate();
                }
            }
        }
    }

    public void BreakCrate()
    {
        // Instantiate WumpaFruit
        Instantiate(wumpaFruitPrefab, transform.position, Quaternion.identity);

        // Destroy the crate
        Destroy(gameObject);
    }
}
