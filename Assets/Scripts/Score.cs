using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public TextMeshProUGUI highScoreTMP;

    private float score;

    public SpawnCubes spawnCubes;

    private void Start()
    {
        float tempHighScore = PlayerPrefs.GetFloat("highScore");
        highScoreText.text = "HighScore: " + tempHighScore;

        highScoreTMP = GetComponent<TextMeshProUGUI>();
        highScoreTMP.text = highScoreText.text;
    }

    private void Update()
    {
        // Update the logic to use the totalPoints from the ScoreManager script
        int managerTotalPoints = ScoreManager.totalPoints;

        

        // Update the score text with the totalPoints
        scoreText.text = "Score: " + managerTotalPoints;

        // Update high score if the current score is higher
        float tempScore = PlayerPrefs.GetFloat("highScore");
        if (managerTotalPoints > tempScore)
        {
            PlayerPrefs.SetFloat("highScore", managerTotalPoints);
            highScoreText.text = "HighScore: " + managerTotalPoints;
            highScoreTMP.text = highScoreText.text;
        }

        // Reset high score
        if (Input.GetKeyDown("p"))
        {
            PlayerPrefs.SetFloat("highScore", 0f);
            highScoreText.text = "HighScore: 0";
            highScoreTMP.text = highScoreText.text;
        }
    }
}
