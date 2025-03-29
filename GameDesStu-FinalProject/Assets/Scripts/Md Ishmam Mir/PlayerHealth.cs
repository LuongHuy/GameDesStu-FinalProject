using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public Text livesText;

    private GameObject lastHitSource;
    private float hitCooldown = 0.2f;
    private float lastHitTime = -1f;

    void Start()
    {
        currentLives = maxLives;

        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }

    public void TakeDamage(GameObject source)
    {
        // Ignore damage from the same source too quickly
        if (source == lastHitSource && Time.time - lastHitTime < hitCooldown)
        {
            Debug.Log("Ignored duplicate hit from: " + source.name);
            return;
        }

        // Update damage source
        lastHitSource = source;
        lastHitTime = Time.time;

        currentLives--;
        Debug.Log("Lives remaining: " + currentLives);

        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }

        if (currentLives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
