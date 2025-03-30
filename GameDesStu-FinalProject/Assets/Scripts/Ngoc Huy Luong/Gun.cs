using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet Bullet;
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

        var bullet = Instantiate(Bullet,findTransform.position,Quaternion.identity);
        bullet.direction = transform.eulerAngles.y == 0? 1: -1;
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        { 
        Shoot();
        }
    }
}
