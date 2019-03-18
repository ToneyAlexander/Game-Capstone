using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IslandGeneratorUtilities
{
    public static List<TilePiece> CreateTileset(int tileCount, int TilesPerLayer, int TileHeight)
    {
        //read in TileData
        TextAsset textFile = Resources.Load<TextAsset>("TileData");

        string[] lines = textFile.text.Split('\n');
        List<TilePiece> tileset = new List<TilePiece>();

        for (int i = 1; i < tileCount + 1; i++)
        {
            if (i <= 2 * TilesPerLayer + 1)
            {
                string[] fields = lines[i].Split(',');
                if (fields.Length >= 11)
                {
                    //6,7,8,9
                    TilePiece t = new TilePiece(Resources.Load<GameObject>(fields[10].Trim()),
                        int.Parse(fields[1]), int.Parse(fields[5]),
                        new Vector3(float.Parse(fields[2]), float.Parse(fields[3]), float.Parse(fields[4])),
                        int.Parse(fields[6]) == 1, int.Parse(fields[7]) == 1, int.Parse(fields[8]) == 1, int.Parse(fields[9]) == 1);
                    tileset.Add(t);
                }
            }
            else
            {
                //higher layers
                TilePiece lowClone = tileset[i - TilesPerLayer - 2];
                Vector3 newLoc = new Vector3(lowClone.modifier.x, lowClone.modifier.y + TileHeight, lowClone.modifier.z);
                TilePiece t = new TilePiece(lowClone.prefab, lowClone.ID + TilesPerLayer, lowClone.rotation, newLoc, lowClone.navigability);
                tileset.Add(t);
            }
        }
        return tileset;
    }

    public static AdjacencyIndex GenerateIndex(Texture2D adjacencyData, int layersAboveBeach)
    {
        AdjacencyIndex index = new AdjacencyIndex();

        //Get patterns from sample and build propagator
        //THIS READS IMAGE FROM TOP LEFT TO BOTTOM RIGHT, LIKE A BOOK
        //YES IT DOES, UNITY READS IMAGES STRANGELY
        for (int j = adjacencyData.height - 1; j >= 0; j--) //y
        {
            for (int i = 0; i < adjacencyData.width; i++)//x
            {

                if ((int)adjacencyData.GetPixel(i, j).r == 1)
                {
                    continue;
                }

                int pixelValue = PixelToId(adjacencyData.GetPixel(i, j));

                //Top 0
                if (j < adjacencyData.height - 1)
                {
                    int otherPixel = PixelToId(adjacencyData.GetPixel(i, j + 1));
                    if ((int)adjacencyData.GetPixel(i, j + 1).r != 1)
                    {
                        for (int layer = 0; layer <= layersAboveBeach; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 0);
                        }
                    }
                }
                //Right 1
                if (i < adjacencyData.width - 1)
                {
                    int otherPixel = PixelToId(adjacencyData.GetPixel(i + 1, j));
                    if ((int)adjacencyData.GetPixel(i + 1, j).r != 1)
                    {
                        for (int layer = 0; layer <= layersAboveBeach; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 1);
                        }
                    }
                }
                //Bottom 2
                if (j > 0)
                {
                    int otherPixel = PixelToId(adjacencyData.GetPixel(i, j - 1));
                    if ((int)adjacencyData.GetPixel(i, j - 1).r != 1)
                    {
                        for (int layer = 0; layer <= layersAboveBeach; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 2);
                        }
                    }
                }
                //Left 3
                if (i > 0)
                {
                    int otherPixel = PixelToId(adjacencyData.GetPixel(i - 1, j));
                    if ((int)adjacencyData.GetPixel(i - 1, j).r != 1)
                    {
                        for (int layer = 0; layer <= layersAboveBeach; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 3);
                        }
                    }
                }
            }
        }
        return index;
    }

    public static int PixelToId(Color pixel)
    {
        int rgb = (int)(pixel.r * 255);
        rgb = (rgb << 8) + (int)(pixel.g * 255);
        rgb = (rgb << 8) + (int)(pixel.b * 255);
        return rgb;
    }
}
