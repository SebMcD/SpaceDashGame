using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    // Speed of weapon
    [SerializeField]
    public float speed = 15f;
    private Rigidbody rb;

    // Screen Bounds - x and y values of screen
    private Vector2 screenBounds;

    EnemyHealth bigAsteroidEnemyHealth;

    void OnEnable()
    {
        bigAsteroidEnemyHealth = null;
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        //Return weapon to Object Pool if it goes beyond screen size
        if (transform.position.x < screenBounds.x * 2)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroys enemy + Return weapon to object pool
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("BigAsteroid"))
        {
            //Find EnemyHealth.cs
            if (bigAsteroidEnemyHealth == null)
            {
                bigAsteroidEnemyHealth = GameObject.FindGameObjectWithTag("BigAsteroid").GetComponent<EnemyHealth>();

                if (bigAsteroidEnemyHealth != null)
                {
                    if (bigAsteroidEnemyHealth.EnemyHealthPoints > 1 && bigAsteroidEnemyHealth.EnemyHealthPoints <= 3)
                    {
                        bigAsteroidEnemyHealth.UpdateEnemyHealth(1);
                    }
                    else if (bigAsteroidEnemyHealth.EnemyHealthPoints == 1)
                    {
                        collision.gameObject.SetActive(false);
                    }
                }
            }
            gameObject.SetActive(false);
        }
    }
}
