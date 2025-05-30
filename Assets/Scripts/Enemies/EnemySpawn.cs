using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject prefabEnemy;
    public List<GameObject> enemyList;
    public List<Transform> enemyPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnPos = GameObject.FindGameObjectWithTag("EnemyPoints");

        foreach (Transform t in spawnPos.transform)
        {
            enemyPoints.Add(t);
        }

        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        int amountOfEnemies = PlayerPrefs.GetInt("AmountOfEnemies");
        int randomPlace = Random.Range(0, enemyPoints.Count);

        for (int i = 0; i < amountOfEnemies; i++)
        {
            Debug.Log("SpawnEnemies: "+i);
            Transform randomPoint = enemyPoints[randomPlace];
            GameObject enemy = Instantiate(prefabEnemy, randomPoint.position, Quaternion.identity);
            enemyList.Add(enemy);
        }
    }
}
