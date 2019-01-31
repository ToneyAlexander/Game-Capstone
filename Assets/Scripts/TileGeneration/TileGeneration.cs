using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class TileGeneration : MonoBehaviour
{

    private List<TilePiece> colorTileDict;
    public GameObject noRise;
    public GameObject Quarter;
    public GameObject Half;
    public GameObject Eighth;
    public GameObject SevenEighth;
    public GameObject ThreeQuarter;

    // This code is so incredibly ugly rn. Planning on cleaning it up. 
    void Start()
    {
        colorTileDict = new List<TilePiece>();
        createColorToTileDictionary();
        colorTileDict.Add(new TilePiece(noRise, 255, 0, new Vector3(0, 1, 0)));
        colorTileDict.Add(new TilePiece(Quarter, 254, 0, Vector3.zero));
        colorTileDict.Add(new TilePiece(Eighth, 244, 270, Vector3.zero));
        colorTileDict.Add(new TilePiece(Eighth, 242, 180, Vector3.zero));
        colorTileDict.Add(new TilePiece(Eighth, 243, 90, Vector3.zero));
        colorTileDict.Add(new TilePiece(Eighth, 245, 0, Vector3.zero));
        colorTileDict.Add(new TilePiece(Half, 251, 0, Vector3.zero));
        colorTileDict.Add(new TilePiece(Half, 252, 180, Vector3.zero));
        colorTileDict.Add(new TilePiece(Half, 250, 270, Vector3.zero));
        colorTileDict.Add(new TilePiece(Half, 253, 90, Vector3.zero));
        colorTileDict.Add(new TilePiece(SevenEighth, 248, 0, Vector3.zero));
        colorTileDict.Add(new TilePiece(SevenEighth, 247, 270, Vector3.zero));
        colorTileDict.Add(new TilePiece(SevenEighth, 246, 90, Vector3.zero));
        colorTileDict.Add(new TilePiece(SevenEighth, 249, 180, Vector3.zero));

        //read in file 
        List<int> x = new List<int>();
        string owo = Directory.GetCurrentDirectory();
        string path = Application.dataPath + "/Images/TestImage2.png";
        byte[] byteImg = File.ReadAllBytes(path);
        Vector3 spot = new Vector3(0, 0, 0);
        Texture2D img = new Texture2D(20, 20);
        img.LoadImage(byteImg);
        int tileSize = 4;
        for (int i = img.height - 1; i >= 0; i--) //x
        {
            spot.z = 0;
            for (int j = 0; j < img.width; j++) //z
            {
                Color pixel = img.GetPixel(j, i);
                float test = pixel.r * (255);
                //go through map of (tile, color)

                foreach (TilePiece a in colorTileDict)
                {
                    if (test.Equals(a.colorID))
                    {
                        GameObject aaa = Instantiate(a.prefab, spot + a.modif, Quaternion.identity);
                        aaa.transform.Rotate(Vector3.up, a.rotation);

                        if (a.rotation == 90)
                        {

                            aaa.transform.position += new Vector3(2, 0, 2);
                        }
                        else if (a.rotation == 180)
                        {
                            aaa.transform.position += new Vector3(4, 0, 0);
                        }
                        else if (a.rotation == 270)
                        {
                            aaa.transform.position += new Vector3(2, 0, -2);
                        }

                        break;
                    }
                }
                spot.z += tileSize;
            }
            spot.x += tileSize;
        }

    }

    void createColorToTileDictionary()
    {
    }
}
