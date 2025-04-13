using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : ElementStatus
{
    [Header("Group")]
    [SerializeField] GameObject mainObject;

    protected override void Start()
    {
        base.Start();
    }
    public override void Die()
    {
        base.Die();
        Destroy(mainObject.gameObject);
    }
}
