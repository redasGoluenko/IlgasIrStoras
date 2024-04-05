using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI; // Import the UI namespace

public class PlayerTests
{
    private PlayerController playerController;
    private DamageUI damageUI;
    [SetUp]
    public void Setup()
    {
        var playerControllerGameObject = new GameObject();
        playerController = playerControllerGameObject.AddComponent<PlayerController>();
        playerController.rb = playerControllerGameObject.AddComponent<Rigidbody2D>();
        playerController.sceneCamera = playerControllerGameObject.AddComponent<Camera>();    
        
        var healthBarObject = new GameObject();
        var healthBar = healthBarObject.AddComponent<HealthBar>();
        healthBar.slider = healthBarObject.AddComponent<UnityEngine.UI.Slider>();
        playerController.healthBar = healthBar;

        var damageUIObject = new GameObject("DamageUI");
        damageUI = damageUIObject.AddComponent<DamageUI>();
        Image overlay = damageUIObject.AddComponent<Image>();
        damageUI.overlay = overlay;
    }

    [UnityTest]
    public IEnumerator PlayerDamageTest()
    {      
        var initialHealth = playerController.health;
        var damage = 10;

        playerController.TakeDamage(damage);

        Assert.AreEqual(initialHealth - damage, playerController.health);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerDeathTest()
    {
        var initialHealth = playerController.health;
        var damage = initialHealth;

        playerController.TakeDamage(damage);
      
        yield return null;
       
        if (playerController != null)
        {          
            Assert.Fail("Player object was not destroyed.");
        }
        else { Assert.Pass(); }
        
    }
    //implement a test for the PowerupTimer method
    [UnityTest]
    public IEnumerator PowerupTimerTest()
    {
        Time.timeScale = 1;
        playerController.fullAuto = true;
        playerController.powerupTimerCoroutine = playerController.StartCoroutine(playerController.PowerupTimer());
      
        yield return new WaitForSeconds(10);
       
        Assert.IsFalse(playerController.fullAuto);
    }
    // Add this test method to your PlayerTests class
    [UnityTest]
    public IEnumerator MoveTest()
    {     
        playerController.moveSpeed = 5f;
        Vector2 initialMoveDirection = playerController.moveDirection;
     
        playerController.Move();

        Assert.AreEqual(initialMoveDirection.x * playerController.moveSpeed, playerController.rb.velocity.x);
        Assert.AreEqual(initialMoveDirection.y * playerController.moveSpeed, playerController.rb.velocity.y);

        yield return null;
    }
    // Add this test method to your PlayerTests class
    [UnityTest]
    public IEnumerator ActivateSpeedBoostTest()
    {
        // Arrange
        float originalSpeed = playerController.moveSpeed;
        float boostSpeed = 10f;
        float boostDuration = 0.5f; // Duration of the speed boost in seconds

        // Act
        playerController.StartCoroutine(playerController.ActivateSpeedBoost(boostSpeed, boostDuration));

        // Wait for the boost duration
        yield return new WaitForSeconds(boostDuration);

        // Assert
        Assert.AreEqual(originalSpeed, playerController.moveSpeed); // Ensure speed is reset after boost duration
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
