using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateEnemies : MonoBehaviour
{
    public EnemiesList enemiesTypes;

    private float spawnRange;

    private int enemiesNumber;
    private GameObject[] enemies;

    // maxTimes is the max times that a navMeshAgent looks for a spawn position on navMesh, so that it won't lead to infinite loop
    private int maxTimes;
    private bool active;

    public void Start()
    {
        maxTimes = 50;

        spawnRange = 20f;
        
        // Random number of enemies
        // enemiesNumber = Random.Range(5, 10);
        enemiesNumber = 3;
        enemies = new GameObject[enemiesNumber];
        
        for (int i = 0; i < enemiesNumber; i++)
        {
            active = true;
            int type = Random.Range(0, enemiesTypes.enemeisList.Count - 1);
            enemies[i] = Instantiate(enemiesTypes.enemeisList[type], RandomPos(), Quaternion.identity, transform);
            enemies[i].name = "Enemy - " + enemiesTypes.enemeisList[type].name;
            if (!active)
            {
                enemies[i].SetActive(false);
                Debug.Log(enemies[i] + " is deactivated because its NavMeshAgent is not placed on a NavMesh.");
            }
        }
    }

    private Vector3 RandomPos()
    {
        Vector2 randomPoint;
        Vector3 randomPos;
        NavMeshHit hit;
        int i = 0;

        do
        {
            randomPoint = Random.insideUnitCircle * spawnRange;
            randomPos = transform.position + new Vector3(randomPoint.x, 0.0f, randomPoint.y);
            i++;
        }
        while (!NavMesh.SamplePosition(randomPos, out hit, 3f, 1) && i < maxTimes);

        if (i < maxTimes)
        {
            randomPos = hit.position;            
        }
        else
        {
            active = false;
        }

        return randomPos;
    }
}
