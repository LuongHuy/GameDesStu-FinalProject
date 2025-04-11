using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpeical : Collectable
{
    protected override void Collected(Collider2D collision)
    {
        Debug.Log("Collect Artifact");
        GameMasterW3.Instance.CollectSecondaryObjective();

    }
}
