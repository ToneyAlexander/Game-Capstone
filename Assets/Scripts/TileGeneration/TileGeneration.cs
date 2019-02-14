using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

public class TileGeneration : MonoBehaviour
{
    [SerializeField]
    private TileData tileData;

    [SerializeField]
    private int tileSize;

    [SerializeField]
    private EnvironmentData treeList;
    [SerializeField]
    private EnvironmentData grassList;
    [SerializeField]
    private EnvironmentData mediumObjectList;
    [SerializeField]
    private EnvironmentData particleEffects;
    [SerializeField]
    private EnvironmentData specialObjects;

    public NavMeshSurface surface;

    void Start()
    {
        generateTiles();
        replaceObjects(60, treeList.EnvironmentList, "Tree");
        replaceObjects(20, grassList.EnvironmentList, "Grass");
        replaceObjects(20, mediumObjectList.EnvironmentList, "Rock");
        replaceObjects(30, particleEffects.EnvironmentList, "Particles");
        replaceObjects(1, specialObjects.EnvironmentList, "SpecialObject");

        surface.BuildNavMesh();
    }
    
    private void generateTiles()
    {
        Vector3 startingLocation = new Vector3(0,0,0);

        //read in file 
        string path = Application.dataPath + "/Images/TestImage2.png";
        byte[] byteImg = File.ReadAllBytes(path);
        Texture2D img = new Texture2D(20, 20);
        img.LoadImage(byteImg);

        //iterate through image and place tiles in Unity scene: 
        for (int i = img.height - 1; i >= 0; i--) //x
        {
            startingLocation.z = 0;
            for (int j = 0; j < img.width; j++) //z
            {
                Color pixel = img.GetPixel(j, i);
                float pixelValue = pixel.r * (255);

                foreach (TilePiece currentPiece in tileData.TileMap)
                {
                    if (pixelValue.Equals(currentPiece.ID)) //found correct tile
                    {
                        GameObject newlyCreatedTile = Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier * Mathf.FloorToInt(Mathf.Sqrt(tileSize)), Quaternion.identity);
                        newlyCreatedTile.transform.Rotate(Vector3.up, currentPiece.rotation);

                        //apply adjustment to tile after rotation
                        if (currentPiece.rotation == 90)
                        {
                            newlyCreatedTile.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                        }
                        else if (currentPiece.rotation == 180)
                        {
                            newlyCreatedTile.transform.position += new Vector3(tileSize, 0, 0);
                        }
                        else if (currentPiece.rotation == 270)
                        {
                            newlyCreatedTile.transform.position += new Vector3(tileSize / 2, 0, -(tileSize / 2));
                        }
                        break;
                    }
                }
                startingLocation.z += tileSize;
            }
            startingLocation.x += tileSize;
        }
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
