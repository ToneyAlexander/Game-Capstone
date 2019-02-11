using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class TileGeneration : MonoBehaviour
{
    [SerializeField]
    private TileData tileData;

    [SerializeField]
    private int tileSize;

    void Start()
    {
        Vector3 startingLocation = Vector3.zero;

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
                        GameObject newlyCreatedTile = Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier *Mathf.FloorToInt(Mathf.Sqrt(tileSize)), Quaternion.identity);
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
                            newlyCreatedTile.transform.position += new Vector3(tileSize / 2, 0, -(tileSize/2));
                        }
                        break;
                    }
                }
                startingLocation.z += tileSize;
            }
            startingLocation.x += tileSize;
        }
    }
}
