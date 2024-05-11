using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOrb : MonoBehaviour
{
    public AudioManager audioManager;
    public PlayerController playerController;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            // Destroy the orb GameObject upon collision with a wall
            Destroy(gameObject);
            audioManager.PlayOrbDestroySound();
            playerController.Heal(12);
            playerController.orbCount++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
