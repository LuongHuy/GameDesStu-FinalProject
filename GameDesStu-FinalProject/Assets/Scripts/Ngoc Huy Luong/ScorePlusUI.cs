using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePlusUI : MonoBehaviour
{
    public TextMeshPro scoreText;   

    public void SetValue(string content, float duration)
    {
        var finalY = transform.position.y + 1;
        scoreText.text = content;
        transform.DOMoveY(finalY, duration).OnComplete(() => Destroy(gameObject));
    }
 

}
