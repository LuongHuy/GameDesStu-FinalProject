using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public float fallThresholdY = -10f;

    private PlayerHealth playerHealth;
    private bool hasFallen = false;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (!hasFallen && transform.position.y < fallThresholdY)
        {
            hasFallen = true; // Prevents repeat damage

            Debug.Log("Player fell below threshold!");

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(gameObject);
            }

            // Optional: Reset the flag after delay so it works again later
            Invoke(nameof(ResetFall), 1f);
        }
    }

    void ResetFall()
    {
        hasFallen = false;
    }
}
