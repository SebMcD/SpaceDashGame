using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField] float speed = 100f;
    [SerializeField] float ySpeed = 30f;
    [SerializeField] float xSpeed = 45f;
    [SerializeField] [Tooltip("Stops bouncing between yValue top and bottom bounds")] float yValueBuffer = 0.5f;
    [SerializeField] float xStoppingDistance = 10f;
    [SerializeField] float retreatDistance = 7.5f;
    [SerializeField] Transform player;

    float fireRate;
    [Header("Enemy Weapon")]
    [SerializeField] [Range(0, 1)] float fireRateMin;
    [SerializeField] [Range(1, 3)] float fireRateMax;
    [SerializeField] float weaponSpeed = 15f;
    [SerializeField] float weaponOffset = 2.4f;

    Rigidbody rb;

    Vector2 distanceToPlayer;

    bool canAttack = true;

    private void OnEnable()
    {
        canAttack = true;

        StartCoroutine(FireWeapon());
    }

    private void OnDisable()
    {
        canAttack = false;

        StopAllCoroutines();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Spaceship").transform;
    }

    private void Update()
    {
        if(player)
        {
            distanceToPlayer = player.position - transform.position;
        }
    }

    private void FixedUpdate()
    {
        if(player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > xStoppingDistance)
            {
                rb.AddForce(speed * Time.deltaTime * distanceToPlayer, ForceMode.Force);
            }
            //If y positions are greater or less than player y position move enemy ship in to player line of sight + adds yValueBuffer to stop bouncing
            else if (transform.position.y > player.transform.position.y + yValueBuffer)
            {
                rb.AddForce(new Vector3(0, -speed * ySpeed * Time.deltaTime, 0), ForceMode.Force);
            }
            else if (transform.position.y < player.transform.position.y - yValueBuffer)
            {
                rb.AddForce(new Vector3(0, speed * ySpeed * Time.deltaTime, 0), ForceMode.Force);
            }
            
            if (transform.position.x < player.position.x || Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                rb.AddForce(new Vector3(-speed * -xSpeed * Time.deltaTime, 0, 0), ForceMode.Force);
            }
        }
    }

    IEnumerator FireWeapon()
    {
        while (canAttack)
        {
            fireRate = Random.Range(fireRateMin, fireRateMax);

            GameObject obj = ObjectPool.SharedInstance.GetPooledObject("EnemyWeapon");

            if (obj != null)
            {
                obj.GetComponent<Rigidbody>().velocity = new Vector2(-weaponSpeed, 0);
                obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                //Add weapon offset to pos x
                obj.transform.SetPositionAndRotation(new Vector3(transform.position.x - weaponOffset, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, -90f));
                obj.SetActive(true);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
}
