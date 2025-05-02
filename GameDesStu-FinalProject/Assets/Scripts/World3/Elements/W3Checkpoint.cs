using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class W3Checkpoint : Collectable
{
    public bool activated;
    [SerializeField] SpriteRenderer sr;
    protected override void Remove()
    {
        // Do not remove;
    }
    protected override void Collected(Collider2D collision)
    {
        if (!activated)
        {
            SoundManager.Instance.playVFX(triggerSound, transform);
            GameMasterW3.Instance.SaveStage(transform);
            activated = true;
            Color original = sr.color;
            original.a = 0.3f;
            sr.DOColor(Color.green, 0.2f);
            //sr.color = original;
        }
        //GameMasterW3.Instance.SpawnPopup("Reach Checkpoint", collision.transform.position, transform);
    }
}
