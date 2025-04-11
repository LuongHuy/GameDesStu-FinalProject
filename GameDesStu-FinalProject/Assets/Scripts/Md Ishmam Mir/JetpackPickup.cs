using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    public GameObject messageUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            JetpackController jetpack = other.GetComponent<JetpackController>();
            if (jetpack != null)
            {
                jetpack.UnlockJetpack();
            }

            if (messageUI != null)
            {
                messageUI.SetActive(true);
                Destroy(messageUI, 4f);
            }

            Destroy(gameObject); // Remove pickup object
        }
    }
}
