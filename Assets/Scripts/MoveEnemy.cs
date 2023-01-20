using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    // Speed of enemy
    [SerializeField]
    private float speed = 10.0f;
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
        }
        if (transform.position.y < screenBounds.y * 2 || transform.position.y > 10f)
        {
            gameObject.SetActive(false);
        }
    }
}
