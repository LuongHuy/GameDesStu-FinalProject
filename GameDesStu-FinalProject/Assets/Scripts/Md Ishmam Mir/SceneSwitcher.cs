using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject currentScene;
    public GameObject nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentScene != null) currentScene.SetActive(false);
            if (nextScene != null) nextScene.SetActive(true);
        }
    }
}
