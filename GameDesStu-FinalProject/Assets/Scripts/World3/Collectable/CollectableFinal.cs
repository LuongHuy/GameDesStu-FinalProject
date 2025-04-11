using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFinal : Collectable
{
    protected override void Collected()
    {
        Debug.Log("Collect Final artifact");
        GameMasterW3.Instance.CollectMainObjective();

        Destroy(gameObject);
    }
}
