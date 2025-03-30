using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public NormalEnemyBullet Bullet;
    public Transform findTransform;
    private float tempDelayTime;
    public float delayTime; 
    private void Shoot()
    {
        if (tempDelayTime > Time.time)
        {
            return;
        }
        tempDelayTime = Time.time + delayTime;

        Instantiate(Bullet, findTransform.position, Quaternion.identity);
    }

    private void Update()
    {
            Shoot();       
    }
}
