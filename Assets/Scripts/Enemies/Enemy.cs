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

        if (PlayerPrefs.GetInt("Level") != 1)
        {
            Debug.Log("currentlv: "+ PlayerPrefs.GetInt("Level"));
            enemyStats.health += PlayerPrefs.GetInt("Level") * 2;
            Debug.Log("Current health: "+enemyStats.health);
            enemyHealth_bar.maxValue = enemyStats.health;
        }

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
            enemyStats.enemyIsDefeat = true;
            GetComponent<Collider>().enabled = false;
            EnemyDead();
            Destroy(gameObject);
        }
        else
        {
            enemyAnimator.SetTrigger("getDamage");
        }
        enemyStats.TakeDamage(damage);
    }

    public IEnumerator EnemyDead()
    {
        yield return new WaitForSeconds(500);
    }
}
