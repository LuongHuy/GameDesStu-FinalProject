using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalGameManager.instance.AddScore(coinValue);
            Destroy(gameObject);
        }
    }
}
