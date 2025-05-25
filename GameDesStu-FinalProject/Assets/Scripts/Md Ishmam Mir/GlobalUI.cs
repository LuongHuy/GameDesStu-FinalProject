using UnityEngine;
using TMPro;

public class GlobalUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    void Update()
    {
        if (GlobalGameManager.instance != null)
        {
            if (scoreText != null)
            {
                scoreText.text = "SCORE: " + GlobalGameManager.instance.score;
            }

            if (livesText != null)
            {
                livesText.text = "LIVES: " + GlobalGameManager.instance.lives;
            }
        }
    }
}
