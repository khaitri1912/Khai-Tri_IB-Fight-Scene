using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public GameObject enemy;
    public Transform rightPunch;

    public Rigidbody punchRB;

    // Start is called before the first frame update
    void Start()
    {
        punchRB.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (Player.PlayerInstance.playerStats.health <= 0)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
        
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().EnemyTakeDamage(Player.PlayerInstance.playerStats.damage);
        }
    }
}
