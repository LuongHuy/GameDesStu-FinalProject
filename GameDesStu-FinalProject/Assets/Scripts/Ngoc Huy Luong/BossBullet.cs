using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;


    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
        CheckColliderWithGround();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        var charHealth = collision.GetComponent<CharacterHealth>();
        if (charHealth != null)
        {
            charHealth.ApplyDamage(1);
            // Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void CheckColliderWithGround()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.3f, 1 << LayerMask.NameToLayer("Ground")))
        {
            Destroy(gameObject);
        }
    }
}
