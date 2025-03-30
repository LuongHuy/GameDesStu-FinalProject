using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyHealth = collision.GetComponent<CharacterHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.ApplyDamage(1);
           
        }
    }
}
