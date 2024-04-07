using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsFiredCounter : MonoBehaviour
{
    private int shotsFired = 0;
    public Text textMeshProText; // Reference to the Text component
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textMeshProText.text = $"Shots Fired: {shotsFired}";
    }
    public void IncrementShotsFired()
    {
        shotsFired++;
    }
}
