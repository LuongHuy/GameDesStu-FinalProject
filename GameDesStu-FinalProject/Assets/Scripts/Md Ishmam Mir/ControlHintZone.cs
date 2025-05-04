using UnityEngine;

public class ControlHintTrigger : MonoBehaviour
{
    public GameObject controlUI; // Assign your UI panel here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlUI.SetActive(false);
        }
    }
}
