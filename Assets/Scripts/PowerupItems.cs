using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupItems : MonoBehaviour
{
    [SerializeField] GameObject shieldPickup;
    [SerializeField] GameObject powerupPickup;
    [SerializeField] float shieldPickupSpawnTime = 5f;
    [SerializeField] float powerupPickupSpawnTime = 10f;

    [Header("Velocity Settings")]
    [SerializeField] [Range(7f,15f)] float minVelocity = 7f;
    [SerializeField] [Range(7f, 15f)] float maxVelocity = 15f;
    [SerializeField] private float velocityRb;

    public GameObject ShieldPickup() { return shieldPickup; }

    public GameObject PowerupPickup() { return powerupPickup; }

    public float ShieldPickupSpawnTime() { return shieldPickupSpawnTime; }

    public float PowerupPickupSpawnTime() { return powerupPickupSpawnTime; }

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        SpawnPickup(shieldPickup, shieldPickupSpawnTime);
        SpawnPickup(powerupPickup, powerupPickupSpawnTime);
    }

    public void SpawnPickup(GameObject gameobject, float seconds)
    {
        StartCoroutine(StartTimer(gameobject, seconds));
    }

    IEnumerator StartTimer(GameObject gameobject, float seconds)
    {
        velocityRb = Random.Range(minVelocity, maxVelocity);

        yield return new WaitForSeconds(seconds);

        gameobject.GetComponent<Rigidbody>().velocity = new Vector2(-velocityRb, 0);
        gameobject.transform.SetPositionAndRotation(new Vector2(screenBounds.x * -2, Random.Range(-screenBounds.y, screenBounds.y + 0.7f)), Quaternion.identity);
        gameobject.SetActive(true);
    }
}
