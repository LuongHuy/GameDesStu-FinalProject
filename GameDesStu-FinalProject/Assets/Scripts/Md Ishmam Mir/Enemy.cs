using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA, pointB;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f;

    private Vector2 target;
    private bool movingToB = true;
    private float shootTimer;

    void Start()
    {
        target = pointB.position;
        shootTimer = shootInterval;
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        // Move enemy using Translate (no physics)
        float moveDirection = Mathf.Sign(target.x - transform.position.x);
        transform.Translate(Vector2.right * moveDirection * speed * Time.deltaTime);

        // Flip sprite
        transform.localScale = (target.x < transform.position.x)
            ? new Vector3(1, 1, 1)  // face left
            : new Vector3(-1, 1, 1); // face right

        // Switch direction at patrol points
        float distanceToTarget = Mathf.Abs(transform.position.x - target.x);
        if (distanceToTarget < 0.1f)
        {
            movingToB = !movingToB;
            target = movingToB ? pointB.position : pointA.position;
        }

        // Shoot at interval
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
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
