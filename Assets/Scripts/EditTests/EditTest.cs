using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditTest
{

    HealthBar healthBar; // Declare a variable to hold a reference to the HealthBar script
    PlayerController playerController;
    private GameObject playerGameObject;

    [SetUp]
    public void SetUp()
    {
        // Find the HealthBar script attached to a GameObject in the scene
        healthBar = GameObject.FindObjectOfType<HealthBar>();

        playerController = GameObject.FindObjectOfType<PlayerController>();

        playerGameObject = new GameObject();
    }

    [Test]
    public void PlayerCanTakeDamage()
    {
        Assert.NotNull(healthBar);


        playerController.TakeDamage(10);

        //float healthbarValue = healthBar.slider.value;

        Assert.AreEqual(90, playerController.health);

    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator EditTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }


    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerGameObject);
    }
    [Test]
    public void PlayerDiesWhenHealthZero()
    {
        playerController.health = 0;

        playerController.TakeDamage(100); // Inflict damage to reduce health to zero

        Assert.IsTrue(playerGameObject == null);
    }
}
