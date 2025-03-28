using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public int scoreAdd;

    public void Awake()
    {
        health = maxHealth;
    }

    public float GetEnemyHealthPercent()
    {
        return health/maxHealth;
    }
    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
