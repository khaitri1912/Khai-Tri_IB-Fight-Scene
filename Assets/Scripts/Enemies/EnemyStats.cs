using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log(health);
    }

    public float IncreaseHealth(float currentHealth , float health)
    {
        currentHealth += health;
        return currentHealth;
    }
}
