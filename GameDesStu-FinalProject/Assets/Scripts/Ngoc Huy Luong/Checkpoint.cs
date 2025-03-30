using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<CharacterControl>();
        if (player != null)
        {
            gameObject.SetActive(false);
            player.checkpoint = transform.position;
        }
    }
}
