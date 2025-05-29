using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPunch : MonoBehaviour
{
    public GameObject enemy;
    public Transform allyRightPunch;

    public Rigidbody allyPunchRB;

    // Start is called before the first frame update
    void Start()
    {
        allyPunchRB.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(transform.GetComponent<Rigidbody>());

        if (other.tag == "Enemy")
        {
            Debug.Log("Ally da va cham voi Enemy");
            other.gameObject.GetComponent<Enemy>().EnemyTakeDamage(Ally.allyInstance.allyStats.damage);
        }
    }
}
