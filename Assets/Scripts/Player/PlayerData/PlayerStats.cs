using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log(health);
    }
}
