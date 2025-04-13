using UnityEngine;

public class JetpackTextTrigger : MonoBehaviour
{
    public GameObject jetpackUIText; // Assign your text UI in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && jetpackUIText != null)
        {
            jetpackUIText.SetActive(true);
        }
    }
}
