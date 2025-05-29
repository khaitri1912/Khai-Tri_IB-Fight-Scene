using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ally : MonoBehaviour
{
    public static Ally allyInstance;
    public Animator allyAnimator;

    public CharSrciptableObject charSO;

    [Header("Ally Stats")]
    public AllyStats allyStats = new AllyStats();
    public Slider allyHealth_Bar;

    private void Awake()
    {
        if (allyInstance == null)
        {
            allyInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        allyStats.health = charSO.AllyData.AllyBaseHealth;
        allyStats.damage = charSO.AllyData.AllyBaseDamage;

        allyHealth_Bar.maxValue = charSO.AllyData.AllyBaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        allyHealth_Bar.value = allyStats.health;
    }

    public void AllyTakeDamge(float damage)
    {
        if (allyStats.health <= 2)
        {
            allyAnimator.SetTrigger("defeat");
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
        }
        else
        {
            allyAnimator.SetTrigger("getDamage");
        }
        allyStats.TakeDamage(damage);
    }
}
