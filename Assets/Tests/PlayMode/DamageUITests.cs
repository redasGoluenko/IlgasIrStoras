using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI; // Import the UI namespace

//Test class for testing DamageUI
public class DamageUITests
{
    private DamageUI damageUI;
    //SetUp method for setting up necessary test environment
    [SetUp]
    public void Setup()
    {
        var damageUIObject = new GameObject("DamageUI");
        damageUI = damageUIObject.AddComponent<DamageUI>();
        Image overlay = damageUIObject.AddComponent<Image>();
        damageUI.overlay = overlay;
    }
    // Test method to verify if alpha value is updated correctly after taking damage
    [Test]
    public void TakeDamage_AlphaValueUpdatedCorrectly()
    {
        damageUI.Start();

        int damage = 20;
        int maxHealth = 100;

        damageUI.TakeDamage(damage, maxHealth);

        float expectedAlpha = (float)damage / (float)maxHealth;
        Assert.AreEqual(expectedAlpha, damageUI.overlay.color.a);
    }
    // Test method to verify if alpha value is updated correctly after gaining health
    [Test]
    public void GainHealth_AlphaValueUpdatedCorrectly()
    {
        damageUI.Start();

        int health = 20;
        int maxHealth = 100;

        // Act
        damageUI.GainHealth(health, maxHealth);

        // Assert
        float expectedAlpha = -(float)health / (float)maxHealth; // Alpha should decrease
        Assert.AreEqual(expectedAlpha, damageUI.overlay.color.a);
    }
}
