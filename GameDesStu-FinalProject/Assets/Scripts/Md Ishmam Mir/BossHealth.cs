using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthSlider; // Assign in Inspector
    private bool hasRegenerated = false;

    public GameObject endGameWinUI; // assign in Inspector

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (!hasRegenerated && currentHealth <= 50)
        {
            RegenerateHealth(25);
            hasRegenerated = true;
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthUI();
    }

    void RegenerateHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("Boss regenerated 25 HP!");
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        if (endGameWinUI != null)
        {
            endGameWinUI.SetActive(true);
            Time.timeScale = 0f; // optional: pause the game
        }

        Destroy(gameObject); // or hide the boss
    }
}
