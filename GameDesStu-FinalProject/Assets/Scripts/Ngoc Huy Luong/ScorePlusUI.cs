using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePlusUI : MonoBehaviour
{
    public TextMeshPro scoreText;   

    public void SetValue(int score)
    {
        var finalY = transform.position.y + 1;
        scoreText.text = "+" + score.ToString();
        transform.DOMoveY(finalY, 0.5f).OnComplete(() => Destroy(gameObject));
    }
 

}
