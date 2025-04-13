using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int scoreCount;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextInTotal;

    public ScoreTextInit playerScore;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        scoreText.text = scoreCount.ToString();
        scoreTextInTotal.text = scoreCount.ToString();

    }

    public void UpdateScore(int score)
    {
        scoreCount += score;
        scoreText.text = scoreCount.ToString();
        scoreTextInTotal.text = scoreCount.ToString();
        playerScore.ShowScoreText(score);
    }
}