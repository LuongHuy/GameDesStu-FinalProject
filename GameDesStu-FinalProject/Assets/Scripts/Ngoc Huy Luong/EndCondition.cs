using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCondition : MonoBehaviour
{
    public GameObject endGameUI;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            if (endGameUI != null)
            {
                endGameUI.SetActive(true);
            }
        }
    }
}
