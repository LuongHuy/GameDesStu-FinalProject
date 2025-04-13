using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementStatus : MonoBehaviour 
{
    // normal stats
    [Header("Stats")]
    [SerializeField] public float hp=1f;
    [SerializeField] public float damage =1f;

    // private variable
    protected float currHP;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // initiate the current hp to be max hp
        currHP = hp;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        //The element die when it is killed
        if (!IsAlive())
        {
            Die();
        }
    }
    protected virtual void FixedUpdate()
    {
        // update location
    }

    public virtual void Attack(ElementStatus target, ElementStatus attacker)
    {
        //Debug.Log( target.gameObject.name.ToString() + " is attacked by " + attacker.gameObject.name.ToString());
        target.GotAttacked(damage);
    }
    public virtual void GotAttacked(float damage)
    {
        currHP -= damage;
        //Debug.Log(gameObject.name.ToString()+ "'s current HP: "+ currHP.ToString());
    }

    public virtual bool IsAlive()
    {
        return currHP > 0;
    }
    public virtual void Die()
    {
        //Debug.Log("The "+ gameObject.name.ToString() +" die");
    }
    public virtual void ResetElement()
    {
        currHP = hp;
    }
}

public class EnemyStatus : ElementStatus
{
    [Header("Group")]
    [SerializeField] GameObject mainObject;

    Vector2 originalPos;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Start()
    {
        base.Start();
        originalPos = mainObject.transform.position;
    }
    public override void Die()
    {
        base.Die();
        //Destroy(mainObject.gameObject);
        GameMasterW3.Instance.AddUnsaveEnemy(this);
        mainObject.gameObject.SetActive(false);
    }
    public override void ResetElement()
    {
        base.ResetElement();

        mainObject.gameObject.SetActive(true);
        mainObject.transform.position = originalPos;
        Instantiate(mainObject, originalPos, Quaternion.identity);
        Destroy(mainObject);
    }
}
