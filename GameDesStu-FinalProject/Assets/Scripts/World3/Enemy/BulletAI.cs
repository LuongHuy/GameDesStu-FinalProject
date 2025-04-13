using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] Rigidbody2D rb;
    Vector3 destination;

    private void Start()
    {
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }
    public void Activate()
    {
        Vector3 direction = (destination - transform.position).normalized;
        rb.velocity = direction * speed;
    }
}
