using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    bool _isTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isTriggered)
        {
            GameMasterW3.Instance.ActivateBoss();
            _isTriggered = true;
        }
    }

    public void Reset()
    {
        _isTriggered=false;
    }
}
