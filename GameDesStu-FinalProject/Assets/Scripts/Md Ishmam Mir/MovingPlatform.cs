using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private bool movingToB = true;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("pointA or pointB is not assigned.");
            enabled = false;
            return;
        }

        Debug.Log("Starting platform movement. From: " + pointA.position + " To: " + pointB.position);
    }

    void Update()
    {
        Vector3 target = movingToB ? pointB.position : pointA.position;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == target)
        {
            movingToB = !movingToB;
            Debug.Log("Switched direction: " + (movingToB ? "to pointB" : "to pointA"));
        }
    }


    private void OnDrawGizmos()
    {
        if (pointA != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointA.position, 0.15f);
        }

        if (pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointB.position, 0.15f);
        }

        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
