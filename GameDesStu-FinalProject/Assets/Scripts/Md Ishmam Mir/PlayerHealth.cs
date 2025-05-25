using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI livesText; // <- Use TextMeshProUGUI, not Text
    public GameObject endGameUI;
    public LevelStatsManager statsManager;

    private GameObject lastHitSource;
    private float lastHitTime = -1f;
    private float hitCooldown = 0.2f;

    void Start()
    {
        UpdateLivesUI();
    }

    public void TakeDamage(GameObject source)
    {
        if (source == lastHitSource && Time.time - lastHitTime < hitCooldown)
        {
            Debug.Log("Ignored duplicate hit from same source.");
            return;
        }

        lastHitSource = source;
        lastHitTime = Time.time;

        GlobalGameManager.instance.lives--;
        UpdateLivesUI();

        Debug.Log("Player hit! Lives left: " + GlobalGameManager.instance.lives);

        if (GlobalGameManager.instance.lives <= 0)
        {
            SaveHighScore();
            ShowGameOverUI();
        }
        else
        {
            RespawnAtCheckpoint();
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "LIVES: " + GlobalGameManager.instance.lives;
        }
    }

    void SaveHighScore()
    {
        if (statsManager != null)
        {
            GetComponent<ScoreTracker>()?.SaveCurrentScore();
        }
    }

    void RespawnAtCheckpoint()
    {
        PlayerRespawner respawner = GetComponent<PlayerRespawner>();
        if (respawner != null)
        {
            respawner.Respawn();
        }
        else
        {
            Debug.LogWarning("No PlayerRespawner found on Player!");
        }
    }

    void ShowGameOverUI()
    {
        Time.timeScale = 0f;
        if (endGameUI != null)
        {
            endGameUI.SetActive(true);
        }
    }
}