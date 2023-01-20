using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Speed of weapon
    [SerializeField]
    public float speed = 15f;
    private Rigidbody rb;

    // Screen Bounds - x and y values of screen
    private Vector2 screenBounds;

    //Reference to Manager Script for score
    private ScoreManager scoreManager;

    GameObject enemy;
    EnemyHealth enemyHealth;
    EnemyHealth bigAsteroidEnemyHealth;
    GameObject gameManager;
    SpawnEnemyShip spawnEnemyShip;

    private void OnEnable()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.Find("GameManager");
            if(gameManager != null)
            {
                spawnEnemyShip = gameManager.GetComponent<SpawnEnemyShip>();
            }
        }
        if(enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("EnemyShip");
        }
        if(enemy != null)
        {
            enemyHealth = enemy.GetComponent<EnemyHealth>();
        }
        bigAsteroidEnemyHealth = null;
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = new Vector2(speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        //Return weapon to Object Pool if it goes beyond screen size
        if (transform.position.x > screenBounds.x * -2)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroys enemy + Return weapon to object pool
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            GameObject obj = ObjectPool.SharedInstance.GetPooledObject("AsteroidPS");
            if (obj != null)
            {
                obj.transform.SetPositionAndRotation(collision.gameObject.transform.position, Quaternion.identity);
                obj.SetActive(true);
            }
            scoreManager.UpdateScore(1f);
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("BigAsteroid"))
        {
            //Find EnemyHealth.cs
            if(bigAsteroidEnemyHealth == null)
            {
                bigAsteroidEnemyHealth = GameObject.FindGameObjectWithTag("BigAsteroid").GetComponent<EnemyHealth>();

                if(bigAsteroidEnemyHealth != null)
                {
                    if (bigAsteroidEnemyHealth.EnemyHealthPoints > 1 && bigAsteroidEnemyHealth.EnemyHealthPoints <= 3)
                    {
                        bigAsteroidEnemyHealth.UpdateEnemyHealth(1);
                    }
                    else if (bigAsteroidEnemyHealth.EnemyHealthPoints == 1)
                    {
                        scoreManager.UpdateScore(3f);
                        collision.gameObject.SetActive(false);
                    }
                }
            }
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("EnemyShip"))
        {
            //Add logic to remove health from enemy/detect if enemy is on last bit of health
            if(enemyHealth != null)
            {
                if(enemyHealth.EnemyHealthPoints > 1 && enemyHealth.EnemyHealthPoints <= 3)
                {
                    //Removes 1hp from enemy
                    enemyHealth.UpdateEnemyHealth(1);
                }
                else if(enemyHealth.EnemyHealthPoints == 1)
                {
                    scoreManager.UpdateScore(5f);
                    enemyHealth.UpdateEnemyHealth(1);
                    //Remove below line once animations added to EnemyGameObject
                    collision.gameObject.SetActive(false);

                    //Call method from SpawnEnemyShip to spawn next enemy ship
                    if (gameManager != null)
                    {
                        float randomSpawnRate = Random.Range(4f, 7f);
                        spawnEnemyShip.Invoke(nameof(spawnEnemyShip.SpawnEnemyShipAfterSeconds), randomSpawnRate);
                    }
                }
            }
            gameObject.SetActive(false);
        }
    }
}
