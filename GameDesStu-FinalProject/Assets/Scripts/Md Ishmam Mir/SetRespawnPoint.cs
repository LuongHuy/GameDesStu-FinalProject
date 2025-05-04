using UnityEngine;

public class SetRespawnPoint : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            // Set respawn point
            other.GetComponent<PlayerRespawner>().respawnPoint = transform;
            Debug.Log("Respawn point set to: " + gameObject.name);

            // Show floating text
            if (floatingTextPrefab != null)
            {
                Canvas canvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();
                if (canvas != null)
                {
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.5f);
                    GameObject textObj = Instantiate(floatingTextPrefab, screenPos, Quaternion.identity, canvas.transform);

                    FloatingText ft = textObj.GetComponent<FloatingText>();
                    if (ft != null)
                        ft.SetText("Checkpoint Reached!");
                }
            }
        }
    }
}
