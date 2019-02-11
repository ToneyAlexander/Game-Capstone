using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

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
        replaceObjects(60, treeList.EnvironmentList, "Tree");
        replaceObjects(20, grassList.EnvironmentList, "Grass");
        replaceObjects(20, mediumObjectList.EnvironmentList, "MediumObject");
    }

    private void replaceObjects(int percentage, List<GameObject> environmentList, string type)
    {
        GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag(type);
        //a tree's spawn chance shall be 75%
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        RNGCryptoServiceProvider rngCsp2 = new RNGCryptoServiceProvider();
        byte[] randomNumber = new byte[100];
        byte[] randomNumber2 = new byte[100];
        foreach (GameObject x in listOfObjects)
        {
            rngCsp.GetBytes(randomNumber);
            byte spawnRoll = (byte)((randomNumber[0] % 100) + 1);
            if (spawnRoll <= percentage) //75% chance
            {
                rngCsp2.GetBytes(randomNumber2);
                byte index = (byte)((randomNumber[0] % environmentList.Count));
                GameObject newObject = Instantiate(environmentList[index], x.transform.position + environmentList[index].transform.position, Quaternion.identity);
                newObject.transform.rotation = new Quaternion(0, Random.rotation.y, 0, 1);
            }
            Destroy(x);
        }
    }
}
