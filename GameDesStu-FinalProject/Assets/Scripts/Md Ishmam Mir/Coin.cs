using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public int coinValue = 10;
    public AudioClip coinPickupSound;

    private bool collected = false; // Prevent double collection

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return; // avoid double trigger
        if (!other.CompareTag("Player")) return;

        collected = true;

        GlobalGameManager.instance.AddScore(coinValue);

        if (coinPickupSound != null)
            AudioSource.PlayClipAtPoint(coinPickupSound, transform.position);

        if (floatingTextPrefab != null)
        {
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.5f);
            GameObject textObj = Instantiate(floatingTextPrefab, screenPos, Quaternion.identity, canvas.transform);

            FloatingText ft = textObj.GetComponent<FloatingText>();
            if (ft != null)
                ft.SetText("+" + coinValue.ToString());
        }

        Destroy(gameObject);
    }
}
