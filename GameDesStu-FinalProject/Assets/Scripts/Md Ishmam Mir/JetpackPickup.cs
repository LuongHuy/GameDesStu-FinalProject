using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    public GameObject messageUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Scene2_PlayerController player = other.GetComponent<Scene2_PlayerController>();
            if (player != null)
            {
                player.UnlockJetpack();
            }

            if (messageUI != null)
            {
                messageUI.SetActive(true);
                // Hide after 4 seconds
                Destroy(messageUI, 4f);
            }

            Destroy(gameObject); // Remove pickup object
        }
    }
}
