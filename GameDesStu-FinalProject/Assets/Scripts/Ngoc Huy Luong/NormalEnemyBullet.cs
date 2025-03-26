using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyBullet : MonoBehaviour
{
    public float moveSpeed = 6f;
    public GameObject hitEffect;
    void Update()
    {

        transform.position += transform.up * -1 * moveSpeed * Time.deltaTime;
        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyHealth = collision.GetComponent<CharacterHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.ApplyDamage(1);
            // Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
