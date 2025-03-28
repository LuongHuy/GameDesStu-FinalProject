using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementStatus : MonoBehaviour 
{
    // normal stats
    [Header("Stats")]
    [SerializeField] float hp=1f;
    [SerializeField] float damage=1f;

    // private variable
    float currHP;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // initiate the current hp to be max hp
        currHP = hp;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // The element die when it is killed
        //if (!IsAlive())
        //{
        //    Die();
        //}
    }

    public virtual void Attack(ElementStatus target, ElementStatus attacker)
    {
        Debug.Log( target.gameObject.name.ToString() + "is attacked by" + attacker.gameObject.name.ToString());
        target.GotAttacked(damage);
    }
    public virtual void GotAttacked(float damage)
    {
        currHP -= damage;
    }

    public virtual bool IsAlive()
    {
        return currHP > 0;
    }
    public virtual void Die()
    {
        Debug.Log("The "+ gameObject.name.ToString() +" die");
    }

}

public class EnemyStatus : ElementStatus
{
    [Header("AI")]
    [SerializeField] EnemyAI AI;

    protected override void Update()
    {
        base.Update();

        // Perform super intelligent move
        //AI.Act();
    }
}
