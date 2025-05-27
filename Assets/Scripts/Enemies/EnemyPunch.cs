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

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());

        if (other.tag == "Player")
        {
            Debug.Log("Enemy da va cham voi Player!");
            other.gameObject.GetComponent<Player>().PlayerTakeDamage(Enemy.enemyInstance.enemyStats.damage);
        }
    }
}
