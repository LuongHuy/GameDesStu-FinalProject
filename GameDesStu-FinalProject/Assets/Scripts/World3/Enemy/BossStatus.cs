using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : ElementStatus
{
    [Header("Group")]
    [SerializeField] GameObject mainObject;

    [SerializeField] GameObject finalArtifact;

    bool invunerable;

    protected override void Start()
    {
        base.Start();
    }
    public override void Die()
    {
        base.Die();
        //Instantiate(finalArtifact, transform);
        finalArtifact.SetActive(true);
        Destroy(mainObject.gameObject);
    }

    public override void GotAttacked(float damage)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Color original = sr.color;
        sr.DOColor(Color.gray, 0.2f).OnComplete(() => sr.DOColor(original, 0.2f));

        if (!invunerable)
        {
            base.GotAttacked(damage);
            StartCoroutine(ImmunityOn());
        }

    }

    IEnumerator ImmunityOn()
    {
        invunerable = true;
        yield return new WaitForSeconds(1);
        invunerable = false;
    }

    public bool CheckImmunity()
    {
        return invunerable;
    }
}
