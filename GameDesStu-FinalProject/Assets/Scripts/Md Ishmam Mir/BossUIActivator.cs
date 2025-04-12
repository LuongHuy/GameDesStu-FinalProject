using UnityEngine;

public class BossUIActivator : MonoBehaviour
{
    public GameObject bossHealthUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (bossHealthUI != null)
            {
                bossHealthUI.SetActive(true);
                Debug.Log("Boss health UI activated!");
            }

            Destroy(gameObject); // Optional: remove the trigger after it's used
        }
    }
}
