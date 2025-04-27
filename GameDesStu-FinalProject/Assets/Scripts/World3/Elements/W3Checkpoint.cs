using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3Checkpoint : Collectable
{
    protected override void Remove()
    {
        // Do not remove;
    }
    protected override void Collected(Collider2D collision)
    {
        GameMasterW3.Instance.SaveStage(transform);
        GameMasterW3.Instance.SpawnPopup("Reach Checkpoint", collision.transform.position, transform);
    }
}
