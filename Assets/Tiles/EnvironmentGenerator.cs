using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField]
    private EnvironmentData treeList;

    // Start is called before the first frame update
    void Start()
    {
        replaceObjectsToTrees();
    }

    private void replaceObjectsToTrees()
    {
        GameObject[] listOfTrees = GameObject.FindGameObjectsWithTag("Tree");
        //a tree's spawn chance shall be 75%
        System.Random rnd = new System.Random();
        rnd = new System.Random();
        System.Random rnd2 = new System.Random();
        foreach (GameObject x in listOfTrees)
        {
            int index = rnd.Next(0, treeList.EnvironmentList.Count);
            int spawnRoll = rnd2.Next(0, 100);
            if (spawnRoll <= 75) //75% chance
            {
                GameObject newObject = Instantiate(treeList.EnvironmentList[index], x.transform.position + treeList.EnvironmentList[index].transform.position, Quaternion.identity);
            }
            Destroy(x);
        }
    }
}
