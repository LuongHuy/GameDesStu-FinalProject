using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet Bullet;
    public Transform findTransform;
    private float tempDelayTime;
    public float delayTime;
    public enum shootType {
    singleShoot,
    tripleShoot
    }
    public shootType ShootType;

    public AudioSource shootSound;
    public AudioClip shootSoundClip;

    void Start()
    {
        shootSound.clip = shootSoundClip;
    }

    private void Shoot()
    {
        if (tempDelayTime > Time.time)
        {
            return;
        }
        tempDelayTime = Time.time + delayTime;

      switch (ShootType)
        {
            case shootType.singleShoot:
                SingleShoot(); break;
            case shootType.tripleShoot: 
                TripleShoot(); break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        { 
            Shoot();
            
        }
    }

    public void UpdateShootType(shootType type)
    {
        ShootType = type;
    }
    private void SingleShoot()
    {
        var bullet = Instantiate(Bullet, findTransform.position, Quaternion.identity);
        bullet.direction = transform.right;
        shootSound.Play();

    }
    
    private void TripleShoot()
    {
            
            var firstDirection = (findTransform.position - transform.position).normalized;
            var secondDirection = ((findTransform.position + transform.up * 0.1f) - transform.position).normalized;
            var thirdDirection = ((findTransform.position - transform.up * 0.1f) - transform.position).normalized;


            var tempbullet1 = Instantiate(Bullet, transform.position, Quaternion.identity);
            tempbullet1.direction = firstDirection;

            var tempbullet2 = Instantiate(Bullet, transform.position, Quaternion.identity);
            tempbullet2.direction = secondDirection;

            var tempbullet3 = Instantiate(Bullet, transform.position, Quaternion.identity);
            tempbullet3.direction = thirdDirection;
        shootSound.Play();

    }
}
