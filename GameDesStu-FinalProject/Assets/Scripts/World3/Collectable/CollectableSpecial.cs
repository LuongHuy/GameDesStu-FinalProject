using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpeical : Collectable
{
    protected override void Collected()
    {
        Debug.Log("Collect Artifact");
        GameMasterW3.Instance.CollectSecondaryObjective();

        Destroy(gameObject);
    }
}
