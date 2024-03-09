using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if bullet collides with the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Calculate the reflection direction
            Vector2 normal = collision.GetContact(0).normal;
            Vector2 reflectDir = Vector2.Reflect(transform.right, normal).normalized;

            // Apply the reflection direction to the bullet's velocity
            GetComponent<Rigidbody2D>().velocity = reflectDir * 10; // Adjust 'speed' as needed
        }
    }
}
