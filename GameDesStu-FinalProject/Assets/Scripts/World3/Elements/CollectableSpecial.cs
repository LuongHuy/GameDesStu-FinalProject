using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpeical : Collectable
{
    protected override void Collected(Collider2D collision)
    {
        //GameMasterW3.Instance.CollectSecondaryObjective();
        GameMasterW3.Instance.PointIncrease(50, collision.transform.position, transform);
        SoundManager.Instance.playVFX(triggerSound, transform);
    }
}
