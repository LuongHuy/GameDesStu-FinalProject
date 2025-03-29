using UnityEngine;

public class Scene3_FlyingEnemy_RightToLeft : MonoBehaviour
{
    public float speed = 2f;
    public float frequency = 2f;
    public float magnitude = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newX = startPos.x - speed * Time.time;
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * magnitude;
        transform.position = new Vector3(newX, newY, transform.position.z);

        if (transform.position.x < -20f) // Adjust for your scene bounds
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Scene3_PlayerController player = other.GetComponent<Scene3_PlayerController>();
            if (player != null)
            {
                player.SendMessage("TakeDamage");
            }
        }
    }
}
