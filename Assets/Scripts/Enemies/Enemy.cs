using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy enemyInstance;

    public EnemyStats enemyStats = new EnemyStats();
    public CharSrciptableObject charSO; 

    public Animator enemyAnimator;

    private void Awake()
    {
        enemyInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyStats.health = charSO.EnemiesData.EnemyBaseHealth;
        enemyStats.damage = charSO.EnemiesData.EnemyBaseDamage;

        Debug.Log(enemyStats.health);
    }

    public void EnemyTakeDamage(float damage)
    {
        Debug.Log(damage);
        if (enemyStats.health <= 2)
        {
            enemyAnimator.SetTrigger("defeat");
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            enemyAnimator.SetTrigger("getDamage");
        }
        enemyStats.TakeDamage(damage);
        
    }
}
