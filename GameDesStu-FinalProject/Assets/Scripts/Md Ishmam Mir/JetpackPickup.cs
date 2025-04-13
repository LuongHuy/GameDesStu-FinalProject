using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    public GameObject messageUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Try to unlock the jetpack via the JetpackController component
            JetpackController jetpack = other.GetComponent<JetpackController>();
            if (jetpack != null)
            {
                jetpack.UnlockJetpack();
                Debug.Log("Jetpack unlocked via pickup.");
            }
            else
            {
                Debug.LogWarning("JetpackController not found on the player.");
            }

            // Hide the message UI immediately
            if (messageUI != null)
            {
                Debug.Log("Hiding message UI.");
                messageUI.SetActive(false);
            }
            else
            {
                Debug.LogWarning("messageUI not assigned in JetpackPickup.");
            }

            // Remove the pickup object
            Destroy(gameObject);
        }
    }
}
