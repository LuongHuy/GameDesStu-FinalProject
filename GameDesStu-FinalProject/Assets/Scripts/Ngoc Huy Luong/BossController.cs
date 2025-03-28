using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int bossPhase;
    public Transform[] patrolPoint;
    private int curPatrolIndex;
    private bool isMoving;
    private Transform nextPatrol;
    public float speed;

    public float timeMoveGapMin;
    public float timeMoveGapMax;
    private float timeMoveGapDelay;

    public BossBullet bullet;
    private float delayShootTime;
    public float timeShootGapMin;
    public float timeShootGapMax;
    public Transform characterTarget;

    public EnemyHealth curHealth;
    public float[] phaseHealth;


    public void BossMove()
    {
        if (!isMoving) 
        {   
            if (timeMoveGapDelay > Time.time)
            {
                return;
            }
            timeMoveGapDelay = Random.Range(timeMoveGapMin, timeMoveGapMax) + Time.time;
            nextPatrol = patrolPoint[curPatrolIndex];
            isMoving = true;

        }
        transform.position = Vector3.Lerp(transform.position, nextPatrol.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position,nextPatrol.position) < 0.5f)
        {
            isMoving = false;
            curPatrolIndex++;
            if (curPatrolIndex > patrolPoint.Length - 1)
            {
                curPatrolIndex = 0;
            }
        }
        
    }

    public void BossAttackPhase1()
    {
        if (delayShootTime > Time.time)
        {
            return;
        }
        delayShootTime = Random.Range(timeShootGapMin, timeShootGapMax) + Time.time;
        StartCoroutine(TripleShot());
    }

    IEnumerator TripleShot()
    {
        for (int i = 0; i < 3; i++)
        {
            var tempbullet = Instantiate(bullet, transform.position, Quaternion.identity);
            tempbullet.direction = (characterTarget.position - transform.position).normalized;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void BossAttackPhase2()
    {
        if (delayShootTime > Time.time)
        {
            return;
        }
        delayShootTime = Random.Range(timeShootGapMin, timeShootGapMax) + Time.time;
      StartCoroutine (TripleShot2());
    }

    IEnumerator TripleShot2()
    {
        for (int i = 0; i < 3; i++)
        {
            var firstDirection = (characterTarget.position - transform.position).normalized;
            var secondDirection = ((characterTarget.position + transform.up * 1.5f) - transform.position).normalized;
            var thirdDirection = ((characterTarget.position - transform.up * 1.5f) - transform.position).normalized;


            var tempbullet1 = Instantiate(bullet, transform.position, Quaternion.identity);
            tempbullet1.direction = firstDirection;

            var tempbullet2 = Instantiate(bullet, transform.position, Quaternion.identity);
            tempbullet2.direction = secondDirection;

            var tempbullet3 = Instantiate(bullet, transform.position, Quaternion.identity);
            tempbullet3.direction = thirdDirection;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void CheckPhase()
    {
        var tempHealth = curHealth.GetEnemyHealthPercent();
        for (int i = 0; i < phaseHealth.Length; i++)
        {
            if (tempHealth < phaseHealth[i] && bossPhase < i)
            {
                bossPhase = i;               
            }
        }
    }

    public void SummonCreep()
    {

    }

    public void Update()
    {
        CheckPhase();
        BossMove();
        if (bossPhase == 0)
        {
            BossAttackPhase1();
        }
        else if (bossPhase == 1)
        {
            BossAttackPhase2();
        }
        if (bossPhase == 2)
        {
            SummonCreep();
        }
    }
}
