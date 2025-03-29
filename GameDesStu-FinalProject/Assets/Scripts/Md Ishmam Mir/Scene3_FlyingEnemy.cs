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
            Debug.Log("Flying enemy collided with player!");

            Scene3_PlayerController player = other.GetComponent<Scene3_PlayerController>();
            if (player != null)
            {
                player.SendMessage("TakeDamage");
                Debug.Log("TRIGGER HIT: " + other.name);
            }
        }
    }
}
