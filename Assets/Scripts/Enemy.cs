using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public GameObject deathEffect; // Reference to the death effect prefab
    private GameObject targetObject; // Reference to the player object
    public SpriteRenderer spriteRenderer; // Reference to the sprite renderer
    
    private int hitCount = 0; // The number of times the enemy has been hit
    private Color hitColor = Color.red; // The color to change to when hit
    private float shootTimer = 0f;

    public float shootCooldown = 2f;
    public int health = 10; // The amount of health the enemy has

    //start method
    private void Start()
    {
        // Find the player object in the scene
        targetObject = GameObject.FindGameObjectWithTag("Player");
    }
    //update mehod
    private void Update()
    {
        if (targetObject != null)
        {
            // Calculate the direction from the player to the enemy
            Vector2 direction = (transform.position - targetObject.transform.position).normalized;

            // Calculate the distance between the player and the enemy
            float distanceToEnemy = Vector2.Distance(targetObject.transform.position, transform.position);

            // Cast a ray from the player towards the enemy with limited distance
            RaycastHit2D hit = Physics2D.Raycast(targetObject.transform.position, direction, distanceToEnemy, LayerMask.GetMask("Wall"));

            // Draw a debug line to visualize the ray
            Debug.DrawRay(targetObject.transform.position, direction * distanceToEnemy, Color.green);

            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                // If the ray hits a wall, do not shoot
            }
            else
            {             
                shootTimer -= Time.deltaTime;

                // If the shoot timer reaches zero, shoot
                if (shootTimer <= 0)
                {
                    Shoot();
                    // Reset the shoot timer
                    shootTimer = shootCooldown;
                }
            }
        }
    }
    void Shoot()
    {
        // Define the offset distance from the enemy where the projectile will spawn
        float spawnOffset = 1.5f;

        // Calculate the spawn position of the projectile slightly away from the enemy
        Vector3 spawnPosition = transform.position + (targetObject.transform.position - transform.position).normalized * spawnOffset;

        // Instantiate a projectile at the calculated spawn position
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Calculate direction towards the player
        Vector2 direction = (targetObject.transform.position - transform.position).normalized;

        // Set velocity of the projectile
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 10f;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            health--; // Decrease the health by 1
            hitCount++; // Increase the hit count

            if (health <= 0)
            {
                GameObject effectInstance = Instantiate(deathEffect, transform.position, Quaternion.identity); // Instantiate the death effect
                Destroy(gameObject); // Destroy the enemy object
                Destroy(effectInstance, 2.0f); // Destroy the death effect instance after 2 seconds (adjust as needed)
            }
            else
            {
                // Change the color of the enemy based on the hit count
                float redAmount = (float)hitCount / (float)health; // Calculate the ratio of hits to health
                spriteRenderer.color = Color.Lerp(Color.white, hitColor, redAmount); // Interpolate between white and hitColor based on the ratio
            }
        }
    }
}