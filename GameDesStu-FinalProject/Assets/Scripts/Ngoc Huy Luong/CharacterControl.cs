using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public Vector3 checkpoint;
    public Gun characterGun;
    public Color normalColor;
    public Color tripleShootColor;
    public SpriteRenderer characterRender;
    public ScoreTextInit text;
    public void Awake()
    {
        checkpoint = transform.position;
    }
    public void Respawn()
    {
        transform.position = checkpoint;
        gameObject.SetActive(true);
        characterGun.UpdateShootType(Gun.shootType.singleShoot);
        UpdateCharacterColor(Gun.shootType.singleShoot);
    }
    public void UpgradeGun(Gun.shootType shootType)
    {
        characterGun.UpdateShootType(shootType);
        UpdateCharacterColor(shootType);
        if (shootType != Gun.shootType.singleShoot)
        {
            text.ShowScoreText("Gun Upgraded", 1f);
        }
    }
    private void UpdateCharacterColor(Gun.shootType shootType)
    {
        switch (shootType) 
        { 
            case Gun.shootType.singleShoot:
                characterRender.DOColor(normalColor,0.5f);
                break;
                case Gun.shootType.tripleShoot:
                characterRender.DOColor(tripleShootColor, 0.5f);
                break;
                default: characterRender.DOColor(normalColor,0.5f); break;

        }
    }
}
