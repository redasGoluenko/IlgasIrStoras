
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool fullAuto = false;   
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    public Weapon weapon;
    Coroutine powerupTimerCoroutine;
    private int randomValue;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop player movement upon collision with a wall
            rb.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Powerup"))
        {
            randomValue = Random.Range(0,2);
            // Destroy the powerup GameObject upon collision with a wall
            Destroy(collision.gameObject);
            
            if(randomValue == 0)
            {
                fullAuto = true;
                // Start the coroutine to reset fullAuto after 10 seconds
                if (powerupTimerCoroutine != null)
                    StopCoroutine(powerupTimerCoroutine);
                powerupTimerCoroutine = StartCoroutine(PowerupTimer());
            }
            else if(randomValue == 1)
            {
                // Increase speed to 10f upon collision with a powerup
                moveSpeed = 10f;               
                // Start the coroutine to reset speed after 10 seconds
                if (powerupTimerCoroutine != null)
                    StopCoroutine(powerupTimerCoroutine);
                powerupTimerCoroutine = StartCoroutine(PowerupTimer());
            }
            
        }
    }

    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(10f); // Wait for 10 seconds
        fullAuto = false; // Reset fullAuto after 10 seconds
        moveSpeed = 5f; // Reset speed to 5f after 10 seconds
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

        if (Input.GetMouseButton(0) && fullAuto)
        {
            weapon.Fire();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }


        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

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
}
