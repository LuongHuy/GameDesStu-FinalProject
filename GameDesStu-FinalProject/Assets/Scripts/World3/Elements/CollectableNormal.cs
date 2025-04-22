using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableNormal : Collectable
{
    protected override void Collected(Collider2D collision)
    {
        //Debug.Log("Collect Normal Coin");
        GameMasterW3.Instance.CollectToken();
    }
}
