using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject meleeEnemy;
    public int meleeNumber;
    private GameObject[] meleeEnemies;

    public GameObject rangedEnemy;
    public int rangedNumber;
    private GameObject[] rangedEnemies;

    void Start()
    {
        meleeEnemies = new GameObject[meleeNumber];
        for (int i = 0; i < meleeNumber; i++)
        {
            meleeEnemies[i] = Instantiate(meleeEnemy, RandomPos(), Quaternion.identity);
            meleeEnemies[i].name = "Enemy " + (i + 1);

            meleeEnemies[i].transform.parent = transform;
        }

        rangedEnemies = new GameObject[rangedNumber];
        for (int i = 0; i < rangedNumber; i++)
        {
            rangedEnemies[i] = Instantiate(rangedEnemy, RandomPos(), Quaternion.identity);
            rangedEnemies[i].name = "Enemy " + (i + 1);

            rangedEnemies[i].transform.parent = transform;
        }
    }

    private Vector3 RandomPos()
    {
        // According to the world scale
        return new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));
    }
}
