using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100;
    private float currentHealth;
    private bool hasRegenerated = false;

    [Header("UI Elements")]
    public Image healthBar;          // Fill image
    public GameObject healthBarCover; // Cover/frame object
    public GameObject endGameWinUI;  // Set this in Inspector

    [Header("Visual Effect")]
    public SpriteRenderer bossRenderer; // For damage flash

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (healthBarCover != null)
            healthBarCover.SetActive(false);
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
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
        DamageFlash();
    }

    void RegenerateHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("Boss regenerated 25 HP!");
    }

    void DamageFlash()
    {
        if (bossRenderer != null)
        {
            bossRenderer.DOColor(Color.red, 0.2f).OnComplete(() =>
                bossRenderer.DOColor(Color.white, 0.2f));
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");

        if (endGameWinUI != null)
        {
            endGameWinUI.SetActive(true);
            Time.timeScale = 0f; // Optional: freeze game
        }

        if (healthBarCover != null)
            healthBarCover.SetActive(false);

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (healthBarCover != null)
        {
            healthBarCover.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (healthBarCover != null)
        {
            healthBarCover.SetActive(true);
        }
    }
}
