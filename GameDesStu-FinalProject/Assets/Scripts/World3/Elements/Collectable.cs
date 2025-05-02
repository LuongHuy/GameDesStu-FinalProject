using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    Vector2 originalPos;

    [SerializeField] protected bool removable;
    [SerializeField] protected bool resetable;
    [SerializeField] protected AudioClip triggerSound;

    protected abstract void Collected(Collider2D collision);

    public virtual void ResetCollectible()
    {
        transform.position = originalPos;
        gameObject.SetActive(true);

        Instantiate(gameObject, originalPos, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Start()
    {
        originalPos = transform.position;
    }
    protected virtual void Remove()
    {
        if (removable)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + " collide with: " + gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            //SoundManager.Instance.playVFX(triggerSound, transform);
            Collected(collision);
            if (resetable)
            {
                GameMasterW3.Instance.AddUnsaveCollectible(this);
            }
            Remove();
        }
    }
}
