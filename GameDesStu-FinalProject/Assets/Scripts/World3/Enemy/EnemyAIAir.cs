using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        Act();
    }

    protected abstract void Act();
} 

public class EnemyAIAir : EnemyAI
{
    [Header("Boundary")]
    [SerializeField] List<Transform> destinations = new List<Transform>();

    [Header("Parameter")]
    [SerializeField] float speed = 1f;
    [SerializeField] Rigidbody2D rd;
    [SerializeField] bool stationary = false;

    // is the element facing right
    bool facingRight = true;
    
    Vector3 nextPos;
    int nextIndex;

    protected void Start() 
    {
        if (stationary)
        {
            // do nothing?
        }
        else
        {
            if (destinations.Count <= 1)
            {
                Debug.LogError("Minimum 2 destinations needed");
            }
            else
            {
                nextIndex = 0;
                nextPos = destinations[0].transform.position;
            }
        }
    }

    protected override void Act()
    {
        if (!stationary)
        {
            // if arrived at location, move to the next location in the list
            if (Vector3.Distance(transform.position, nextPos) < 0.1f)
            {
                nextIndex = (nextIndex + 1) % destinations.Count;
                nextPos = destinations[nextIndex].transform.position;
            }

            Vector3 direction = (nextPos - transform.position).normalized;
            rd.velocity = direction * speed;
            if (rd.velocity.x > 0)
            {
                facingRight = true;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                facingRight = false;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

        }
    }
}
