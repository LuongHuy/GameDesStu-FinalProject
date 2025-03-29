using UnityEngine;
using UnityEngine.UI;

public class GlobalUIManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;

    void Update()
    {
        if (GlobalGameManager.instance != null)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + GlobalGameManager.instance.score;
            }

            if (livesText != null)
            {
                livesText.text = "Lives: " + GlobalGameManager.instance.lives;
            }
        }
    }
}
