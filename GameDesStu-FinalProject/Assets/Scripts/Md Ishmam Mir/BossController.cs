using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 3f;
    public Transform player;

    [Header("Movement")]
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public float timeMoveGapMin = 1f;
    public float timeMoveGapMax = 2.5f;

    private float shootTimer;
    private int currentPatrolIndex = 0;
    private Transform targetPoint;
    private bool isMoving = false;
    private float nextMoveTime = 0f;

    void Start()
    {
        shootTimer = shootInterval;

        if (patrolPoints.Length > 0)
        {
            targetPoint = patrolPoints[currentPatrolIndex];
        }

        nextMoveTime = Time.time + Random.Range(timeMoveGapMin, timeMoveGapMax);
    }

    void Update()
    {
        HandleShooting();
        HandleMovement();
    }

    void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void HandleMovement()
    {
        if (targetPoint == null || Time.time < nextMoveTime)
            return;

        isMoving = true;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            isMoving = false;

            // Pick next patrol point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            targetPoint = patrolPoints[currentPatrolIndex];

            // Set next move delay
            nextMoveTime = Time.time + Random.Range(timeMoveGapMin, timeMoveGapMax);
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
