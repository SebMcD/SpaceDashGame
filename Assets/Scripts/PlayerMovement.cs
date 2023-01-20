using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float speed = 20f;
    [SerializeField] float weaponOffset = 5f;
    [SerializeField] float timeToAttack = 0.2f;
    [SerializeField] float timeToDash = 4f;
    [SerializeField] float dashForce = 2f;

    [Header("Powerup Button Settings")]
    [SerializeField] GameObject powerupObject;
    [SerializeField] float powerupActiveTime = 2f;

    Rigidbody rb;
    public PlayerActions playerActions;
    PowerupItems powerupItems;

    bool canAttack = true;
    bool canDash = true;
    bool hasPowerup = false;

    private Vector2 screenBounds;
    [Header("Player Height / Width")]
    [SerializeField] private float objectWidth;
    [SerializeField] private float objectHeight;

    AudioManager audioManager;

    public bool CanAttack(bool trueOrFalse) { canAttack = trueOrFalse; return canAttack; }
    public bool HasPowerup(bool powerup) { hasPowerup = powerup; return hasPowerup; }
    public bool HasDash(bool trueOrFalse) { canDash = trueOrFalse; return canDash; }

    GameObject uiLaser;
    GameObject uiDash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerActions = new PlayerActions();
        playerActions.PlayerControls.Enable();
        playerActions.PlayerControls.Attack.performed += Attack_performed;
        playerActions.PlayerControls.Dash.performed += Dash_performed;
        playerActions.PlayerControls.Powerup.performed += Powerup_performed;

        canAttack = true;
        canDash = true;

        hasPowerup = false;
        powerupObject.SetActive(false);
        uiLaser = GameObject.FindWithTag("UILaser");
        uiDash = GameObject.FindWithTag("UIDash");

        powerupItems = GameObject.Find("GameManager").GetComponent<PowerupItems>();

        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        uiDash.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerActions.PlayerControls.Movement.ReadValue<Vector2>();
        rb.AddForce(new Vector3(inputVector.x, inputVector.y, 0) * speed, ForceMode.Force);

        //World boundaries
        Boundaries();
    }

    private void Attack_performed(InputAction.CallbackContext context)
    {
        if (canAttack)
        {
            StartCoroutine(PreventSpamAttack());
            GameObject obj = ObjectPool.SharedInstance.GetPooledObject("Weapon");
            obj.GetComponent<Rigidbody>().velocity = new Vector2(15f, 0);
            obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            obj.transform.SetPositionAndRotation(new Vector3(transform.position.x + weaponOffset, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, -90f));
            obj.SetActive(true);
            audioManager.audioSource.PlayOneShot(audioManager.laserAudioClip, audioManager.laserClipVolume);
        }
    }

    private void Dash_performed(InputAction.CallbackContext context)
    {
        if (canDash)
        {
            StartCoroutine(PreventSpamDash());
            Vector2 inputVector = playerActions.PlayerControls.Movement.ReadValue<Vector2>();
            Vector2 noInput = new (0,0);
            if(inputVector == noInput)
            {
                rb.AddForce(new Vector3(1, 0, 0) * dashForce, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(new Vector3(inputVector.x, inputVector.y, 0) * dashForce, ForceMode.Impulse);
            }
            audioManager.audioSource.PlayOneShot(audioManager.dashAudioClip, audioManager.dashClipVolume);
        }
    }

    private void Powerup_performed(InputAction.CallbackContext context)
    {
        if (hasPowerup)
        {
            hasPowerup = false;

            //Turn on PowerLaser in coroutine and turn off after x seconds
            StartCoroutine(PowerupActiveTime());

            //Call SpawnPickup method for laser
            powerupItems.SpawnPickup(powerupItems.PowerupPickup(), powerupItems.PowerupPickupSpawnTime());

            //Turn off UILaser
            uiLaser.SetActive(false);

            audioManager.audioSource.PlayOneShot(audioManager.powerupAudioClip, audioManager.powerupClipVolume);
        }
        else if (!hasPowerup)
        {
            return;
        }
    }

    IEnumerator PreventSpamAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeToAttack);
        canAttack = true;
    }

    IEnumerator PreventSpamDash()
    {
        canDash = false;
        uiDash.SetActive(true);
        yield return new WaitForSeconds(timeToDash);
        canDash = true;
        uiDash.SetActive(false);
    }

    void Boundaries()
    {
        Vector3 pos = transform.position;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        pos.x = Mathf.Clamp(pos.x, screenPos.x + objectWidth, screenPos.x * -1 - objectWidth);
        pos.y = Mathf.Clamp(pos.y, screenPos.y + objectHeight, screenPos.y * -1 - objectHeight);
        Vector3 posToViewportPoint = Camera.main.WorldToViewportPoint(pos);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(posToViewportPoint);
    }

    IEnumerator PowerupActiveTime()
    {
        powerupObject.SetActive(true);
        yield return new WaitForSeconds(powerupActiveTime);
        powerupObject.SetActive(false);
    }
}
