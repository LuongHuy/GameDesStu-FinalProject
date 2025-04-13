using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public struct BossParameter
{
    public List<Transform> destinations;
    public float speed;
    public float moveDelay;
    public float attackDelay;
    public float attackDelayInBetween;
    public float attackAmount;
    public GameObject bulletPrefab;
}

public abstract class BossState
{
    protected BossAI boss;
    BossParameter parameter;
    protected float _stayTimer;
    protected float _shootTimer;
    protected float _shootTimerInbetween;

    public void SetBoss(BossAI boss)
    {
        this.boss = boss;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void Act()
    {
        // For moving

        // if not moving
        if (!boss.CheckArrival())
        {
            // wait for second
            _stayTimer += Time.deltaTime;
            if (_stayTimer > boss.GetParameter().moveDelay)
            {
                // then move
                boss.Move();
            }
        }
        else
        {
            _stayTimer = 0;
        }

        // shooting back may change in stage
    }
    public virtual void StateChange()
    {
    }
}

public class Normal: BossState
{
    public override void OnEnter()
    {
        boss.ChangeParameter(0);
    }

    public override void OnExit()
    {
    }
    public override void Act()
    {
        base.Act();
        // For combat

        // After delay, shoot
        _shootTimer += Time.deltaTime;
        if ( _shootTimer > boss.GetParameter().attackDelay)
        {
            // Shoot x bullet
            _shootTimerInbetween += Time.deltaTime;
            for (int i = 0; i < boss.GetParameter().attackAmount; i++)
            {

            }
            boss.Shoot(Vector2.zero);
            _shootTimer = 0;
        }
    }
    public override void StateChange()
    {
        base.StateChange();
        if (boss.GetHP() >= Mathf.RoundToInt(boss.GetHP() / 2)) 
        {
            boss.TransitTo(new Bloody());
        }
    }
}


public class Bloody : BossState
{
    public override void OnEnter()
    {
        boss.ChangeParameter(1);
    }

    public override void OnExit()
    {
    }
    public override void Act()
    {
        base.Act();
        // For combat
    }
    public override void StateChange()
    {
        base.StateChange();
    }
}

public class BossAI : EnemyAI
{
    [Header("Normal State")]
    [SerializeField] BossParameter parameters;

    [Header("Bloody State")]
    [SerializeField] BossParameter parametersBloody;

    [Header("Import script")]
    [SerializeField] Rigidbody2D rd;
    [SerializeField] ElementStatus bossStatus;
    [SerializeField] GameObject target;

    // Setup State machine
    BossState currState;

    // Current parameter;
    BossParameter currParameter;

    public void TransitTo(BossState state)
    {
        // if the new state does not exist
        if (state == null)
        {
            return;
        }
        // Execute on exit
        if (currState != null)
        {
            currState.OnExit();
        }
        // Switch state
        state.SetBoss(this);
        currState = state;
        // execute on enter
        currState.OnEnter();
    }

    // is the element facing right
    bool facingRight = true;
    
    Vector3 nextPos;
    int nextIndex;

    protected void Start() 
    {
        TransitTo(new Normal());

        if (parameters.destinations.Count <= 1 || parametersBloody.destinations.Count <=1)
        {
            Debug.LogError("Minimum 2 destinations needed for each state");
        }
        else
        {
            nextIndex = 0;
            nextPos = parameters.destinations[0].transform.position;
        }
    }

    protected override void Act()
    {
        currState.Act();
    }

    public void Move()
    {
        Vector3 direction = (nextPos - transform.position).normalized;
        rd.velocity = direction * currParameter.speed;

    }

    public void Shoot(Vector3 offset)
    {
        GameObject bulletObj = Instantiate(currParameter.bulletPrefab, transform.position, Quaternion.identity);
        BulletAI bullet = bulletObj.GetComponent<BulletAI>();
        bullet.SetDestination(target.transform.position + offset);
        bullet.Activate();
    }

    IEnumerable ShootPattern1()
    {
        for (int i = 0; i < currParameter.attackAmount; i++)
        { 
            yield return new WaitForSeconds(currParameter.attackDelayInBetween);
            Shoot(Vector3.zero);
        }
    }

    public bool CheckArrival()
    {
        bool arrived = Vector3.Distance(transform.position, nextPos) < 0.1f;
        // if arrived at location, move to the next location in the list
        if (arrived)
        {
            nextIndex = (nextIndex + 1) % currParameter.destinations.Count;
            nextPos = currParameter.destinations[nextIndex].transform.position;
            // Stop movement
            rd.velocity = Vector2.zero;
        }
        return arrived;
    }

    public void ChangeParameter(float state)
    {
        switch (state)
        {
            case 0:
                currParameter = parameters;
                break;
            case 1:
                currParameter = parametersBloody;
                break;
        }
    }
    public BossParameter GetParameter()
    {
        return currParameter; 
    }

    public float GetHP()
    {
        return bossStatus.hp;
    }
}
