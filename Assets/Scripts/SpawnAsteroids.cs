using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;

    private Vector2 screenBounds;

    bool spawnBig = true;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        spawnRate = 1f;
        spawnBig = true;

        StartCoroutine(Asteroids());
        StartCoroutine(SpawnBigAsteroid());
    }

    //Rewrite coroutine
    IEnumerator Asteroids()
    {
        while (true)
        {
            if (spawnRate <= 0.1f)
            {
                spawnRate = 0.1f;
            }
            else if (spawnRate >= 0.11f && spawnRate <= 0.3f)
            {
                spawnRate -= 0.002f;
            }
            else
            {
                spawnRate -= 0.009f;
            }

            GameObject obj = ObjectPool.SharedInstance.GetPooledObject("Asteroid");
            if (obj != null)
            {
                //Reset velocity when recycled
                //Give random y velocity between -0.5, 0.5
                float randomYVelocity = Random.Range(-0.5f, 0.5f);
                obj.GetComponent<Rigidbody>().velocity = new Vector2(-10f, randomYVelocity);

                obj.transform.SetPositionAndRotation(new Vector2(screenBounds.x * -2, Random.Range(-screenBounds.y, screenBounds.y)), Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
                obj.SetActive(true);
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator SpawnBigAsteroid()
    {
        while (spawnBig)
        {
            //Get Random Wait Time each run
            float randomWaitTime = Random.Range(7f, 15f);

            GameObject obj = ObjectPool.SharedInstance.GetPooledObject("BigAsteroid");
            if (obj != null)
            {
                //Reset velocity when recycled
                //Give random y velocity between -0.5, 0.5
                float randomYVelocity = Random.Range(-0.2f, 0.2f);
                obj.GetComponent<Rigidbody>().velocity = new Vector2(-5f, randomYVelocity);

                obj.transform.SetPositionAndRotation(new Vector2(screenBounds.x * -2, Random.Range(-screenBounds.y, screenBounds.y)), Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
                obj.SetActive(true);
            }
            yield return new WaitForSeconds(randomWaitTime);
        }
    }
}
