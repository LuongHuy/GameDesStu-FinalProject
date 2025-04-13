using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 3f;
    public Transform player;

    private float shootTimer;

    void Start()
    {
        shootTimer = shootInterval;
    }

    void Update()
    {
        // Shooting logic only
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();

            if (bulletScript != null)
            {
                Vector2 directionToPlayer = (player.position - firePoint.position).normalized;
                bulletScript.direction = directionToPlayer;
            }

            Debug.Log("Boss shot toward player!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InvisibilityPower invisibility = other.GetComponent<InvisibilityPower>();
            if (invisibility != null && invisibility.IsInvulnerable())
            {
                Debug.Log("Player invisible — boss collision ignored");
                return;
            }

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(this.gameObject);
            }
        }
    }
}
