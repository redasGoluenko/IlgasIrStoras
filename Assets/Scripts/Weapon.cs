
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public AudioManager audioManager;

    public float fireForce;
    public void Fire()
    {
        // Play the shooting sound
        audioManager.PlayShootingSound();
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        Destroy(projectile, 2f);
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
