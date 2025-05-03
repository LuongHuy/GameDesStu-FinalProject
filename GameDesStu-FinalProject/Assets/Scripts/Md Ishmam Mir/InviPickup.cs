using UnityEngine;

public class InvisibilityPickup : MonoBehaviour
{
    public AudioClip invulnerablePickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InvisibilityPower invisibility = other.GetComponent<InvisibilityPower>();
            if (invisibility != null)
            {
                invisibility.ActivateInvisibility();
            }

            AudioSource.PlayClipAtPoint(invulnerablePickupSound, transform.position);

            Destroy(gameObject); // Pickup disappears after use
        }
    }
}
