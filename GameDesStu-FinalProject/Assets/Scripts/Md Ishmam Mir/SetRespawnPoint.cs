using UnityEngine;

public class SetRespawnPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerRespawner>().respawnPoint = transform;
            Debug.Log(" Respawn point set to: " + gameObject.name);
        }
    }
}
