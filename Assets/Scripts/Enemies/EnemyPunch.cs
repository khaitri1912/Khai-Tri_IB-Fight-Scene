using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{

    public Rigidbody enemyPunchRB;

    // Start is called before the first frame update
    void Start()
    {
        enemyPunchRB.velocity = Vector3.zero;
    }

    /*private void OnBecameVisible()
    {
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
        Debug.Log(other.tag);

        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().PlayerTakeDamage(Enemy.enemyInstance.enemyStats.damage);
        }else if (other.tag == "Ally")
        {
            other.gameObject.GetComponent<Ally>().AllyTakeDamge(Enemy.enemyInstance.enemyStats.damage);
        }
    }
}
