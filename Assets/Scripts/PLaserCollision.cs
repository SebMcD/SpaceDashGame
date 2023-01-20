using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLaserCollision : MonoBehaviour
{
    ScoreManager scoreManager;

    GameObject gameManager;
    SpawnEnemyShip spawnEnemyShip;

    private void OnEnable()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager");
            scoreManager = gameManager.GetComponent<ScoreManager>();
            if (gameManager != null)
            {
                spawnEnemyShip = gameManager.GetComponent<SpawnEnemyShip>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            other.gameObject.SetActive(false);

            scoreManager.UpdateScore(1);
        }
        if (other.gameObject.CompareTag("BigAsteroid"))
        {
            other.gameObject.SetActive(false);

            scoreManager.UpdateScore(3);
        }
        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("EnemyShip"))
        {
            other.gameObject.SetActive(false);

            scoreManager.UpdateScore(5);

            //Call method from SpawnEnemyShip to spawn next enemy ship
            if (gameManager != null)
            {
                float randomSpawnRate = Random.Range(4f, 7f);
                spawnEnemyShip.Invoke(nameof(spawnEnemyShip.SpawnEnemyShipAfterSeconds), randomSpawnRate);
            }
        }
    }
}
