using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class TileGeneration : MonoBehaviour
{

    private Dictionary<string, TilePiece> colorTileDict;
    public GameObject noRise;
    public GameObject Quarter;
    public GameObject Half;
    public GameObject Eighth;
    public GameObject SevenEighth;
    public GameObject ThreeQuarter;

    // if you're looking at this code it's very ugly right now please look away
    void Start()
    {
        colorTileDict = new Dictionary<string, TilePiece>();
        createColorToTileDictionary();
        colorTileDict.Add("2E130FFF", new TilePiece(noRise, "2E130FFF", 0, new Vector3(0, 1, 0)));
        colorTileDict.Add("005E0DFF", new TilePiece(Quarter, "005E0DFF", 0, Vector3.zero));
        colorTileDict.Add("60440FFF", new TilePiece(Eighth, "60440FFF", 270, Vector3.zero));
        colorTileDict.Add("14010EFF", new TilePiece(Eighth, "14010EFF", 180, Vector3.zero));
        colorTileDict.Add("2C0D0EFF", new TilePiece(Eighth, "2C0D0EFF", 90, Vector3.zero));
        colorTileDict.Add("30000EFF", new TilePiece(Half, "30000EFF", 0, Vector3.zero));
        colorTileDict.Add("334B0FFF", new TilePiece(Half, "334B0FFF", 180, Vector3.zero));
        colorTileDict.Add("1B2D0FFF", new TilePiece(Half, "1B2D0FFF", 270, Vector3.zero));
        colorTileDict.Add("481E0EFF", new TilePiece(Half, "481E0EFF", 90, Vector3.zero));
        colorTileDict.Add("1A0C0FFF", new TilePiece(SevenEighth, "1A0C0FFF", 0, Vector3.zero));
        colorTileDict.Add("5A1D0FFF", new TilePiece(SevenEighth, "5A1D0FFF", 270, Vector3.zero));
        colorTileDict.Add("2A0D10FF", new TilePiece(SevenEighth, "2A0D10FF", 90, Vector3.zero));

        //read in file 
        List<int> x = new List<int>();
        string owo = Directory.GetCurrentDirectory();
        string path = Application.dataPath + "/Images/TestImage.png";
        byte[] byteImg = File.ReadAllBytes(path);
        Vector3 spot = new Vector3(0, 0, 0);
        Texture2D img = new Texture2D(20, 20);
        img.LoadImage(byteImg);
        int tileSize = 4;
        for (int i = img.height-1; i >= 0; i--) //x
        {
            spot.z = 0;
            for(int j = 0; j < img.width; j++) //z
            {
                Color pixel = img.GetPixel(j, i);
                string hexPixel = ColorUtility.ToHtmlStringRGBA(pixel);
                Debug.Log(hexPixel);
                //go through map of (tile, color)

                foreach (KeyValuePair<string, TilePiece> a in colorTileDict)
                {
                    if (hexPixel.Equals(a.Key))
                    {
                        GameObject aaa = Instantiate(a.Value.prefab, spot + a.Value.modif, Quaternion.identity);
                        aaa.transform.Rotate(Vector3.up, a.Value.rotation);

                        if (a.Value.rotation == 90)
                        {
                            
                            aaa.transform.position += new Vector3(2, 0, 2);
                        }else if(a.Value.rotation == 180) {
                            aaa.transform.position += new Vector3(4, 0, 0);
                        }
                        else if(a.Value.rotation == 270)
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
