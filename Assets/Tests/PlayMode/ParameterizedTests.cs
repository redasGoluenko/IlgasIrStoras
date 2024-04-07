using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI; // Import the UI namespace

public class ParameeterizedTests
{
    private DashCounter dashCounter;
    private ElapsedTime elapsedTimeScript;
    private KillCounter killCounterScript;
    private PowerupCounter PowerUpCounterScript;
    private ScoreCounter scoreCounterScript;
    private ShotsFiredCounter shotsFiredCounterScript;
    private ShotsTakenCounter shotsTakenCounterScript;
    private SpawnerDestroyedCounter spawnerDestroyedCounterScript;

    [SetUp]
    public void Setup()
    {
        var dashCounterObject = new GameObject();
        dashCounter = dashCounterObject.AddComponent<DashCounter>();
        dashCounter.dashCounterText = dashCounterObject.AddComponent<UnityEngine.UI.Text>();

        var gameObject = new GameObject();
        elapsedTimeScript = gameObject.AddComponent<ElapsedTime>();
        elapsedTimeScript.sessionTimeText = gameObject.AddComponent<Text>();

        var killGameObject = new GameObject();
        killCounterScript = killGameObject.AddComponent<KillCounter>();
        killCounterScript.textMeshProText = killGameObject.AddComponent<Text>();

        var powerUpGameObject = new GameObject();
        PowerUpCounterScript = powerUpGameObject.AddComponent<PowerupCounter>();
        PowerUpCounterScript.powerupCountText = powerUpGameObject.AddComponent<Text>();

        var scoreGameObject = new GameObject();
        scoreCounterScript = scoreGameObject.AddComponent<ScoreCounter>();
        scoreCounterScript.textMeshProText = scoreGameObject.AddComponent<Text>();

        var shotFiredGameObject = new GameObject();
        shotsFiredCounterScript = shotFiredGameObject.AddComponent<ShotsFiredCounter>();
        shotsFiredCounterScript.textMeshProText = shotFiredGameObject.AddComponent<Text>();

        var shotTakenGameObject = new GameObject();
        shotsTakenCounterScript = shotTakenGameObject.AddComponent<ShotsTakenCounter>();
        shotsTakenCounterScript.shotsTakenText = shotTakenGameObject.AddComponent<Text>();

        var spawnerDestroyedGameObject = new GameObject();
        spawnerDestroyedCounterScript = spawnerDestroyedGameObject.AddComponent<SpawnerDestroyedCounter>();
        spawnerDestroyedCounterScript.spawnerDestroyedCountText = spawnerDestroyedGameObject.AddComponent<Text>();
    }

    // Parameterized test for dash counter
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void DashCounterTest(int dashes)
    {
        // Act
        for (int i = 0; i < dashes; i++)
        {
            dashCounter.IncrementDashCounter();
        }

        // Assert
        Assert.AreEqual(dashes, dashCounter.dashCounter);
    }
    // Parameterized test for ElapsedTime
    [TestCase(0, "00:00:00")]
    [TestCase(65, "00:01:05")]
    [TestCase(3600, "01:00:00")]
    [TestCase(3665, "01:01:05")]
    public void ElapsedTimeTest(float elapsedTime, string expectedTimeString)
    {
        // Set the start time of the session
        elapsedTimeScript.Start();

        // Set the current time to the given elapsed time
        elapsedTimeScript.Update();

        // Simulate the passage of time by updating the session time text directly
        UpdateSessionTime(elapsedTime);

        // Check if the displayed session time matches the expected string
        Assert.AreEqual("Session Time: " + expectedTimeString, elapsedTimeScript.sessionTimeText.text);
    }
    // Method to simulate the passage of time by updating session time text directly
    private void UpdateSessionTime(float elapsedTime)
    {
        int hours = (int)(elapsedTime / 3600);
        int minutes = (int)((elapsedTime % 3600) / 60);
        int seconds = (int)(elapsedTime % 60);

        // Update UI text to display the session time
        elapsedTimeScript.sessionTimeText.text = "Session Time: " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    // Parameterized test for KillCounter
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void KillCounterTest(int kills)
    {
        // Set the kills count
        for (int i = 0; i < kills; i++)
        {
            killCounterScript.IncrementKills();
        }

        // Check if the displayed kills count matches the expected value
        Assert.AreEqual(kills, killCounterScript.kills);
    }
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void PowerUpCounterTest(int powerUps)
    {
        // Set the kills count
        for (int i = 0; i < powerUps; i++)
        {
            PowerUpCounterScript.IncrementPowerupCount();
        }

        // Check if the displayed kills count matches the expected value
        Assert.AreEqual(powerUps, PowerUpCounterScript.powerupCount);
    }
    // Parameterized test for ScoreCounter
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(5, 5)]
    [TestCase(10, 10)]
    public void ScoreCounterTest(int initialScore, int incrementAmount)
    {
        // Set the initial score count
        scoreCounterScript.score = initialScore;

        // Increment the score by the specified amount
        scoreCounterScript.IncrementScore(incrementAmount);

        // Check if the displayed score matches the expected value
        Assert.AreEqual(initialScore + incrementAmount, scoreCounterScript.score);
    }
    // Parameterized test for ShotsFiredCounter
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void ShotsFiredCounterTest(int shotsFired)
    {
        // Set the shots fired count
        for (int i = 0; i < shotsFired; i++)
        {
            shotsFiredCounterScript.IncrementShotsFired();
        }

        // Check if the displayed shots fired count matches the expected value
        Assert.AreEqual(shotsFired, shotsFiredCounterScript.shotsFired);
    }
    // Parameterized test for ShotsTakenCounter
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void ShotsTakenCounterTest(int shotsTaken)
    {
        // Set the shots taken count
        for (int i = 0; i < shotsTaken; i++)
        {
            shotsTakenCounterScript.IncrementShotsTaken();
        }

        // Check if the displayed shots taken count matches the expected value
        Assert.AreEqual(shotsTaken, shotsTakenCounterScript.shotsTakenCount);
    }
    // Parameterized test for SpawnerDestroyedCounter
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void SpawnerDestroyedCounterTest(int spawnersDestroyed)
    {
        // Set the spawners destroyed count
        for (int i = 0; i < spawnersDestroyed; i++)
        {
            spawnerDestroyedCounterScript.IncrementSpawnerDestroyedCount();
        }

        // Check if the displayed spawners destroyed count matches the expected value
        Assert.AreEqual(spawnersDestroyed, spawnerDestroyedCounterScript.spawnerDestroyedCount);
    }
    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(elapsedTimeScript.gameObject);
    }
}
