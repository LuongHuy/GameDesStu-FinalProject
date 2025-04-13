using UnityEngine;
using System.Collections;

public class PlayerRespawner : MonoBehaviour
{
    public Transform respawnPoint; // Assign in Inspector

    public void Respawn()
    {
        StartCoroutine(DelayedRespawn());
    }

    private IEnumerator DelayedRespawn()
    {
        yield return new WaitForSeconds(0.5f); // 0.5 second delay before respawn

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            Debug.Log("Player respawned at checkpoint after delay.");
        }
        else
        {
            Debug.LogWarning("No respawn point assigned!");
        }
    }
}
