using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXSource; // Reference to the AudioSource component
    public AudioClip driving; // Reference to the driving sound effect

    // Start is called before the first frame update
    void Start()
    {
        SFXSource.clip = driving; // Set the audio clip to the driving sound effect
    }

    void Update()
    {
        // Check if W, A, S, or D keys are pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // Check if the sound is not already playing to avoid overlapping
            if (!SFXSource.isPlaying)
            {
                // Play the driving sound
                SFXSource.Play();
            }
        }
        else
        {
            // Stop the sound if none of the keys are pressed
            SFXSource.Stop();
        }
    }
}
