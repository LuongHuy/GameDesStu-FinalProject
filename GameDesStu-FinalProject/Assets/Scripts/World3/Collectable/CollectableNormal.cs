using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableNormal : Collectable
{
    protected override void Collected()
    {
        Debug.Log("Collect Normal Coin");
        GameMasterW3.Instance.CollectToken();

        Destroy(gameObject);
    }
}
