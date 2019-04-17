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

        spawnRange = 5f;
        
        // Random number of enemies
        enemiesNumber = Random.Range(3, 8);
        enemies = new GameObject[enemiesNumber];
        
        for (int i = 0; i < enemiesNumber; i++)
        {
            active = true;
            int type = SelectByChance();
            // enemies[i] = Instantiate(enemiesTypes.enemeisList[type], transform.position, Quaternion.identity, transform);
            enemies[i] = Instantiate(enemiesTypes.enemeisList[type], RandomPos(), Quaternion.identity, transform);
            enemies[i].name = "Enemy - " + enemiesTypes.enemeisList[type].name;
            if (!active)
            {
                enemies[i].SetActive(false);
                Debug.Log(enemies[i] + " is deactivated because its NavMeshAgent is not placed on a NavMesh.");
            }
        }
    }

    private int SelectByChance()
    {   
        float randomNum = 0;
        int selectNum = 0;
        if (enemiesTypes.theme == EnemiesList.Theme.Grass)
        {
            // Grass theme has higher chance to spawn bees
            randomNum = Random.value;
            if (randomNum <= 0.6f)
            {
                selectNum = Random.Range(0, 2);
            }
            else
            {
                selectNum = Random.Range(2, 5);
            }
        }
        else if (enemiesTypes.theme == EnemiesList.Theme.Swamp)
        {
            // Swamp theme has higher chance to spawn bats and plants
            randomNum = Random.value;
            if (randomNum < 0.75f)
            {
                selectNum = Random.Range(0, 2);
            }
            else
            {
                selectNum = Random.Range(2, 5);
            }
        }
        else if (enemiesTypes.theme == EnemiesList.Theme.Snow)
        {
            // Snow theme has higher chance to spawn fungi
            selectNum = Random.Range(0, 5);
        }
        else if (enemiesTypes.theme == EnemiesList.Theme.Desert)
        {
            // Desert theme has higher chance to spawn bats and spiders and low chance to spawn fungi
            randomNum = Random.value;
            if (randomNum < 0.7f)
            {
                selectNum = Random.Range(0, 2);
            }
            else if (randomNum >= 0.7f && randomNum < 0.9f)
            {
                selectNum = 2;
            } 
            else
            {
                selectNum = 3;
            }
        }
        else if (enemiesTypes.theme == EnemiesList.Theme.All)
        {
            selectNum = Random.Range(0, enemiesTypes.enemeisList.Count - 1);
        }
        return selectNum;
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
