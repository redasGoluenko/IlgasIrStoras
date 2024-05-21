using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;


//Unit Tests for PlayerController class
public class PlayerTests
{
    private PlayerController playerController;

    //SetUp method for setting up necessary test environment
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

        
    }

    //Unit test for game start up
    [UnityTest]
    public IEnumerator PlayerStartHealthTest()
    { 
        Assert.AreEqual(100, playerController.health);
        yield return null;
        
    }
    //Unit test for player death
    [UnityTest]
    public IEnumerator PlayerDeathTest()
    {
        var initialHealth = playerController.health;
        var damage = initialHealth;

        playerController.TakeDamage(damage);
      
        yield return new WaitForEndOfFrame();
       
        if (playerController != null)
        {          
            Assert.Fail("Player object was not destroyed.");
        }
        else { Assert.Pass(); }
        
    }
    //Unit test for if player can take damage
    [UnityTest]
    public IEnumerator PlayerDamageTest()
    {      
        var initialHealth = playerController.health;
        var damage = 10;

        playerController.TakeDamage(damage);

        Assert.AreEqual(initialHealth - damage, playerController.health);
        yield return null;
       
    }
    //unit test for for firewithdelay
    [UnityTest]
    public IEnumerator FireWithDelay_CanFireIsTrueWhileFiring()
    {
        playerController.canFire = false;

        // Act
        playerController.FireWithDelay();
        yield return null;
        // Assert
        Assert.IsFalse(playerController.canFire);
    }
    //Unit test for the PowerupTimer method
    [UnityTest]
    public IEnumerator PowerupTimerTest()
    {
        Time.timeScale = 1;
        playerController.fullAuto = true;
        playerController.powerupTimerCoroutine = playerController.StartCoroutine(playerController.PowerupTimer());
      
        yield return new WaitForSeconds(10);
       
        Assert.IsFalse(playerController.fullAuto);
        
    }
    //Unit test for player movement
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
    // Unit test for SpeedBoost power-up
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
    //Unit test for fire force increase
    [UnityTest]
    public IEnumerator IncreaseFireForce_FireForceIncreasesAndResetsAfterDelay()
    {
        // Arrange
        GameObject weaponObject = new GameObject("Weapon");
        Weapon weapon = weaponObject.AddComponent<Weapon>();
      //  weapon.audioManager = new AudioManager(); // Mock audio manager
        weapon.fireForce = 10f;
        int amount = 5;

        // Act
        weapon.IncreaseFireForce(amount);

        // Assert (check initial increase)
        Assert.AreEqual(10f + amount, weapon.fireForce); // Ensure fire force increased immediately

        // Wait for reset
        yield return new WaitForSeconds(10.1f); // Wait for reset time

        // Assert (check reset)
        Assert.AreEqual(10f, weapon.fireForce); // Ensure fire force reset to initial value

        // Cleanup
        GameObject.Destroy(weaponObject);

       
    }
    //Teardown method for cleaning up resources after each test
    [TearDown]
    public void Teardown()
    {
        if (playerController != null)
        {
            GameObject.Destroy(playerController.gameObject);
        }
    }
}
