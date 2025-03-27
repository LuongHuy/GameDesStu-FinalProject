using UnityEngine;
using UnityEngine.SceneManagement;

public class Artifact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the Player is tagged correctly
        {
            Debug.Log("You Win! Artifact Collected.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload scene for demo
        }
    }
}
