using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public int scoreAdd;
    public Action onDied;
    public Image healthBar;
    public GameObject healthBarCover;

    public SpriteRenderer enemyRenderer;

    public void Awake()
    {
        health = maxHealth;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthBar == null)
        {
            return;
        }
        healthBar.fillAmount = health / maxHealth;
    }

    private void OnEnable()
    {
        if (healthBarCover != null)
        {
            healthBarCover.gameObject.SetActive(true);
        }      
    }

    private void OnDisable()
    {
        if (healthBarCover != null)
        {
            healthBarCover.gameObject.SetActive(false);
        }     
    }

    public float GetEnemyHealthPercent()
    {
        return health/maxHealth;
    }
    public void ApplyDamage(float damage)
    {
        health -= damage;
        UpdateHealthUI();
        if (health <= 0)
        {
            onDied?.Invoke();
            Destroy(gameObject);
            ScoreManager.instance.UpdateScore(scoreAdd);
            return;
        }
        DamagedEffect();
    }
    public void DamagedEffect()
    {
        enemyRenderer.DOColor(Color.red, 0.2f).OnComplete(() => enemyRenderer.DOColor(Color.white,0.2f));
    }
}
