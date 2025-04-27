using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 6f;
    public GameObject hitEffect;
    public Vector3 direction;

    private Vector3 initialPosition;

    public float distanctTravel;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
 
        transform.position += moveSpeed * direction * Time.deltaTime;

       
        if (Vector3.Distance(initialPosition, transform.position) >= distanctTravel)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.ApplyDamage(1);
            // Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
