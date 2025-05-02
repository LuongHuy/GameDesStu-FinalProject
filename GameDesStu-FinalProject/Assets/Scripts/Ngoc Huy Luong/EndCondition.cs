using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class EndCondition : MonoBehaviour
{
    public GameObject endGameUI;
    public LevelStatsManager statsManager;
    public Image[] winStars;                        
    public Sprite filledStar;                        
    public Sprite emptyStar;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            if (endGameUI != null)
            {
                endGameUI.SetActive(true);
                // Show stars earned
                int starsEarned = statsManager.CalculateStars(ScoreManager.instance.scoreCount);

                for (int i = 0; i < winStars.Length; i++)
                {
                    winStars[i].sprite = i < starsEarned ? filledStar : emptyStar;
                }

                // Save stars
                statsManager.SaveStars(ScoreManager.instance.scoreCount);
            }

        }
    }

}
