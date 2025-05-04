using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gun;

public class HealthCollect : MonoBehaviour
{
    public int healthAmount = 1;
    public ScoreTextInit text;

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
            var playerHealth = collision.gameObject.GetComponent<CharacterHealth>();
            if (playerHealth != null)
            {
                playerHealth.RegenLive(healthAmount);
                text.ShowScoreText("Life Increased", 1.5f);
                collectSound.Play();
                Destroy(gameObject);
            }
        }
    }
}
