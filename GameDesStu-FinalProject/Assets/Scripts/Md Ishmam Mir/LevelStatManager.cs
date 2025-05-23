using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelStatsManager : MonoBehaviour
{
    [Header("Level Info")]
    public string levelID = "Level1"; // change per scene
    public int maxScore = 100;

    [Header("Thresholds (percent)")]
    public int oneStarPercent = 30;
    public int twoStarPercent = 60;
    public int threeStarPercent = 90;

    public int CalculateStars(int currentScore)
    {
        float percent = ((float)currentScore / maxScore) * 100f;

        if (percent >= threeStarPercent)
            return 3;
        else if (percent >= twoStarPercent)
            return 2;
        else if (percent >= oneStarPercent)
            return 1;
        else
            return 0;
    }

    public void SaveStars(int currentScore)
    {
        int stars = CalculateStars(currentScore);
        int previousStars = PlayerPrefs.GetInt(levelID + "_stars", 0);

        // Only save if the new score is better
        if (stars > previousStars)
        {
            PlayerPrefs.SetInt(levelID + "_stars", stars);
            PlayerPrefs.Save();
        }
    }

    public void SaveHighScore(int currentScore)
    {
        string key = "HighScore_Level_" + SceneManager.GetActiveScene().buildIndex;
        int previousHigh = PlayerPrefs.GetInt(key, 0);

        if (currentScore > previousHigh)
        {
            PlayerPrefs.SetInt(key, currentScore);
            PlayerPrefs.Save();
        }
    }

    public int GetHighScore()
    {
        string key = "HighScore_Level_" + SceneManager.GetActiveScene().buildIndex;
        return PlayerPrefs.GetInt(key, 0);
    }


    public int GetStars()
    {
        return PlayerPrefs.GetInt(levelID + "_stars", 0);
    }
}
