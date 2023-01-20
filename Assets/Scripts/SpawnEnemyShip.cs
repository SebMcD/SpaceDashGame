using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyShip : MonoBehaviour
{
    [SerializeField] GameObject enemyShipGameObject;
    [SerializeField] Vector3 enemyShipRotation = new Vector3(0, 0, 0);
    [SerializeField] float spawnRate = 5f;

    private Vector2 screenBounds;

    private void Awake()
    {
        enemyShipGameObject.SetActive(false);
    }

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        Invoke(nameof(SpawnEnemyShipAfterSeconds), spawnRate);
    }

    public void SpawnEnemyShipAfterSeconds()
    {
        enemyShipGameObject.transform.SetPositionAndRotation(new Vector2(screenBounds.x * -2, Random.Range(-screenBounds.y, screenBounds.y)), Quaternion.Euler(enemyShipRotation));
        enemyShipGameObject.SetActive(true);
    }
}
