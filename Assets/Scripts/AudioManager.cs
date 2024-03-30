using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources ( for adjusting sounds )")]
    [SerializeField] private AudioSource movingSoundSource; // Reference to the AudioSource component for driving sound
    [SerializeField] private AudioSource backgroundMusicSource; // Reference to the AudioSource component for background music
    [SerializeField] private AudioSource shootingSoundSource;
    [SerializeField] private AudioSource playerHitSoundSource;
    [SerializeField] private AudioSource powerupPickupSoundSource;
    [SerializeField] private AudioSource dashingSoundSource;
    [SerializeField] private AudioSource playerDeathSoundSource;
    [SerializeField] private AudioSource enemyDeathSoundSource;
    [SerializeField] private AudioSource enemyHitSoundSource;
    [SerializeField] private AudioSource enemyShootingSoundSource; // Reference to the AudioSource component for enemy shooting sound
    [SerializeField] private AudioSource enemySpawnerDestroySoundSource; // Reference to the AudioSource component for enemy death sound
    [SerializeField] private AudioSource enemySpawnSoundSource;

    [Header("Audio Clips ( where the sounds go )")]
    public AudioClip driving; // Reference to the driving sound effect
    public AudioClip background; // Reference to the background music
    public AudioClip shooting; // Reference to the shooting sound effect
    public AudioClip playerHit; // Reference to the death sound effect
    public AudioClip powerupPickup; //Reference to the powerup pickup
    public AudioClip dashing;
    public AudioClip playerDeath;
    public AudioClip enemyDeath;
    public AudioClip enemyHit;
    public AudioClip enemyShooting; // Reference to the enemy shooting sound effect
    public AudioClip enemySpawnerDestroy;
    public AudioClip enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        // Set the audio clip for the driving sound effect
        movingSoundSource.clip = driving;

        // Set the audio clip for the background music and make it loop
        backgroundMusicSource.clip = background;
        backgroundMusicSource.loop = true;

        // Start playing the background music
        backgroundMusicSource.Play();
    }

    void Update()
    {
        // Check if W, A, S, or D keys are pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // Check if the sound is not already playing to avoid overlapping
            if (!movingSoundSource.isPlaying)
            {
                // Play the driving sound
                movingSoundSource.Play();
            }
        }
        else
        {
            // Stop the driving sound if none of the keys are pressed
            movingSoundSource.Stop();
        }
    }
    public void PlayShootingSound()
    {
        // Play the shooting sound
        shootingSoundSource.PlayOneShot(shooting);
    }
    public void PlayPlayerHitSound()
    {
        playerHitSoundSource.PlayOneShot(playerHit);
    }
    public void PlayPowerupPickupSound()
    {
        powerupPickupSoundSource.PlayOneShot(powerupPickup);
    }
    public void PlayDashingSound()
    {
        dashingSoundSource.PlayOneShot(dashing);
    }
    public void PlayPlayerDeathSound()
    {
        playerDeathSoundSource.PlayOneShot(playerDeath);
    }
    //make a method that would stop the background music
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }
    public void PlayEnemyDeathSound()
    {
        enemyDeathSoundSource.PlayOneShot(enemyDeath);
    }
    public void PlayEnemyHitSound()
    {
        enemyHitSoundSource.PlayOneShot(enemyHit);
    }
    public void PlayEnemyShootingSound()
    {
        enemyShootingSoundSource.PlayOneShot(enemyShooting);
    }
    public void PlayEnemySpawnerDestroySound()
    {
        enemySpawnerDestroySoundSource.PlayOneShot(enemySpawnerDestroy);
    }
    public void PlayEnemySpawnSound()
    {
        enemySpawnSoundSource.PlayOneShot(enemySpawn);
    }
}
