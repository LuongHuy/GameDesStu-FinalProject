using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 10;
    public AudioClip coinPickupSound; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalGameManager.instance.AddScore(coinValue);

            if (coinPickupSound != null)
            {
                AudioSource.PlayClipAtPoint(coinPickupSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
