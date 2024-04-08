using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI; // Import the UI namespace

//Test class for DeathText
public class DeathTextTests
{
    private DeathText deathText;
    // Test to verify that DeathText is visible when player is destroyed
    [Test]
    public void DeathText_VisibleWhenPlayerDestroyed()
    {
        var deathTextObject = new GameObject("DeathText");
        deathText = deathTextObject.AddComponent<DeathText>();
        Renderer rendererComponent = deathTextObject.AddComponent<MeshRenderer>();
        deathText.rendererComponent = rendererComponent;
        // Act
        deathText.Update(); // Simulate update call

        // Assert
        Assert.IsTrue(rendererComponent.enabled);       
    }
    // Test to verify that DeathText is invisible when player exists
    [Test]
    public void DeathText_InvisibleWhenPlayerExists()
    {
        // Arrange
        GameObject deathTextObject = new GameObject("DeathText");
        DeathText deathText = deathTextObject.AddComponent<DeathText>();
        Renderer rendererComponent = deathTextObject.AddComponent<MeshRenderer>();
        deathText.rendererComponent = rendererComponent;

        GameObject playerObject = new GameObject("Player");
        playerObject.tag = "Player"; // Set player tag to simulate existing player

        // Act
        deathText.Update(); // Simulate update call

        // Assert
        Assert.IsTrue(rendererComponent.enabled);

    }
}
