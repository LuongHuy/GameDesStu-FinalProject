using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider;
    public ScoreTextInit text;

    public AudioSource collectSound;
    public AudioClip collectSoundClip;

    void Start()
    {
        collectSound.clip = collectSoundClip;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<CharacterControl>();
        if (player != null)
        {
            boxCollider.enabled = false;
            player.checkpoint = transform.position;
            text.ShowScoreText("Checkpoint Reached", 1.5f);
            collectSound.Play();
            sprite.DOColor(Color.green, 0.5f);
        }
    }
}
