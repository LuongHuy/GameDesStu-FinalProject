using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InvisibilityPower : MonoBehaviour
{
    public float invisibilityDuration = 15f;
    public Text timerText; // UI Text element (assign in Inspector)

    private bool isInvisible = false;
    private float remainingTime;

    public void ActivateInvisibility()
    {
        if (!isInvisible)
        {
            isInvisible = true;
            remainingTime = invisibilityDuration;

            Debug.Log("Invisibility activated!");
            StartCoroutine(InvisibilityCountdown());
        }
    }

    public bool IsInvisible()
    {
        return isInvisible;
    }

    IEnumerator InvisibilityCountdown()
    {
        if (timerText != null)
            timerText.gameObject.SetActive(true);

        while (remainingTime > 0f)
        {
            if (timerText != null)
                timerText.text = "Invisibility: " + Mathf.CeilToInt(remainingTime);

            remainingTime -= Time.deltaTime;
            yield return null;
        }

        isInvisible = false;
        Debug.Log("Invisibility ended.");

        if (timerText != null)
        {
            timerText.text = "";
            timerText.gameObject.SetActive(false);
        }
    }
}
