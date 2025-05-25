using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class EnemyAIGround : EnemyAI
{
    [SerializeField] ElementStatus bodyStatus;
    [SerializeField] SpriteRenderer sr;

    [Header("Boundary")]
    [SerializeField] List<Transform> destinations = new List<Transform>();

    [Header("Parameter")]
    [SerializeField] float speed = 1f;
    [SerializeField] Rigidbody2D rd;
    [SerializeField] bool stationary = false;

    [Header("Can shoot")]
    [SerializeField] bool canShoot = false;
    [SerializeField] AudioClip shootSound;
    [SerializeField] float attackDelay;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] bool startStaringRight = true;
    // is the element facing right
    bool facingRight = true;
    
    Vector3 nextPos;
    int nextIndex;

    protected void Start()
    {
        facingRight = startStaringRight;

        if (stationary)
        {
            // do nothing?
        }
        else
        {
            if (destinations.Count <= 1)
            {
                Debug.LogError("Minimum 2 destinations needed");
            }
            else
            {
                nextIndex = 0;
                nextPos = destinations[0].transform.position;
            }
        }

        if (canShoot)
        {
            StartCoroutine(Shoot());

        }
    }

    protected override void Act()
    {
        if (!stationary)
        {
            // if arrived at location, move to the next location in the list
            if (Mathf.Abs(nextPos.x - transform.position.x) < 0.1f)
            {
                nextIndex = (nextIndex + 1) % destinations.Count;
                nextPos = destinations[nextIndex].transform.position;
            }

            Vector3 direction = (nextPos - transform.position).normalized;
            rd.velocity = new Vector2( direction.x * speed,rd.velocity.y);
        }
        if (rd.velocity.x > 0)
        {
            facingRight = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rd.velocity.x < 0)
        {
            facingRight = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Color original = sr.color;
            sr.DOColor(Color.blue, 0.2f).OnComplete(() => sr.DOColor(original, 0.2f));
            sr.DOColor(Color.blue, 0.2f).OnComplete(() => sr.DOColor(original, 0.2f));
            yield return new WaitForSeconds(0.1f);
            //bodyStatus.PlayAnimation("Attack");
            //yield return new WaitForSeconds(0.3f);

            Vector3 spawnPos = transform.position + new Vector3(0.25f * (facingRight ? 1 : -1), 0, 0);
            Vector2 destination = spawnPos + new Vector3(0.75f * (facingRight ? 1 : -1), 0);

            ShootOnce(destination, spawnPos, bulletPrefab);
            yield return new WaitForSeconds(attackDelay);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShootOnce(Vector3 destination, Vector3 start, GameObject bulletPrefab)
    {
        if (shootSound != null)
        {
            SoundManager.Instance.playVFX(shootSound, transform);
        }
        GameObject bulletObj = Instantiate(bulletPrefab, start, Quaternion.identity);
        BulletStatus bullet = bulletObj.GetComponent<BulletStatus>();
        bullet.SetDestination(destination);
        bullet.SetBoss(bodyStatus);
        bullet.Activate();
        GameMasterW3.Instance.AddBullet(bullet);
    }

}
