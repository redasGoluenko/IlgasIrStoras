using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    private PlayerController playerController;
    private GameObject playerGameObject;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject to hold the PlayerController
        playerGameObject = new GameObject();

        // Add the PlayerController component to the GameObject
        playerController = playerGameObject.AddComponent<PlayerController>();

        // Set up other dependencies, such as Rigidbody2D
        playerController.rb = playerGameObject.AddComponent<Rigidbody2D>();

        // Set up other dependencies like sceneCamera, weapon, healthBar, etc. if necessary
    }

    [TearDown]
    public void TearDown()
    {
        // Destroy the GameObject after each test to clean up
        Object.Destroy(playerGameObject);
    }

    [UnityTest]
    public IEnumerator PlayerMovesWhenInputReceived()
    {
        // Ensure player object is initialized properly
        Assert.NotNull(playerController);

        // Set up player's move speed
        playerController.moveSpeed = 5f;

        Vector3 initialPosition = playerGameObject.transform.position;

        // Simulate input (e.g., pressing arrow keys)
        playerController.ProcessInputs();

        // Wait for physics update
        yield return new WaitForFixedUpdate();

        // Assert that the player has moved
        Assert.AreNotEqual(initialPosition, playerGameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator PlayerTakesDamageOnCollision()
    {
        // Ensure player object is initialized properly
        Assert.NotNull(playerController);

        // Set up player's health
        playerController.health = 100;

        // Simulate collision by directly calling TakeDamage method
        playerController.TakeDamage(10); // Simulating collision with an enemy projectile

        // Wait for a short time for the damage to be processed
        yield return new WaitForSeconds(0.1f);

        // Assert that player health is reduced
        Assert.Less(playerController.health, 100);
    }
    [Test]
    public void TestProcessInputsError()
    {
        // Set up the scene without assigning the sceneCamera

        PlayerController playerController = new PlayerController();
        playerController.ProcessInputs();

        Assert.IsTrue(playerController.HasSceneCameraError(), "Expected an error when sceneCamera is not assigned.");
    }

    [UnityTest]
    public IEnumerator PlayerDiesWhenHealthZero()
    {
        // Ensure player object is initialized properly
        Assert.NotNull(playerController);

        // Set up player's health
        playerController.health = 10;

        // Simulate taking damage that reduces health to zero
        playerController.TakeDamage(10);

        // Wait for a short time for the player to die
        yield return new WaitForSeconds(0.1f);

        // Assert that player object is destroyed
        Assert.IsNull(playerGameObject);
    }
}