using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI; 

//Parameterized Tests vor various methods that can be test this way
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

    // Setup method to initialize the test environment before each test case
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
        elapsedTimeScript.Start();
        elapsedTimeScript.Update();

        UpdateSessionTime(elapsedTime);
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
        for (int i = 0; i < kills; i++)
        {
            killCounterScript.IncrementKills();
        }
        Assert.AreEqual(kills, killCounterScript.kills);
    }
    // Parameterized test for PowerUpCounter
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void PowerUpCounterTest(int powerUps)
    {
        for (int i = 0; i < powerUps; i++)
        {
            PowerUpCounterScript.IncrementPowerupCount();
        }
        Assert.AreEqual(powerUps, PowerUpCounterScript.powerupCount);
    }
    // Parameterized test for ScoreCounter
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(5, 5)]
    [TestCase(10, 10)]
    public void ScoreCounterTest(int initialScore, int incrementAmount)
    {
        scoreCounterScript.score = initialScore;
        scoreCounterScript.IncrementScore(incrementAmount);

        Assert.AreEqual(initialScore + incrementAmount, scoreCounterScript.score);
    }
    // Parameterized test for ShotsFiredCounter
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void ShotsFiredCounterTest(int shotsFired)
    {
        for (int i = 0; i < shotsFired; i++)
        {
            shotsFiredCounterScript.IncrementShotsFired();
        }

        Assert.AreEqual(shotsFired, shotsFiredCounterScript.shotsFired);
    }
    // Parameterized test for ShotsTakenCounter
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void ShotsTakenCounterTest(int shotsTaken)
    {
        for (int i = 0; i < shotsTaken; i++)
        {
            shotsTakenCounterScript.IncrementShotsTaken();
        }
        Assert.AreEqual(shotsTaken, shotsTakenCounterScript.shotsTakenCount);
    }
    // Parameterized test for SpawnerDestroyedCounter
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void SpawnerDestroyedCounterTest(int spawnersDestroyed)
    {
        for (int i = 0; i < spawnersDestroyed; i++)
        {
            spawnerDestroyedCounterScript.IncrementSpawnerDestroyedCount();
        }
        Assert.AreEqual(spawnersDestroyed, spawnerDestroyedCounterScript.spawnerDestroyedCount);
    }
    //Teardown method for cleaning up resources after each test
    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(elapsedTimeScript.gameObject);
    }
}
