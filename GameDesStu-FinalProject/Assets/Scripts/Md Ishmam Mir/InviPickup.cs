using UnityEngine;

public class InvisibilityPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InvisibilityPower invisibility = other.GetComponent<InvisibilityPower>();
            if (invisibility != null)
            {
                invisibility.ActivateInvisibility();
            }

            Destroy(gameObject); // Pickup disappears after use
        }
    }
}
