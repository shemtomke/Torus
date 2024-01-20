using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public int score = 0;
    private int highScore = 0;

    private void Start()
    {
        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }
    private void Update()
    {
        scoreText.text = "" + score;

        // Check if the current score is higher than the stored high score
        if (score > highScore)
        {
            highScore = score;
            UpdateHighScoreText();

            // Save the new high score to PlayerPrefs
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
    private void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void ResetScore()
    {
        score = 0;

        // Update the UI text
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
