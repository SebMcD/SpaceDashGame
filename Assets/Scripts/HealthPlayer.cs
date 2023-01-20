using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] int health = 100;
    public int Health() { return health; }

    GameObject gameManager;

    GameObject gameCanvas;
    GameObject gameOverCanvas;
    GameOverScreen gameOverScreen;
    ScoreManager scoreManager;
    PowerupItems powerupItems;

    PlayerMovement playerMovement;

    private void Awake()
    {
        gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");

        gameOverCanvas = GameObject.FindGameObjectWithTag("GameOverCanvas");
        gameOverCanvas.SetActive(false);
        gameOverScreen = gameOverCanvas.GetComponent<GameOverScreen>();

        gameManager = GameObject.Find("GameManager");
        scoreManager = gameManager.GetComponent<ScoreManager>();
        powerupItems = gameManager.GetComponent<PowerupItems>();

        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        health = 100;
    }

    public void UpdateHealth(int damageTaken)
    {
        health -= damageTaken;
        if(health <= 0)
        {
            //Set GameCanvas to false
            gameCanvas.SetActive(false);

            gameOverCanvas.SetActive(true);
            gameOverScreen.UpdateGameoverScore(scoreManager.Score());

            //Set shield + powerups to inactive
            powerupItems.ShieldPickup().SetActive(false);
            powerupItems.PowerupPickup().SetActive(false);


            health = 0;
            //Disable player
            playerMovement.playerActions.PlayerControls.Disable();

            //Stop score from updating
            scoreManager.updateScore = false;

            //
            StartCoroutine(TimeScaleWaitTime());
        }
    }

    IEnumerator TimeScaleWaitTime()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
}
