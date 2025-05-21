using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    public LevelStatsManager statsManager; // Assign in Inspector
    public TextMeshProUGUI highScoreText; // Optional: Assign if you want to show high score on screen

    // Enable this flag to reset the score once at level start
    public bool resetScoreOnStart = false;

    private void Start()
    {
        if (resetScoreOnStart)
        {
            ResetHighScore();
        }

        // Show saved high score on level start
        if (highScoreText != null && statsManager != null)
        {
            highScoreText.text = "Best Score: " + statsManager.GetHighScore().ToString();
        }
    }

    public void SaveCurrentScore()
    {
        if (statsManager != null)
        {
            statsManager.SaveHighScore(GlobalGameManager.instance.score);
        }
    }

    public void RefreshHighScoreUI()
    {
        if (highScoreText != null && statsManager != null)
        {
            highScoreText.text = "Best Score: " + statsManager.GetHighScore().ToString();
        }
    }

    public void ResetHighScore()
    {
        if (statsManager != null)
        {
            string key = "HighScore_Level_" + SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();

            if (highScoreText != null)
            {
                highScoreText.text = "Best Score: 0";
            }

            Debug.Log("High score reset for: " + key);
        }
    }
}
