using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public Vector2 direction = Vector2.right;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("FlyingEnemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Boss"))
        {
            BossHealth boss = other.GetComponent<BossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(10);
            }

            Destroy(gameObject);
            return;
        }

        // Don't destroy if it's Player or Coin
        if (other.CompareTag("Player") || other.CompareTag("Coin") || other.CompareTag("Wall") || other.CompareTag("JetPack"))
        {
            return;
        }

        // Destroy for all other objects (e.g., walls)
        Destroy(gameObject);
    }
}
