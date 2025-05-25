using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int points = 0;
    public TextMeshProUGUI pointsText; // <- Use TMP version here

    void Awake()
    {
        instance = this;
    }

    public void AddPoint(int value)
    {
        points += value;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (pointsText != null)
        {
            pointsText.text = "SCORE: " + points;
        }
    }
}