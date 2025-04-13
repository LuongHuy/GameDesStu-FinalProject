using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject bossToActivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (bossToActivate != null)
            {
                bossToActivate.SetActive(true);
                Debug.Log("Boss activated!");
            }

            Destroy(gameObject); // Optional: remove trigger after it's used
        }
    }
}
