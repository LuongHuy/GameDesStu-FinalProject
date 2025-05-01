using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float health;
    public CharacterControl characterControl;
    public int respawnTime;
    public float maxHealth;
    private bool isImmortal;

    public void Start()
    {
        health = maxHealth;
    }
    public void ApplyDamage(float damage)
    {
        if (isImmortal == true) 
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            Invoke(nameof(Respawn), respawnTime);
            LifeManager.instance.DecreaseLive();
        }
    }
    public void Respawn()
    {
        Debug.Log("Respawn");
        characterControl.Respawn();
        health = maxHealth;
        isImmortal = true;
        Invoke(nameof(DelayEnableHit), 3);
    }

    public void DelayEnableHit()
    {
        isImmortal = false;
    }

    public void RegenLive(int i)
    {
        LifeManager.instance.LiveRegen(i);
    }

}
