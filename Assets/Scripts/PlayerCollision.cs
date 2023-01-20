using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    HealthPlayer playerHealth;
    HealthBar healthBar;

    AudioManager audioManager;

    private void Awake()
    {
        playerHealth = GetComponent<HealthPlayer>();
        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            playerHealth.UpdateHealth(25);

            healthBar.SetHealth(playerHealth.Health());

            audioManager.audioSource.PlayOneShot(audioManager.enemyHitAudioClip, audioManager.enemyHitClipVolume);
        }
        if (collision.gameObject.CompareTag("BigAsteroid"))
        {
            playerHealth.UpdateHealth(40);

            healthBar.SetHealth(playerHealth.Health());

            audioManager.audioSource.PlayOneShot(audioManager.enemyHitAudioClip, audioManager.enemyHitClipVolume);
        }
        if (collision.gameObject.CompareTag("EnemyWeapon"))
        {
            collision.gameObject.SetActive(false);
            playerHealth.UpdateHealth(25);

            healthBar.SetHealth(playerHealth.Health());

            audioManager.audioSource.PlayOneShot(audioManager.enemyHitAudioClip, audioManager.enemyHitClipVolume);
        }
        if (collision.gameObject.CompareTag("EnemyShip"))
        {
            playerHealth.UpdateHealth(25);

            healthBar.SetHealth(playerHealth.Health());

            audioManager.audioSource.PlayOneShot(audioManager.enemyHitAudioClip, audioManager.enemyHitClipVolume);
        }
    }
}
