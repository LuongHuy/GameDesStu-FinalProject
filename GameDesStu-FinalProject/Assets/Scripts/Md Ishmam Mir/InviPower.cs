using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InvisibilityPower : MonoBehaviour
{
    public float invisibilityDuration = 15f;
    public float gracePeriod = 1.5f; // Time after invisibility ends where player is still safe
    public Text timerText; // Assign in Inspector

    private bool isInvisible = false;
    private bool inGracePeriod = false;
    private float remainingTime;

    public void ActivateInvisibility()
    {
        if (!isInvisible && !inGracePeriod)
        {
            isInvisible = true;
            remainingTime = invisibilityDuration;

            Debug.Log("Invisibility activated!");
            StartCoroutine(InvisibilityCountdown());
        }
    }

    public bool IsInvulnerable()
    {
        return isInvisible || inGracePeriod;
    }

    IEnumerator InvisibilityCountdown()
    {
        if (timerText != null)
            timerText.gameObject.SetActive(true);

        // Invisibility countdown
        while (remainingTime > 0f)
        {
            if (timerText != null)
                timerText.text = "Invulnerable: " + Mathf.CeilToInt(remainingTime);

            remainingTime -= Time.deltaTime;
            yield return null;
        }

        // Transition to grace period
        isInvisible = false;
        inGracePeriod = true;

        Debug.Log("Invisibility ended ? Back to Normal period started");

        float graceTimer = gracePeriod;

        // Grace period countdown
        while (graceTimer > 0f)
        {
            if (timerText != null)
                timerText.text = "Back to Normal: " + graceTimer.ToString("F1");

            graceTimer -= Time.deltaTime;
            yield return null;
        }

        // Done
        inGracePeriod = false;

        if (timerText != null)
        {
            timerText.text = "";
            timerText.gameObject.SetActive(false);
        }

        Debug.Log("Period ended ? Player vulnerable again");
    }
}
