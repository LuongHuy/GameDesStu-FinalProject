using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int points = 0;
    public Text pointsText;

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
            pointsText.text = "Points: " + points;
        }
    }
}
