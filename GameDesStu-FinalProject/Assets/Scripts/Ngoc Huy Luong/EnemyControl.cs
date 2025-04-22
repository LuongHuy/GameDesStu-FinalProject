using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var charHealth = collision.collider.GetComponent<CharacterHealth>();
        if (charHealth != null)
        {
            charHealth.ApplyDamage(1);
        }
    }
}
