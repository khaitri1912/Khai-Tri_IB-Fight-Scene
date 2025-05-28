using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy enemyInstance;

    public EnemyStats enemyStats = new EnemyStats();
    public Slider enemyHealth_bar;

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

        enemyHealth_bar.maxValue = charSO.EnemiesData.EnemyBaseHealth;

        Debug.Log(enemyStats.health);
    }

    private void Update()
    {
        enemyHealth_bar.value = enemyStats.health;
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
