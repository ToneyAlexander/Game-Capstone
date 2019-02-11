using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField]
    private EnvironmentData treeList;
    [SerializeField]
    private EnvironmentData grassList;
    [SerializeField]
    private EnvironmentData mediumObjectList;

    // Start is called before the first frame update
    void Start()
    {
        replaceObjects(75, treeList.EnvironmentList, "Tree");
        replaceObjects(90, grassList.EnvironmentList, "Grass");
        replaceObjects(90, mediumObjectList.EnvironmentList, "MediumObject");
    }

    private void replaceObjects(int percentage, List<GameObject> environmentList, string type)
    {
        GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag(type);
        //a tree's spawn chance shall be 75%
        System.Random rnd = new System.Random();
        rnd = new System.Random();
        System.Random rnd2 = new System.Random();
        foreach (GameObject x in listOfObjects)
        {
            int index = rnd.Next(0, environmentList.Count);
            int spawnRoll = rnd2.Next(0, 100);
            if (spawnRoll <= percentage) //75% chance
            {
                GameObject newObject = Instantiate(environmentList[index], x.transform.position + environmentList[index].transform.position, Quaternion.identity);
                newObject.transform.rotation = new Quaternion(0, Random.rotation.y, 0, 1);
            }
            Destroy(x);
        }
    }
}
