using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStatus : ElementStatus
{
    [SerializeField] float speed = 15f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ElementStatus boss;
    Vector3 destination;

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }
    public void SetBoss(ElementStatus boss)
    {
        this.boss = boss;
        damage = boss.GetDamage();
    }
    public override void GotAttacked(float damage)
    {
        base.GotAttacked(damage);

        GameMasterW3.Instance.AddUnsaveEnemy(this);

    }
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, 3.5f);
    }

    public override void Die()
    {
        //Debug.Log("Bullet got destroyed");
        rb.velocity = Vector2.zero;
        Destroy(gameObject,0.2f);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        Vector3 direction = (destination - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ElementStatus enemyStatus = collision.rigidbody.gameObject.GetComponent<ElementStatus>();
            if (enemyStatus != null )
            {
                if (enemyStatus != boss)
                {
                    Attack(enemyStatus, boss);
                    EnterDieState();
                }
                else
                {
                    //Debug.LogError("Do not allow self attack");
                }
            }
            else
            {
                Debug.LogError("Player does not have Element Status class");
            }
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
