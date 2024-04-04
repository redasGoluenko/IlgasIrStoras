using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditTest
{
    
    HealthBar healthBar;
    // A Test behaves as an ordinary method
    [Test]
    public void EditTestSimplePasses()
    {
        float healthbarValue = healthBar.slider.value;
        
        
        Assert.Equals(0, healthbarValue);
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
}
