using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public float health;
    public float damage;
    /*public float attackSpeed;*/

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }
}
