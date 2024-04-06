using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Object references")]
    public Camera sceneCamera;
    public Rigidbody2D rb;
    public Weapon weapon;
    public HealthBar healthBar;
    public DamageUI damageUI;
    public AudioManager audioManager;
    Coroutine powerupTimerCoroutine;
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private bool sceneCameraError = false;
    private bool fullAuto = false;
    private bool speedBoostOnCooldown = false;
    private bool canFire = true;
    private bool homing = false;

    [Header("Player Stats")]
    public float moveSpeed;
    public int health = 100;

    public void Start()
    {
        if (healthBar != null)
            healthBar.SetHealth(100);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            rb.velocity = Vector2.zero;

        if (collision.gameObject.CompareTag("Powerup"))
        {
            audioManager.PlayPowerupPickupSound();
            int randomValue = Random.Range(0, 3);
            Destroy(collision.gameObject);

            if (randomValue == 0)
            {
                Debug.Log("Full Auto");
                fullAuto = true;
                if (powerupTimerCoroutine != null)
                    StopCoroutine(powerupTimerCoroutine);
                powerupTimerCoroutine = StartCoroutine(PowerupTimer());
            }
            else if (randomValue == 1)
            {
                Debug.Log("Speed Boost");
                moveSpeed = 10f;
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
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            TakeDamage(10);
            if (damageUI != null)
                damageUI.TakeDamage(10, 100);
        }
        if (collision.gameObject.CompareTag("projectile"))
        {
            TakeDamage(1);
            if (damageUI != null)
                damageUI.TakeDamage(1, 100);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(50);
            if (damageUI != null)
                damageUI.TakeDamage(50, 100);
        }
    }

    void Update()
    {
        ProcessInputs();
        if (Input.GetKeyDown(KeyCode.H))
            homing = !homing;
    }

    void FixedUpdate()
    {
        Move();
    }

    public void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0) && fullAuto && canFire)
            StartCoroutine(FireWithDelay());
        else if (Input.GetMouseButtonDown(0))
            weapon.Fire(homing);

        if (Input.GetKeyDown(KeyCode.Space) && !speedBoostOnCooldown)
        {
            if (audioManager != null)
                audioManager.PlayDashingSound();

            StartCoroutine(ActivateSpeedBoost(20f, 0.075f));
        }

        // Check if sceneCamera is null before using it
        if (sceneCamera == null)
        {
            sceneCameraError = true;
            return;
        }

        // Use sceneCamera safely
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool HasSceneCameraError()
    {
        return sceneCameraError;
    }

    // Reset the error flag
    public void ResetErrorFlags()
    {
        sceneCameraError = false;
    }


    IEnumerator ActivateSpeedBoost(float newSpeed, float duration)
    {
        speedBoostOnCooldown = true;
        float originalSpeed = moveSpeed;
        moveSpeed = newSpeed;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
        yield return new WaitForSeconds(2f);
        speedBoostOnCooldown = false;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    IEnumerator FireWithDelay()
    {
        canFire = false;
        weapon.Fire(homing);
        yield return new WaitForSeconds(0.05f);
        canFire = true;
    }

    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(10f);
        fullAuto = false;
        moveSpeed = 5f;
    }

    public void TakeDamage(int damage)
    {
        audioManager.PlayPlayerHitSound();
        health -= damage;
        if (healthBar != null)
            healthBar.SetHealth(health);
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        audioManager.PlayPlayerDeathSound();
        audioManager.StopBackgroundMusic();
        Destroy(gameObject);
        Time.timeScale = 0;
    }
}