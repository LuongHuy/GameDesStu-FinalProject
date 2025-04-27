using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpeical : Collectable
{
    protected override void Collected(Collider2D collision)
    {
        //Debug.Log("Collect Artifact");
        //GameMasterW3.Instance.CollectSecondaryObjective();
        GameMasterW3.Instance.PointIncrease(50, collision.transform.position, transform);
        //GameMasterW3.Instance.SpawnPopup("+50", collision.transform.position, transform);
    }
}
