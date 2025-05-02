using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableNormal : Collectable
{
    protected override void Collected(Collider2D collision)
    {
        SoundManager.Instance.playVFX(triggerSound, transform);
        GameMasterW3.Instance.PointIncrease(10, collision.transform.position, transform);
    }
}
