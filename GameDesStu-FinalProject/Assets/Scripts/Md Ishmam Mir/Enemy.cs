using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA, pointB;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f;

    private Vector2 target;
    private bool movingToB = true;
    private float initialY;
    private float shootTimer;

    void Start()
    {
        target = pointB.position;
        initialY = transform.position.y;
        shootTimer = shootInterval;
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        // Movement
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        float distanceToTarget = Mathf.Abs(transform.position.x - target.x);
        if (distanceToTarget < 0.1f)
        {
            movingToB = !movingToB;
            target = movingToB ? pointB.position : pointA.position;
        }

        // Lock Y position
        transform.position = new Vector3(transform.position.x, initialY, transform.position.z);

        // Shooting
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }

        // Flip enemy sprite to face movement direction
        if (target.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1); // Facing left
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing right
        }

    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Flip bullet direction based on enemy scale
            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            if (bulletScript != null)
            {
                bulletScript.direction = (transform.localScale.x > 0) ? Vector2.left : Vector2.right;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
    }
}
