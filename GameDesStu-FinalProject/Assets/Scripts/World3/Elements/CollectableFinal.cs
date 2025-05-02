using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFinal : Collectable
{
    protected override void Collected(Collider2D collision)
    {
        playSound();
        Invoke("playSound", 0.3f);
        Invoke("playSound", 0.3f);
        GameMasterW3.Instance.CollectMainObjective();
        GameMasterW3.Instance.SpawnPopup("Final artifact collected", collision.transform.position, transform);
    }
    void playSound()
    {
        SoundManager.Instance.playVFX(triggerSound, transform);
    }
}
