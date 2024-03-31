using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject homingBullet;
    public Transform firePoint;
    public AudioManager audioManager;

    public float fireForce;
    public void Fire(bool homing)
    {
        if (!homing)
        {
            // Play the shooting sound
            audioManager.PlayShootingSound();
            GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * fireForce;
            Destroy(projectile, 2f);
        }
        else
        {
            FireHoming();
        }
    }
    public void FireHoming()
    {
        // Play the shooting sound
        audioManager.PlayHomingSound();
        GameObject projectile = Instantiate(homingBullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up, ForceMode2D.Impulse);       
    }
    public void IncreaseFireForce(int amount)
    {
        //increase fireForce by amount for 10 seconds
        fireForce += amount;
        StartCoroutine(ResetFireForce(amount));
    }
    IEnumerator ResetFireForce(int amount)
    {
        yield return new WaitForSeconds(10f);
        fireForce -= amount;
    }
}