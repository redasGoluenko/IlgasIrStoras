using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCounter : MonoBehaviour
{
    public Text dashCounterText;
    private int dashCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dashCounterText.text = "Dash Counter: " + dashCounter;
    }
    public void IncrementDashCounter()
    {
        dashCounter++;
    }
}
