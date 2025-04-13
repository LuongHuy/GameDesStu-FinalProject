using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStatus : ElementStatus
{
    [SerializeField] float speed = 2f;
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
    }
    public override void GotAttacked(float damage)
    {
        base.GotAttacked(damage);

        GameMasterW3.Instance.AddUnsaveEnemy(this);

    }
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, 15);
    }

    public override void Die()
    {
        // Don't die
    }
    public void Activate()
    {
        gameObject.SetActive(true);
        Vector3 direction = (destination - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ElementStatus player = collision.gameObject.GetComponent<ElementStatus>();
            if (player != null)
            {
                Attack(player, boss);
            }
            else
            {
                Debug.LogError("Player does not have Element Status class");
            }
        }
    }
}
