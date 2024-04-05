using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI; // Import the UI namespace

public class DamageUITests
{
    private DamageUI damageUI;
    [SetUp]
    public void Setup()
    {
        var damageUIObject = new GameObject("DamageUI");
        damageUI = damageUIObject.AddComponent<DamageUI>();
        Image overlay = damageUIObject.AddComponent<Image>();
        damageUI.overlay = overlay;
    }
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
