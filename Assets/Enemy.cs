using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public Transform targetObject; // The object to move towards
    public float moveSpeed = 5f; // The speed at which the sprite moves

    private void Update()
    {
        if (targetObject != null)
        {
            // Calculate the direction from the current position to the target object
            Vector3 direction = (targetObject.position - transform.position).normalized;

            // Move towards the target object
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {

            // Destroy the enemy when it collides with a projectile
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {

            // Destroy the enemy when it collides with a projectile
            Destroy(collision.gameObject);
        }
    }
}
