using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{  
    public int health = 10; // The amount of health the enemy has
    public Transform targetObject; // The object to move towards
    public float moveSpeed = 5f; // The speed at which the sprite moves
    private float bufferDistance = 100f; // The buffer distance to stop moving when a wall is close
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer
    private int hitCount = 0; // The number of times the enemy has been hit
    private Color hitColor = Color.red; // The color to change to when hit
    public Rigidbody2D rb; // Reference to the Rigidbody2D component  
    public PlayerController playerController; // Reference to the PlayerController script

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer component       
    }

    private void Update()
    {
        if (targetObject != null)
        {
            // Check for obstacles between the enemy and the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (targetObject.position - transform.position).normalized, Mathf.Infinity, LayerMask.GetMask("Wall"));

            if (hit.collider != null && hit.collider.CompareTag("Wall") && hit.distance <= bufferDistance)
            {
                // If there's a wall within buffer distance, don't move
                return;
            }
            else
            {         
                bufferDistance = 1f; // The buffer distance to stop moving when a wall is close
                // Move towards the target object
                transform.Translate((targetObject.position - transform.position).normalized * moveSpeed * Time.deltaTime);
            }
                                               
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            Destroy(collision.gameObject);
            health--; // Decrease the health by 1
            hitCount++; // Increase the hit count

            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                // Change the color of the enemy based on the hit count
                float redAmount = (float)hitCount / (float)health; // Calculate the ratio of hits to health
                spriteRenderer.color = Color.Lerp(Color.white, hitColor, redAmount); // Interpolate between white and hitColor based on the ratio
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {          
            playerController.TakeDamage(50); // Call the TakeDamage method in the PlayerController script
        }
    }  
}
