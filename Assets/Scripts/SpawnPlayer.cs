using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    //Get Player prefab
    public GameObject playerPrefab;

    //Change transform.position of playerPrefab
    private Vector3 playerPos;

    // Spawn player prefab on startup
    void Start()
    {
        playerPos = new Vector3(-8, 0, 0);
        SpawnShip();
    }

    void SpawnShip()
    {
        Instantiate(playerPrefab, playerPos, playerPrefab.transform.rotation);
    }
}
