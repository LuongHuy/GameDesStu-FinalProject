using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : ElementStatus
{
    [Header("Import player movement manager")]
    [SerializeField] PlayerMovementW3 movementManager;

    [Header("Respawn location")]
    [SerializeField] GameObject respawnLoc;

    protected override void Start()
    {
        base.Start();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ElementStatus enemy = collision.gameObject.GetComponent<ElementStatus>();
            if (enemy != null)
            {
                if (movementManager.CheckStepOnEnemy())
                {
                    Attack(enemy, this);
                }
                else
                {
                    enemy.Attack(this, enemy);
                }
            }
            else
            {
                Debug.LogError("Collide with: " + collision.gameObject.name.ToString() + " without a elementStatus");

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Deadzone"))
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        // Reset to position
        this.gameObject.transform.position = respawnLoc.transform.position;

        //Reset Parameter
        currHP = hp;
    }
}
