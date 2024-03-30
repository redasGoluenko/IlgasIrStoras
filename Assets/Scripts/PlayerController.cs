using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Object references")]
    public Camera sceneCamera;
    public Rigidbody2D rb;
    public Weapon weapon;
    public HealthBar healthBar;
    public DamageUI damageUI;
    Coroutine powerupTimerCoroutine;
    public AudioManager audioManager;

    private Vector2 moveDirection;
    private Vector2 mousePosition;


    private bool fullAuto = false;
    private bool speedBoostOnCooldown = false; // Track if speed boost is on cooldown
    private int randomValue;
    private bool canFire = true; // Track if the player can fire 

    [Header("Player Stats")]
    public float moveSpeed;
    public int health = 100;

    public void Start()
    {
        healthBar.SetHealth(100);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop player movement upon collision with a wall
            rb.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Powerup"))
        {
            audioManager.PlayPowerupPickupSound();
            randomValue = Random.Range(0, 3);
            // Destroy the powerup GameObject upon collision with a wall
            Destroy(collision.gameObject);

            if (randomValue == 0)
            {
                fullAuto = true;
                // Start the coroutine to reset fullAuto after 10 seconds
                if (powerupTimerCoroutine != null)
                    StopCoroutine(powerupTimerCoroutine);
                powerupTimerCoroutine = StartCoroutine(PowerupTimer());
            }
            else if (randomValue == 1)
            {
                // Increase speed to 10f upon collision with a powerup
                moveSpeed = 10f;
                // Start the coroutine to reset speed after 10 seconds
                if (powerupTimerCoroutine != null)
                    StopCoroutine(powerupTimerCoroutine);
                powerupTimerCoroutine = StartCoroutine(PowerupTimer());
            }
            else if (randomValue == 2)
            {
                weapon.IncreaseFireForce(20);
            }
        }
        if(collision.gameObject.CompareTag("EnemyProjectile"))
        {
            TakeDamage(10);
            damageUI.TakeDamage(10, 100);
        }
        if (collision.gameObject.CompareTag("projectile"))
        {
            TakeDamage(5);
            damageUI.TakeDamage(5, 100);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(50);
            damageUI.TakeDamage(50, 100);
        }   
    }

   

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0) && fullAuto && canFire) // Check if full auto and can fire
        {
            StartCoroutine(FireWithDelay());
        }
        else if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        if (Input.GetMouseButtonDown(1) && !speedBoostOnCooldown) // Check if speed boost is not on cooldown
        {
            audioManager.PlayDashingSound();
            StartCoroutine(ActivateSpeedBoost(20f, 0.075f));
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    IEnumerator ActivateSpeedBoost(float newSpeed, float duration)
    {
        speedBoostOnCooldown = true; // Set speed boost to cooldown
        float originalSpeed = moveSpeed;
        moveSpeed = newSpeed;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
        yield return new WaitForSeconds(2f); // Cooldown duration
        speedBoostOnCooldown = false; // Reset speed boost cooldown
    }

    //turns image so the image is facing the direction of movement

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //rotate player to face mouse
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
    IEnumerator FireWithDelay()
    {
        canFire = false; // Set canFire to false to prevent firing multiple shots quickly
        weapon.Fire();
        yield return new WaitForSeconds(0.05f); // Adjust this value to control the firing rate
        canFire = true; // Set canFire to true to allow firing again after the delay
    }

    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(10f); // Wait for 10 seconds
        fullAuto = false; // Reset fullAuto after 10 seconds
        moveSpeed = 5f; // Reset speed to 5f after 10 seconds
    }
    public void TakeDamage(int damage)
    {
        audioManager.PlayPlayerHitSound();
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        audioManager.PlayPlayerDeathSound();
        audioManager.StopBackgroundMusic();
        Destroy(gameObject);    
        //stop time
        Time.timeScale = 0;
    }
}
