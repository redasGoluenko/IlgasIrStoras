using UnityEngine;
using UnityEngine.UI;

public class ElapsedTime : MonoBehaviour
{
    public Text sessionTimeText; // Reference to the UI text element where you want to display the session time

    private float startTime; // Variable to store the time when the session started

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup; // Record the start time of the session
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the elapsed time since the session started
        float elapsedTime = Time.realtimeSinceStartup - startTime;

        // Convert elapsed time into hours, minutes, and seconds
        int hours = (int)(elapsedTime / 3600);
        int minutes = (int)((elapsedTime % 3600) / 60);
        int seconds = (int)(elapsedTime % 60);

        // Update UI text to display the session time
        sessionTimeText.text = "Session Time: " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}

