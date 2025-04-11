using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3Checkpoint : Collectable
{

    protected override void Collected()
    {
        GameMasterW3.Instance.SaveStage(transform);
    }
}
