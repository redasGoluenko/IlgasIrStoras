using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System.Collections;
using Moq;
public class MockTest
{
    private PlayerController playerController;
    private Mock<Rigidbody2D> mockRigidbody2D;
    private Mock<Camera> mockCamera;
    private Mock<HealthBar> mockHealthBar;
    private Mock<Slider> mockSlider;
    [SetUp]
    public void Setup()
    {
       
        mockRigidbody2D = new Mock<Rigidbody2D>();
        mockCamera = new Mock<Camera>();
        mockHealthBar = new Mock<HealthBar>();
        mockSlider = new Mock<Slider>();

        var playerControllerGameObject = new GameObject();
        playerController = playerControllerGameObject.AddComponent<PlayerController>();
        playerController.rb = mockRigidbody2D.Object;
        playerController.sceneCamera = mockCamera.Object;

        var healthBarObject = new GameObject();
        playerController.healthBar = mockHealthBar.Object;
        mockHealthBar.Setup(hb => hb.slider).Returns(mockSlider.Object);
    }

    // Mock test for game startup
    [UnityTest]
    public IEnumerator PlayerStartHealthTest()
    {
        Assert.AreEqual(100, playerController.health);
        yield return null;
    }

    // Mock test for if the player can take damage
    [UnityTest]
    public IEnumerator PlayerDamageTest()
    {
        var initialHealth = playerController.health;
        var damage = 10;

        playerController.TakeDamage(damage);

        Assert.AreEqual(initialHealth - damage, playerController.health);
        yield return null;
    }

    // Mock test for FireWithDelay method
    [UnityTest]
    public IEnumerator FireWithDelay_CanFireIsTrueWhileFiring()
    {
        playerController.canFire = false;

        playerController.FireWithDelay();
        yield return null;

        Assert.IsFalse(playerController.canFire);
    }

    // Mock test for the PowerupTimer method
    [UnityTest]
    public IEnumerator PowerupTimerTest()
    {
        Time.timeScale = 1;
        playerController.fullAuto = true;
        playerController.powerupTimerCoroutine = playerController.StartCoroutine(playerController.PowerupTimer());

        yield return new WaitForSeconds(10);

        Assert.IsFalse(playerController.fullAuto);
    }

    // Mock test for player movement
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

    // Mock test for SpeedBoost power-up
    [UnityTest]
    public IEnumerator ActivateSpeedBoostTest()
    {
        float originalSpeed = playerController.moveSpeed;
        float boostSpeed = 10f;
        float boostDuration = 0.5f;

        playerController.StartCoroutine(playerController.ActivateSpeedBoost(boostSpeed, boostDuration));

        yield return new WaitForSeconds(boostDuration);

        Assert.AreEqual(originalSpeed, playerController.moveSpeed);
    }

    // Teardown method for cleaning up resources after each test
    [TearDown]
    public void Teardown()
    {
        if (playerController != null)
        {
            GameObject.Destroy(playerController.gameObject);
        }
    }
}

