using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    //Score
    [SerializeField] float score;
    //Getter method for score
    public float Score() { return score; }

    [SerializeField] float scoreIncreasedPerSecond;

    //Score Text
    public TextMeshProUGUI scoreText;

    public static bool gameIsPaused;

    public bool updateScore = true;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        if(updateScore)
        {
            score += scoreIncreasedPerSecond * Time.deltaTime;
            scoreText.text = score.ToString("00");
        }
    }

    public void UpdateScore(float newScore)
    {
        score += newScore;
        scoreText.text = score.ToString();
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
