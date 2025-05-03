using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BossHealth : MonoBehaviour
{
    //public LevelStatsManager statsManager;           // Reference to score manager
    //public Image[] winStars;                         // 3 star UI images
    //public Sprite filledStar;                        // Yellow star
    //public Sprite emptyStar;                         // Gray star

    //public TextMeshProUGUI finalScoreText;           // Score display on win screen

    [Header("Health Settings")]
    public float maxHealth = 100;
    private float currentHealth;
    private bool hasRegenerated = false;

    [Header("UI Elements")]
    public Image healthBar;
    public GameObject healthBarCover;
    //public GameObject endGameWinUI;

    [Header("Visual Effect")]
    public SpriteRenderer bossRenderer;

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

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.bossDefeated = true;
            }
        }

        // Show final score on UI
        //if (finalScoreText != null)
        //{
        //    finalScoreText.text = "Score: " + GlobalGameManager.instance.score;
        //}

        // Activate win panel
        //if (endGameWinUI != null)
        //{
        //    endGameWinUI.SetActive(true);
        //}

        // Show stars earned

        //int starsEarned = statsManager.CalculateStars(GlobalGameManager.instance.score);

        //for (int i = 0; i < winStars.Length; i++)
        //{
        //    winStars[i].sprite = i < starsEarned ? filledStar : emptyStar;
        //}

        // Save stars
        //statsManager.SaveStars(GlobalGameManager.instance.score);

        // Hide health bar cover if needed
        if (healthBarCover != null)
            healthBarCover.SetActive(false);

        // Optional: pause game
        //Time.timeScale = 0f;

        // Remove boss
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
