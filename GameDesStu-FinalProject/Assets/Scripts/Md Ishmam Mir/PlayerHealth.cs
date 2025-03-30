using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Text livesText;

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
            RespawnAtCheckpoint();
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + GlobalGameManager.instance.lives;
        }
    }

    void RespawnAtCheckpoint()
    {
        PlayerRespawner respawner = GetComponent<PlayerRespawner>();
        if (respawner != null)
        {
            respawner.Respawn();
            GlobalGameManager.instance.lives = 3; // Optional: Reset lives after respawn
            UpdateLivesUI();
        }
        else
        {
            Debug.LogWarning("No PlayerRespawner found on Player!");
        }
    }
}
