using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f;
    public Vector2 direction = Vector2.left;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InvisibilityPower invisibility = other.GetComponent<InvisibilityPower>();
            if (invisibility != null && invisibility.IsInvisible())
            {
                Debug.Log("Bullet ignored due to invisibility");
                Destroy(gameObject);
                return;
            }

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(this.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
