using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f;
    public float maxRange = 15f;
    public Vector2 direction = Vector2.left;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifetime); //z Time-based fallback
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Distance-based range check
        float distance = Vector3.Distance(transform.position, startPosition);
        if (distance >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InvisibilityPower invisibility = other.GetComponent<InvisibilityPower>();
            if (invisibility != null && invisibility.IsInvulnerable())
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

        // Hit a solid object like Ground or Platform
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
