using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player Health: " + health);

        if (health <= 0)
        {
            Debug.Log("Player Died!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart scene
        }
    }
}
