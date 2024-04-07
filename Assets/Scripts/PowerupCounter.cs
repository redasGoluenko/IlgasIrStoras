using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupCounter : MonoBehaviour
{
    public int powerupCount = 0;
    public Text powerupCountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        powerupCountText.text = "Powerups: " + powerupCount;
    }
    public void IncrementPowerupCount()
    {
        powerupCount++;
    }
}
