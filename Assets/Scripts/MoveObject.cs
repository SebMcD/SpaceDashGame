using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Speed of object
    [SerializeField] public float speed = 7.5f;
    [SerializeField] PowerupItems powerupItemsScript;
    [SerializeField] float timeToStartSpawnMethod = 5f;
    
    private Rigidbody rb;

    // Screen Bounds - x and y values of screen
    private Vector2 screenBounds;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if (transform.position.x < screenBounds.x * 2)
        {
            gameObject.SetActive(false);

            powerupItemsScript.SpawnPickup(this.gameObject, timeToStartSpawnMethod);
        }
    }
}