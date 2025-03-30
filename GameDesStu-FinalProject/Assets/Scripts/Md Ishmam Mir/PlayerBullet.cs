using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public Vector2 direction = Vector2.right; // Set by player on spawn

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
        }

        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
