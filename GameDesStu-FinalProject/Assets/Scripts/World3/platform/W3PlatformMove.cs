using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3PlatformMove : MonoBehaviour
{
    [Header("DestinationList")]
    [SerializeField] List<Transform> destinations = new List<Transform>();

    [Header("parameter")]
    [SerializeField] float time= 1f;
    Vector3 velocity;

    Vector3 nextPos;
    int nextIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (destinations.Count <= 1 )
        {
            Debug.LogError("Minimum 2 destinations needed");
        }
        else
        {
            nextIndex = 0;
            nextPos = destinations[0].transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if arrived at location, move to the next location in the list
        if (Vector3.Distance(transform.position, nextPos) < 0.1f)
        {
            nextIndex = (nextIndex + 1)%destinations.Count;
            nextPos = destinations[nextIndex].transform.position;
        }

        transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, time);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

}
