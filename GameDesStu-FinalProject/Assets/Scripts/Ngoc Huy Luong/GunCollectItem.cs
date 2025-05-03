using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollectItem : MonoBehaviour
{
    public Gun.shootType shootType;

    public AudioSource collectSound;
    public AudioClip collectSoundClip;

    void Start()
    {
        collectSound.clip = collectSoundClip;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<CharacterControl>();
            if (player != null) 
            {
                player.UpgradeGun(shootType);
                collectSound.Play();
                Destroy(gameObject);
            }
                       
        }
    }
}
