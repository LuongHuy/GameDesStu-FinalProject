using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableNormal : Collectable
{
    [SerializeField] int earnPoint = 10;
    protected override void Collected(Collider2D collision)
    {
        SoundManager.Instance.playVFX(triggerSound, transform);
        GameMasterW3.Instance.PointIncrease(earnPoint, collision.transform.position, transform);
    }
}
