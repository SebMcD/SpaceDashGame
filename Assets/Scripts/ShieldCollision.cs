using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    [SerializeField] GameObject shieldFull;
    [SerializeField] GameObject shieldHalf;

    [SerializeField] PowerupItems powerupItemsScript;

    [SerializeField] BoxCollider playershipCollider;
    [SerializeField] BoxCollider powerupShieldActiveCollider;

    [SerializeField] AudioManager audioManager;

    GameObject uiShield;

    private void OnEnable()
    {
        powerupItemsScript = GameObject.Find("GameManager").GetComponent<PowerupItems>();
        if (uiShield == null)
        {
            uiShield = GameObject.FindWithTag("UIShield");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("BigAsteroid")|| collision.gameObject.CompareTag("EnemyWeapon"))
        {
            collision.gameObject.SetActive(false);
            if (shieldFull.activeInHierarchy)
            {
                shieldFull.SetActive(false);
                shieldHalf.SetActive(true);
            }
            else
            {
                uiShield.SetActive(false);
                gameObject.SetActive(false);
                powerupItemsScript.SpawnPickup(powerupItemsScript.ShieldPickup(), powerupItemsScript.ShieldPickupSpawnTime());

                playershipCollider.enabled = true;
                powerupShieldActiveCollider.enabled = false;
            }
            audioManager.audioSource.PlayOneShot(audioManager.shieldHitAudioClip, audioManager.shieldHitClipVolume);
        }
    }
}
