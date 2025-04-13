using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject gate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
            gate.SetActive(true);
        }
    }
}
