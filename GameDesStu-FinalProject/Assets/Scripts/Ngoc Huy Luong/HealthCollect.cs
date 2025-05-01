using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gun;

public class HealthCollect : MonoBehaviour
{
    public int healthAmount = 1;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerHealth = collision.gameObject.GetComponent<CharacterHealth>();
            if (playerHealth != null)
            {
                playerHealth.RegenLive(healthAmount);               
                Destroy(gameObject);
            }
        }
    }
}
