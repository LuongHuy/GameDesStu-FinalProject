using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct BossParameter
{
    public List<Transform> destinations;
    public float speed;
    public float moveDelay;
    public float attackDelay;
    public float attackDelayInBetween;
    public float attackAmount;
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
        if (boss.CheckImmunity())
        {
            _stayTimer = boss.GetParameter().moveDelay;
        }

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
            boss.ShootPatternSimple();
            _shootTimer = 0;
        }
    }
    public override void StateChange()
    {
        base.StateChange();
        if (boss.GetHP() <= boss.GetPercentageHP(0.5f)) 
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

        // After delay, shoot
        _shootTimer += Time.deltaTime;
        if (_shootTimer > boss.GetParameter().attackDelay)
        {
            // Shoot x bullet
            _shootTimerInbetween += Time.deltaTime;
            // attack 2 has 35% chance of happen
            float token = Random.Range(0, 1f);
            if (token > 0.35f)
            {
                boss.ShootPatternSimple();
            }
            else
            {
                boss.ShootPattern2();
            }
            _shootTimer = 0;
        }
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
    [SerializeField] SpriteRenderer sr;
    [SerializeField] AudioClip shootSound;

    [SerializeField] GameObject bulletPrefab1;
    [SerializeField] GameObject bulletPrefab2;


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
        if (bossStatus.IsAlive()) {
            currState.Act();
            currState.StateChange();
        }
        // facing direction
        Vector3 lookingDirection = target.transform.position - transform.position;

        if (lookingDirection.normalized.x>0)
        {
            facingRight = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            facingRight = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void Move()
    {
        Vector3 direction = (nextPos - transform.position).normalized;
        rd.velocity = direction * currParameter.speed * (CheckImmunity()?2:1);

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

    public void ShootOnce(Vector3 destination, Vector3 start, GameObject bulletPrefab)
    {
        SoundManager.Instance.playVFX(shootSound, transform);
        GameObject bulletObj = Instantiate(bulletPrefab, start, Quaternion.identity);
        BulletStatus bullet = bulletObj.GetComponent<BulletStatus>();
        bullet.SetDestination(destination);
        bullet.SetBoss(bossStatus);
        bullet.Activate();
        GameMasterW3.Instance.AddBullet(bullet);
    }

    IEnumerator ShootMultipleSimple(Vector3 offset, GameObject bulletPrefab)
    {
        Color original = sr.color;
        sr.DOColor(Color.blue, 0.2f).OnComplete(() => sr.DOColor(original, 0.2f));
        sr.DOColor(Color.blue, 0.2f).OnComplete(() => sr.DOColor(original, 0.2f));
        yield return new WaitForSeconds(0.1f);
        bossStatus.PlayAnimation("Attack");
        yield return new WaitForSeconds(0.3f);

        Vector3 spawnPos = transform.position + new Vector3(0.25f * (facingRight ? 1 : -1), 0, 0);
        for (int i = 0; i < currParameter.attackAmount; i++)
        { 
            ShootOnce(target.transform.position + offset, spawnPos, bulletPrefab);
            yield return new WaitForSeconds(currParameter.attackDelayInBetween);
        }
        yield return new WaitForSeconds(0.1f);
        bossStatus.PlayAnimation("Idle");
    }
    
    IEnumerator ShootMultiple2(Vector3 offset, GameObject bulletPrefab)
    {
        Color original = sr.color;
        sr.DOColor(Color.blue, 0.2f).OnComplete(() => sr.DOColor(original, 0.2f));
        sr.DOColor(Color.blue, 0.2f).OnComplete(() => sr.DOColor(original, 0.2f));
        yield return new WaitForSeconds(0.1f);
        bossStatus.PlayAnimation("Attack");
        yield return new WaitForSeconds(0.3f);

        Vector3 spawnPos = transform.position + new Vector3(0.25f * (facingRight ? 1 : -1), 0, 0);
        for (int i = 0; i < 3; i++)
        { 
            Vector3 direction = spawnPos + offset + new Vector3(0.25f*(i+1)* (facingRight ? 1 : -1), 1,0);
            Debug.Log(direction);
            ShootOnce(direction, spawnPos, bulletPrefab);
            yield return new WaitForSeconds(currParameter.attackDelayInBetween);
        }
        yield return new WaitForSeconds(0.1f);
        bossStatus.PlayAnimation("Idle");
    }

    public void ShootPatternSimple()
    {
        StartCoroutine(ShootMultipleSimple(Vector3.zero, bulletPrefab1));
    }

    public void ShootPattern2()
    {
        StartCoroutine(ShootMultiple2(Vector3.zero, bulletPrefab2));
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
        return bossStatus.GetHP();
    }
    public float GetPercentageHP(float percentage)
    {
        return bossStatus.GetPercentageHP(percentage);
    }
    public bool CheckImmunity()
    {
        return ((BossStatus) bossStatus).CheckImmunity();
    }
}
