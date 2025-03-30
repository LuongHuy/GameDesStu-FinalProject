using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    public Transform respawnPoint; // Assign in Inspector

    public void Respawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            Debug.Log("Player respawned at checkpoint.");
        }
        else
        {
            Debug.LogWarning("No respawn point assigned!");
        }
    }
}
