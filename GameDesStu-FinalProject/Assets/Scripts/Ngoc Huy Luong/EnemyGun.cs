using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public NormalEnemyBullet Bullet;
    public Transform findTransform;
    private float tempDelayTime;
    public float delayTime;

   
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

        Instantiate(Bullet, findTransform.position, Quaternion.identity);
       shootSound.Play();
    }

    private void Update()
    {
            Shoot();       
    }
}
