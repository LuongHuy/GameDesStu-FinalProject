using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    public int maxLife;
    public int curLife;
    public GameObject endgameUI;
    public TextMeshProUGUI lifeText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        curLife = maxLife;
        lifeText.text = curLife.ToString();
    }

    public void DecreaseLive()
    {
        curLife--;
        lifeText.text = curLife.ToString();
        if (curLife <= 0)
        {
            endgameUI.SetActive(true);
        }
    }
}
