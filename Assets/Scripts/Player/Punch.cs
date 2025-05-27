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

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
/*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player da va cham voi Enemy!");
            collision.gameObject.GetComponent<Enemy>().EnemyTakeDamage(2);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
        /*if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player da va cham voi Enemy!");
            transform.parent = other.transform;
            other.gameObject.GetComponent<Enemy>().EnemyTakeDamage(2);
        }*/
        if (other.tag == "Enemy")
        {
            Debug.Log("Player da va cham voi Enemy!");
            //transform.parent = other.transform;
            other.gameObject.GetComponent<Enemy>().EnemyTakeDamage(2);
        }
    }
}
