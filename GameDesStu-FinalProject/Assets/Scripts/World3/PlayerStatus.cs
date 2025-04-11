using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : ElementStatus
{
    [SerializeField] PlayerMovementW3 movementManager;
    [SerializeField] float lifeLimit;
    [SerializeField] TextMeshProUGUI lifeText;

    float currentLife;
    protected override void Start()
    {
        base.Start();
        currentLife = lifeLimit;
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

        //if the player still has life
        if (currentLife > 0)
        {
            // calling World to update the information
            GameMasterW3.Instance.ResetState();
            Transform respawnLoc = GameMasterW3.Instance.GetCheckpoint();

            // Reset to position
            gameObject.transform.position = respawnLoc.transform.position;

            //Reset Parameter
            currHP = hp;

            currentLife--;
            lifeText.SetText(currentLife.ToString());
        }
        else
        {
            GameMasterW3.Instance.Lose();
        }
    }
}
