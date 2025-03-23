using UnityEngine;

public class EnemyAMovingControl : MonoBehaviour
{
    public float moveSpeed = 2f; 
    public float moveDistance = 5f; 

    private Vector2 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float targetX = movingRight ? startPosition.x + moveDistance : startPosition.x - moveDistance;

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetX, transform.position.y), moveSpeed * Time.deltaTime);

        if (transform.position.x == targetX)
        {
            movingRight = !movingRight;
        }
    }
}