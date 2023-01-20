using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    [SerializeField] private float highScore;

    public void UpdateGameoverScore(float score)
    {
        scoreText.text = $"SCORE: {score.ToString("00")}";
        CheckHighScore(score);
        highScoreText.text = $"HIGH SCORE: {Mathf.Round(PlayerPrefs.GetFloat("highscore"))}";
    }

    public void CheckHighScore(float score)
    {
        if(PlayerPrefs.HasKey("highscore"))
        {
            if(score > PlayerPrefs.GetFloat("highscore"))
            {
                highScore = score;
                PlayerPrefs.SetFloat("highscore", highScore);
                PlayerPrefs.Save();
            }
        }
        else
        {
            if(score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetFloat("highscore", highScore);
                PlayerPrefs.Save();
            }
        }
    }

    /*void ResetHighScore()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", 0);
            PlayerPrefs.Save();
        }
        else
        {
            return;
        }
    }*/
}
