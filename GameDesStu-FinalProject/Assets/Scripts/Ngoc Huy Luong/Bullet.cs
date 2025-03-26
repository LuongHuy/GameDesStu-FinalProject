using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 6f;
    public GameObject hitEffect;
    public int direction;
    void Update()
    {
        
        transform.position += transform.right * moveSpeed * direction * Time.deltaTime;
        if (transform.position.y > 10)
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
