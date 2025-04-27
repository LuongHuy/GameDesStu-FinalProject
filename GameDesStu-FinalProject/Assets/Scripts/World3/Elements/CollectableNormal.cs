using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableNormal : Collectable
{
    protected override void Collected(Collider2D collision)
    {
        //Debug.Log("Collect Normal Coin");
        //GameMasterW3.Instance.CollectToken();
        //GameMasterW3.Instance.SpawnPopup("+1", collision.transform.position, transform);
        GameMasterW3.Instance.PointIncrease(10, collision.transform.position, transform);
    }
}
