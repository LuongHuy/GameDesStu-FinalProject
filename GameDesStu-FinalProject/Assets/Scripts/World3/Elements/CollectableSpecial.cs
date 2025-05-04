using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpeical : Collectable
{
    [SerializeField] int earnPoint=25;
    protected override void Collected(Collider2D collision)
    {
        //GameMasterW3.Instance.CollectSecondaryObjective();
        GameMasterW3.Instance.PointIncrease(earnPoint, collision.transform.position, transform);
        SoundManager.Instance.playVFX(triggerSound, transform);
    }
}
