using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFinal : Collectable
{
    protected override void Collected(Collider2D collision)
    {
        Debug.Log("Collect Final artifact");
        GameMasterW3.Instance.CollectMainObjective();
    }
}
