using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextInit : MonoBehaviour
{
    public ScorePlusUI scorePrefab;
    public Transform startPosition;

    public void ShowScoreText(string content, float duration)
    {
        var text = Instantiate(scorePrefab);
        text.transform.position = startPosition.position;
        text.SetValue(content,duration);
    }
}
