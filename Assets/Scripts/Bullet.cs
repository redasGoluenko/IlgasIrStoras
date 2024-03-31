
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Speed after bounce
    [SerializeField] private float bounciness = 0.8f; // Adjust bounciness (0 = no bounce, 1 = perfect bounce)  
    public float timeBeforeDestruction = 2f; // Time before the bullet is destroyed
    //Deals with the bullets collision response
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Calculate the reflection direction
            Vector2 normal = collision.GetContact(0).normal;
            Vector2 reflectDir = Vector2.Reflect(GetComponent<Rigidbody2D>().velocity.normalized, normal).normalized;

            // Apply the reflection direction to the bullet's velocity
            GetComponent<Rigidbody2D>().velocity = reflectDir * speed * bounciness;

            // Apply some randomness to simulate realistic bouncing
            float randomAngle = Random.Range(-15f, 15f); // Adjust angle randomness as needed
            reflectDir = Quaternion.Euler(0, 0, randomAngle) * reflectDir;
            GetComponent<Rigidbody2D>().velocity = reflectDir * speed * bounciness;
        }
        else if(collision.gameObject.CompareTag("PlayButton"))
        {
            SceneControl.Instance.NextScene();
        }     
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("projectile"))
        {
            Destroy(gameObject, 3f);
        }
        else
        {
        }
    }
}
