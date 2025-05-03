using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public int scoreAdd;
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
            collectSound.Play();
            Destroy(gameObject);
            ScoreManager.instance.UpdateScore(scoreAdd);           
        }
    }
}
