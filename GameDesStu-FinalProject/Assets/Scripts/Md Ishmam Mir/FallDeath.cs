using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public float fallThresholdY = -10f;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (transform.position.y < fallThresholdY)
        {
            Debug.Log("Player fell!");
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(gameObject);
            }
        }
    }
}
