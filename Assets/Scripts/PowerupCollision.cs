using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects the collisions/triggers between the player and a shield or powerup item
/// </summary>
public class PowerupCollision : MonoBehaviour
{
    [Header("Shield Pickup on Player")]
    [SerializeField] GameObject shield;
    [SerializeField] GameObject shieldFull;
    [SerializeField] GameObject shieldHalf;
    
    [SerializeField] BoxCollider playershipCollider;
    [SerializeField] BoxCollider powerupShieldActiveCollider;

    PlayerMovement playerMovement;
    GameObject uiLaser;
    GameObject uiShield;

    AudioManager audioManager;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playershipCollider = GetComponent<BoxCollider>();

        shield.SetActive(false);
        if(uiLaser == null)
        {
            uiLaser = GameObject.FindWithTag("UILaser");
            uiLaser.SetActive(false);
        }
        if (uiShield == null)
        {
            uiShield = GameObject.FindWithTag("UIShield");
            uiShield.SetActive(false);
        }

        audioManager = GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Shield"))
        {
            other.gameObject.SetActive(false);

            playershipCollider.enabled = false;
            powerupShieldActiveCollider.enabled = true;

            shield.SetActive(true);
            shieldFull.SetActive(true);
            shieldHalf.SetActive(false);
            uiShield.SetActive(true);

            audioManager.audioSource.PlayOneShot(audioManager.shieldActivatedAudioClip, audioManager.shieldActivatedClipVolume);
        }
        if(other.gameObject.CompareTag("Laser"))
        {
            other.gameObject.SetActive(false);
            playerMovement.HasPowerup(true);
            uiLaser.SetActive(true);

            audioManager.audioSource.PlayOneShot(audioManager.superLaserAudioClip, audioManager.superLaserClipVolume);
        }
    }
}
