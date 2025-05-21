using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : ElementStatus
{
    [SerializeField] PlayerMovementW3 movementManager;
    [SerializeField] float stepMultiplier = 5;
    [SerializeField] float lifeLimit = 3;
    [SerializeField] TextMeshProUGUI lifeText;

    float currentLife;
    protected override void Start()
    {
        base.Start();
        currentLife = lifeLimit;
        lifeText.SetText(currentLife.ToString());
    }

    protected override void Update()
    {
        base.Update();
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
                    //GameMasterW3.Instance.SpawnPopup("Enemy defeated", transform.position, transform
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

        if (collision.gameObject.CompareTag("Deadzone"))
        {
            PlayAnimation("Die");
            Invoke("Die", 0.2f);
        }
    }

    public override void Die()
    {
        base.Die();
        //if the player still has life

        if (currentLife > 0)
        {
            currentLife = currentLife - 1;
            lifeText.SetText(currentLife.ToString());

            // calling World to update the information
            GameMasterW3.Instance.SpawnPopup("Innitiate Rebirth Sequence", transform.position, transform);
            GameMasterW3.Instance.ResetState();
            Transform respawnLoc = GameMasterW3.Instance.GetCheckpoint();

            // Reset to position
            gameObject.transform.position = respawnLoc.transform.position;

            //Reset Parameter
            currHP = hp;
        }
        else
        {
            Debug.Log("No more life");
            GameMasterW3.Instance.Lose();
        }
    }

    public void LifeUp()
    {
        currentLife += 1;
        lifeText.SetText(currentLife.ToString());
        GameMasterW3.Instance.SpawnPopup("+1 life",transform.position, transform);
    }
}
