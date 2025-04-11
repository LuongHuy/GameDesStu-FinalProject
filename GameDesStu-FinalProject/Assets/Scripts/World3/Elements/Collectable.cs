using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    Vector2 originalPos;

    [SerializeField] bool removable;
    [SerializeField] bool resetable;

    protected abstract void Collected(Collider2D collision);

    public virtual void ResetCollectible()
    {
        Debug.Log(gameObject.name + " reset.");
        transform.position = originalPos;
        gameObject.SetActive(true);
    }

    private void Start()
    {
        originalPos = transform.position;
    }
    protected virtual void Remove()
    {
        //Destroy(gameObject);
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
            Collected(collision);
            if (resetable)
            {
                GameMasterW3.Instance.AddUnsaveCollectible(this);
            }
            Remove();
        }
    }
}
