using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace

public class DamageUI : MonoBehaviour
{
    // Reference to the Image component
    public Image overlay;
    private float damagePercentage = 0;

    // Start is called before the first frame update
    void Start()
    {
        overlay.color = new Color(1, 1, 1, damagePercentage); // Set the initial alpha value to 0
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage, int maxHealth)
    {
        // Calculate the percentage of damage taken relative to max health
        damagePercentage += (float)damage / (float)maxHealth;
        // Set the alpha value accordingly
        overlay.color = new Color(1, 1, 1, damagePercentage);
    }
    public void GainHealth(int health, int maxHealth)
    {
        // Calculate the percentage of health gained relative to max health
        damagePercentage -= (float)health / (float)maxHealth;
        // Set the alpha value accordingly
        overlay.color = new Color(1, 1, 1, damagePercentage);
    }
}
