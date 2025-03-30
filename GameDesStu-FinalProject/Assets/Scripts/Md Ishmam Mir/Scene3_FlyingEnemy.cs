using UnityEngine;

public class FlyingEnemyStraight : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;

    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Flip target when enemy reaches one of the points
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InvisibilityPower invisibility = other.GetComponent<InvisibilityPower>();
            if (invisibility != null && invisibility.IsInvisible())
            {
                Debug.Log("Player is invisible — enemy collision ignored");
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
