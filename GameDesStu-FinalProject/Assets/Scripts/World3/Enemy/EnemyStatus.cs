using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementStatus : MonoBehaviour 
{
    // normal stats
    [Header("Stats")]
    [SerializeField] protected float hp=1f;
    [SerializeField] protected float damage =1f;

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
        ////The element die when it is killed
    }

    public virtual void Attack(ElementStatus target, ElementStatus attacker)
    {
        //Debug.Log(target.gameObject.name.ToString() + " is attacked by " + attacker.gameObject.name.ToString());
        target.GotAttacked(damage);
    }
    public virtual void GotAttacked(float damage)
    {
        currHP -= damage;
        //Debug.Log(gameObject.name.ToString() + "'s current HP: " + currHP.ToString());
        if (!IsAlive())
        {
            Invoke("Die", 0.2f);
        }
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
    public float GetHP()
    {
        return currHP;
    }
    public float GetDamage() { 
        return damage; 
    }
    public float GetPercentageHP(float percentage) { 
        return Mathf.RoundToInt(hp * percentage);
    }
}

public class EnemyStatus : ElementStatus
{
    [Header("Group")]
    [SerializeField] GameObject mainObject;

    Vector2 originalPos;

    protected override void Start()
    {
        base.Start();
        originalPos = mainObject.transform.position;
    }
    public override void GotAttacked(float damage)
    {
        base.GotAttacked(damage);
        GameMasterW3.Instance.AddUnsaveEnemy(this);
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Color original = sr.color;
        sr.DOColor(Color.gray, 0.2f).OnComplete(() => sr.DOColor(original, 0.2f));
    }

    public override void Die()
    {
        base.Die();
        //Destroy(mainObject.gameObject);
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
