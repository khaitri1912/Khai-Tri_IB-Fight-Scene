using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawn : MonoBehaviour
{
    public GameObject prefabally;
    public List<GameObject> allies;
    public List<Transform> allyPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject allySpawnPos = GameObject.FindGameObjectWithTag("AllyPoints");

        foreach(Transform t in allySpawnPos.transform)
        {
            allyPoints.Add(t);
        }

        SpawnAllies();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAllyAlive();
    }

    public void SpawnAllies()
    {
        int amountOfAllies = PlayerPrefs.GetInt("AmountOfAllies");
        int randomAllyPlace = Random.Range(0, allyPoints.Count);

        for (int i = 0; i < amountOfAllies; i++)
        {
            Transform randomPoint = allyPoints[randomAllyPlace];
            GameObject ally = Instantiate(prefabally, randomPoint.position, Quaternion.identity);
            allies.Add(ally);
        }
    }

    public bool CheckAllyAlive()
    {
        bool allyAlive = true;
        foreach(GameObject ally in allies)
        {
            if (ally.GetComponent<Ally>().allyStats.health <= 0)
            {
                allyAlive = false;
            }
            else
            {
                allyAlive = true;
            }
        }
        return allyAlive;
    }
}
