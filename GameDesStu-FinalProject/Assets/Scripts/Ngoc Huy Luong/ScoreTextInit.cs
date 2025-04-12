using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextInit : MonoBehaviour
{
    public ScorePlusUI scorePrefab;
    public Transform startPosition;

    public void ShowScoreText(int score)
    {
        var text = Instantiate(scorePrefab);
        text.transform.position = startPosition.position;
        text.SetValue(score);
    }
}
