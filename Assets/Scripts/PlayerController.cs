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
    public Coroutine powerupTimerCoroutine;
    public AudioManager audioManager;
    public DashCounter dashCounter;
    public ShotsTakenCounter shotsTakenCounter;
    public PowerupCounter powerupCounter;
    public SpawnerDestroyedCounter spawnerDestroyedCounter;

    public Vector2 moveDirection;
    private Vector2 mousePosition;


    public bool fullAuto = false;
    private bool speedBoostOnCooldown = false; // Track if speed boost is on cooldown miau
    private int randomValue;
    public bool canFire = true; // Track if the player can fire 
    private bool homing = false;

    [Header("Player Stats")]
    public float moveSpeed;
    public int health = 100;

    public void Start()
    {
       healthBar.SetHealth(100);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemySpawner"))
        {
            spawnerDestroyedCounter.IncrementSpawnerDestroyedCount();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop player movement upon collision with a wall miau
            rb.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Powerup"))
        {
            powerupCounter.IncrementPowerupCount();
            audioManager.PlayPowerupPickupSound();
            randomValue = Random.Range(0, 3);
            // Destroy the powerup GameObject upon collision with a wall
            Destroy(collision.gameObject);

            if (randomValue == 0)
            {
                Debug.Log("Full Auto");
                fullAuto = true;
                // Start the coroutine to reset fullAuto after 10 seconds
                if (powerupTimerCoroutine != null)
                    StopCoroutine(powerupTimerCoroutine);
                powerupTimerCoroutine = StartCoroutine(PowerupTimer());
            }
            else if (randomValue == 1)
            {
                Debug.Log("Speed Boost");
                // Increase speed to 10f upon collision with a powerup
                moveSpeed = 10f;
                // Start the coroutine to reset speed after 10 seconds
                if (powerupTimerCoroutine != null)
                    StopCoroutine(powerupTimerCoroutine);
                powerupTimerCoroutine = StartCoroutine(PowerupTimer());
            }
            else if (randomValue == 2)
            {
                Debug.Log("Bullet Speed Increased");
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
            shotsTakenCounter.IncrementShotsTaken();
            TakeDamage(1);
            damageUI.TakeDamage(1, 100);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(50);
            damageUI.TakeDamage(50, 100);
        }   
    }

   

    // Update is called once per frame miau
    void Update()
    {
        ProcessInputs();
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(homing)
            {
                homing = false;
            }
            else
            {
                homing = true;
            }
        }
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
            weapon.Fire(homing);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !speedBoostOnCooldown) // Check if speed boost is not on cooldown miau
        {
            dashCounter.IncrementDashCounter();
            audioManager.PlayDashingSound();
            StartCoroutine(ActivateSpeedBoost(20f, 0.075f));
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public IEnumerator ActivateSpeedBoost(float newSpeed, float duration)
    {
        speedBoostOnCooldown = true; // Set speed boost to cooldown
        float originalSpeed = moveSpeed;
        moveSpeed = newSpeed;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
        yield return new WaitForSeconds(2f); // Cooldown duration
        speedBoostOnCooldown = false; // Reset speed boost cooldown
    }

    IEnumerator DisableHomingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        homing = false;
    }

    //turns image so the image is facing the direction of movement

    public void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //rotate player to face mouse
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
    public IEnumerator FireWithDelay()
    {
        canFire = false; // Set canFire to false to prevent firing multiple shots quickly miau
        weapon.Fire(homing);
        yield return new WaitForSeconds(0.05f); // Adjust this value to control the firing rate miau
        canFire = true; // Set canFire to true to allow firing again after the delay
    }

    public IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(10f); // Wait for 10 seconds
        fullAuto = false; // Reset fullAuto after 10 seconds
        moveSpeed = 5f; // Reset speed to 5f after 10 seconds miau
    }
    public void TakeDamage(int damage)
    {
        //audioManager.PlayPlayerHitSound();
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //audioManager.PlayPlayerDeathSound();
        //audioManager.StopBackgroundMusic();
        Destroy(gameObject);    
        //stop time
        Time.timeScale = 0;
        // miaumiau
        // mur mur
        // -Juste
        // miau
        // -Redas
    }
}
