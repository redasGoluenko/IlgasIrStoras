using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsTakenCounter : MonoBehaviour
{
    public Text shotsTakenText; // Reference to the UI text element where you want to display the shots taken
    private int shotsTakenCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotsTakenText.text = "Shots Taken: " + shotsTakenCount.ToString();
    }
    public void IncrementShotsTaken()
    {
        shotsTakenCount++;
    }
}
