using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GenerateIsland : MonoBehaviour
{
    //TODO: OPTIMIZATIONS USING DATA STRUCTURES - DONT DO N^3 ALL THE TIME
    //TODO: SWAP N AND M OH FUCK

    //[SerializeField]
    //private TileData tileData;

    [SerializeField]
    private int tileSize;

    [SerializeField]
    private int ISLE_WIDE_HIGH;

    [SerializeField]
    Texture2D img;

    List<TilePiece> tiles;

    private AdjacencyIndex index;

    private List<GameObject> generatedMap;

    private int ISLE_WIDE = 50;
    private int ISLE_HIGH = 50;

    private int TILE_HEIGHT = 4;

    //max 3 right now
    //fix csv/code to be able to work with arbitaray number more
    private static int LAYERS_ABOVE_BEACH = 6;
    private static int TILES_PER_LAYER = 33;
    private int NUMBER_OF_TILES = TILES_PER_LAYER * (LAYERS_ABOVE_BEACH + 1) + 1;

    private int LESS_THAN_LAND_REGEN_COUNT = 10;

    private int WATER_INDEX = 0;
    private int LAND_INDEX = 33;

    // This code is so incredibly ugly rn. Planning on cleaning it up. 
    void Start()
    {
        //TODO: add backtracking and optimize search
        //TODO: remove these lines
        ISLE_WIDE = ISLE_WIDE_HIGH;
        ISLE_HIGH = ISLE_WIDE_HIGH;

        Vector3 startingLocation = Vector3.zero;

        //read in file 
        index = generateIndex(img);

        testIndex(index);

        //read in TileData
        TextAsset textFile = Resources.Load<TextAsset>("TileData");

        string[] lines = textFile.text.Split('\n');
        tiles = new List<TilePiece>();

        for (int i = 1; i < NUMBER_OF_TILES+1; i++)
        {
                if (i <= TILES_PER_LAYER + 1)
                {
                    string[] fields = lines[i].Split(',');
                    if (fields.Length >= 11)
                    {
                        TilePiece t = new TilePiece(Resources.Load<GameObject>(fields[10].Trim()), int.Parse(fields[1]), int.Parse(fields[5]), new Vector3(float.Parse(fields[2]), float.Parse(fields[3]), float.Parse(fields[4])));
                        tiles.Add(t);
                    }
                }
                else
                {
                    //higher layers
                    TilePiece lowClone = tiles[i - TILES_PER_LAYER - 1];
                    Vector3 newLoc = new Vector3(lowClone.modifier.x, lowClone.modifier.y + TILE_HEIGHT, lowClone.modifier.z);
                    TilePiece t = new TilePiece(lowClone.prefab, lowClone.ID + TILES_PER_LAYER, lowClone.rotation, newLoc);
                    tiles.Add(t);
                }
        }
        Debug.Log(tiles.Count);

        List<Vector2Int> updated = new List<Vector2Int>();

        bool[,][] island = initializeCircleMap(updated);
        island[ISLE_WIDE / 2, ISLE_HIGH / 2] = makeTile(NUMBER_OF_TILES-1);
        updated.Add(new Vector2Int(ISLE_WIDE / 2, ISLE_HIGH / 2));

        drawBullshit(Vector3.zero);

        //Propagate from initial set up
        propagate(island, updated, index);
        while (!finished(island))
        {
            observe(island, updated);
            propagate(island, updated, index);
        }
        generatedMap = drawMap(island, startingLocation);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            while (generatedMap.Count > 0)
            {
                Object.Destroy(generatedMap[0]);
                generatedMap.RemoveAt(0);
            }
            Vector3 startingLocation = Vector3.zero;
            List<Vector2Int> updated = new List<Vector2Int>();
            bool[,][] island = initializeCircleMap(updated);
            propagate(island, updated, index);
            while (!finished(island))
            {
                observe(island, updated);
                propagate(island, updated, index);
            }
            generatedMap = drawMap(island, startingLocation);
        }
    }

    private bool[,][] initializeCircleMap(List<Vector2Int> updated)
    {
        //initialize map
        bool[,][] island = new bool[ISLE_WIDE, ISLE_HIGH][];

        bool[] basic = new bool[NUMBER_OF_TILES];
        for(int b = 0; b < basic.Length; b++)
        {
            basic[b] = true;
        }
        //basic.Clone();
        for (int m = 0; m < island.GetLength(0); m++)
        {
            for (int n = 0; n < island.GetLength(1); n++)
            {
                if (false && (m == 0 || n == 0 || m == island.GetLength(0) - 1 || n == island.GetLength(1) - 1))
                {
                    island[m, n] = makeWater();
                    Vector2Int coords = new Vector2Int(m, n);
                    //TODO: speed up - list contains is slow?
                    if (!updated.Contains(coords))
                    {
                        updated.Add(coords);
                    }
                }
                else
                {
                    island[m, n] = (bool[])basic.Clone();
                }
            }
        }

        drawcircle(ISLE_WIDE / 2, ISLE_HIGH / 2, (int)Mathf.Ceil(ISLE_WIDE / 2f), island, updated);
        floodFill(new Vector2Int(0, 0), island, updated);
        floodFill(new Vector2Int(0, island.GetLength(1) - 1), island, updated);
        floodFill(new Vector2Int(island.GetLength(0) - 1, 0), island, updated);
        floodFill(new Vector2Int(island.GetLength(0) - 1, island.GetLength(1) - 1), island, updated);

        //water
        //island[0, 0] = makeWater();
        //island[0, ISLE_WIDE-1] = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true };
        //island[ISLE_HIGH-1, 0] = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true };
        //island[ISLE_HIGH-1, ISLE_WIDE-1] = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true };

        return island;
    }

    private int pixelToId(Color pixel)
    {
        int rgb = (int)(pixel.r*255);
        rgb = (rgb << 8) + (int)(pixel.g*255);
        rgb = (rgb << 8) + (int)(pixel.b*255);
        return rgb;
    }

    private AdjacencyIndex generateIndex(Texture2D adjacencyData)
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

                int pixelValue = pixelToId(adjacencyData.GetPixel(i, j));

                //Top 0
                if (j < adjacencyData.height - 1)
                {
                    int otherPixel = pixelToId(adjacencyData.GetPixel(i, j + 1));
                    if ((int)adjacencyData.GetPixel(i, j + 1).r != 1)
                    {
                        for(int layer = 0; layer <= LAYERS_ABOVE_BEACH; layer++)
                        {
                            index.Add(pixelValue+33*layer, otherPixel+33*layer, 0);
                        }
                    }
                }
                //Right 1
                if (i < adjacencyData.width - 1)
                {
                    int otherPixel = pixelToId(adjacencyData.GetPixel(i + 1, j));
                    if ((int)adjacencyData.GetPixel(i + 1, j).r != 1)
                    {
                        for (int layer = 0; layer <= LAYERS_ABOVE_BEACH; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 1);
                        }
                    }
                }
                //Bottom 2
                if (j > 0)
                {
                    int otherPixel = pixelToId(adjacencyData.GetPixel(i, j - 1));
                    if ((int)adjacencyData.GetPixel(i, j - 1).r != 1)
                    {
                        for (int layer = 0; layer <= LAYERS_ABOVE_BEACH; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 2);
                        }
                    }
                }
                //Left 3
                if (i > 0)
                {
                    int otherPixel = pixelToId(adjacencyData.GetPixel(i - 1, j));
                    if ((int)adjacencyData.GetPixel(i - 1, j).r != 1)
                    {
                        for (int layer = 0; layer <= LAYERS_ABOVE_BEACH; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 3);
                        }
                    }
                }
            }
        }
        return index;
    }

    private void floodFill(Vector2Int start, bool[,][] island, List<Vector2Int> updated)
    {
        Queue<Vector2Int> locations = new Queue<Vector2Int>();
        locations.Enqueue(start);
        while (locations.Count > 0)
        {
            Vector2Int current = locations.Dequeue();
            if (!isWater(island[current.x, current.y]))
            {
                island[current.x, current.y] = makeWater();
                //Add neighbors to queue
                if (current.x > 0)
                {
                    locations.Enqueue(new Vector2Int(current.x - 1, current.y));
                }
                if (current.y > 0)
                {
                    locations.Enqueue(new Vector2Int(current.x, current.y - 1));
                }
                if (current.x < island.GetLength(0) - 1)
                {
                    locations.Enqueue(new Vector2Int(current.x + 1, current.y));
                }
                if (current.y < island.GetLength(1) - 1)
                {
                    locations.Enqueue(new Vector2Int(current.x, current.y + 1));
                }
            }
        }
    }

    private bool[] makeWater()
    {
        return makeTile(WATER_INDEX);
    }

    private bool[] makeTile(int tileID)
    {
        bool[] r = new bool[NUMBER_OF_TILES];
        r[tileID] = true;
        return r;
    }

    private bool isWater(bool[] slot)
    {
        return countTrues(slot) == 1 && slot[WATER_INDEX] == true;
    }

    //https://en.wikipedia.org/wiki/Midpoint_circle_algorithm
    private void drawcircle(int x0, int y0, int radius, bool[,][] island, List<Vector2Int> updated)
    {
        int x = radius - 1;
        int y = 0;
        int dx = 1;
        int dy = 1;
        int err = dx - (radius << 1);

        while (x >= y)
        {
            island[x0 + x, y0 + y] = makeWater();
            island[x0 + x, y0 + y] = makeWater();
            island[x0 + y, y0 + x] = makeWater();
            island[x0 - y, y0 + x] = makeWater();
            island[x0 - x, y0 + y] = makeWater();
            island[x0 - x, y0 - y] = makeWater();
            island[x0 - y, y0 - x] = makeWater();
            island[x0 + y, y0 - x] = makeWater();
            island[x0 + x, y0 - y] = makeWater();

            updated.Add(new Vector2Int(x0 + x, y0 + y));
            updated.Add(new Vector2Int(x0 + x, y0 + y));
            updated.Add(new Vector2Int(x0 + y, y0 + x));
            updated.Add(new Vector2Int(x0 - y, y0 + x));
            updated.Add(new Vector2Int(x0 - x, y0 + y));
            updated.Add(new Vector2Int(x0 - x, y0 - y));
            updated.Add(new Vector2Int(x0 - y, y0 - x));
            updated.Add(new Vector2Int(x0 + y, y0 - x));
            updated.Add(new Vector2Int(x0 + x, y0 - y));

            if (err <= 0)
            {
                y++;
                err += dy;
                dy += 2;
            }

            if (err > 0)
            {
                x--;
                dx += 2;
                err += dx - (radius << 1);
            }
        }
    }

    private int countTrues(bool[] arr)
    {
        int count = 0;
        foreach (bool b in arr)
        {
            if (b)
            {
                count++;
            }
        }
        return count;
    }

    private List<GameObject> drawMap(bool[,][] island, Vector3 startingLocation)
    {
        List<GameObject> generatedMap = new List<GameObject>();
        //Draw the map (all at once - interlace with decision for speed)
        for (int n = 0; n < island.GetLength(0); n++)
        {
            for (int m = 0; m < island.GetLength(1); m++)
            {
                for (int o = 0; o < island[m, n].Length; o++)
                {
                    if (island[n, m][o] && o != WATER_INDEX)
                    {
                        int tileId = indexToTile(o);
                        if (tileId == -1)
                        {
                            Debug.Log("NEGATIVE TILE");
                            continue;
                        }
                        TilePiece currentPiece = tiles[o];
                        GameObject newlyCreatedTile = Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier, Quaternion.identity);
                        //TODO: REFLECTION
                        //GameObject reflection = Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier, Quaternion.identity);

                        newlyCreatedTile.transform.Rotate(Vector3.up, currentPiece.rotation);
                        //reflection.transform.Rotate(Vector3.up, currentPiece.rotation);
                        //reflection.transform.localScale = new Vector3(-1, -1, 1);

                        //apply adjustment to tile after rotation
                        if (currentPiece.rotation == 90)
                        {
                            newlyCreatedTile.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                            //reflection.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                        }
                        else if (currentPiece.rotation == 180)
                        {
                            newlyCreatedTile.transform.position += new Vector3(tileSize, 0, 0);
                            //reflection.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                        }
                        else if (currentPiece.rotation == 270)
                        {
                            newlyCreatedTile.transform.position += new Vector3(tileSize / 2, 0, -(tileSize / 2));
                            //reflection.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                        }
                        generatedMap.Add(newlyCreatedTile);
                        break;
                    }
                }
                startingLocation.x += tileSize;
            }
            startingLocation.x = 0;
            startingLocation.z += tileSize;
        }
        return generatedMap;
    }

    private void testIndex(AdjacencyIndex index)
    {
        validTest(index, 0, 0, 0, true);
        validTest(index, 0, 1, 0, true);
        validTest(index, 0, 2, 0, true);
        validTest(index, 0, 3, 0, false);
        validTest(index, 0, 4, 0, false);
        validTest(index, 0, 5, 0, false);
        validTest(index, 0, 6, 0, true);
        validTest(index, 0, 7, 0, false);
        validTest(index, 0, 8, 0, false);
        validTest(index, 0, 9, 0, false);
        validTest(index, 0, 10, 0, false);
        validTest(index, 0, 11, 0, false);
        validTest(index, 0, 12, 0, false);
        validTest(index, 0, 33, 0, false);

        validTest(index, 0, 0, 1, true);
        validTest(index, 0, 1, 1, false);
        validTest(index, 0, 2, 1, true);
        validTest(index, 0, 3, 1, true);
        validTest(index, 0, 4, 1, false);
        validTest(index, 0, 5, 1, false);
        validTest(index, 0, 6, 1, false);
        validTest(index, 0, 7, 1, true);
        validTest(index, 0, 8, 1, false);
        validTest(index, 0, 9, 1, false);
        validTest(index, 0, 10, 1, false);
        validTest(index, 0, 11, 1, false);
        validTest(index, 0, 12, 1, false);
        validTest(index, 0, 33, 1, false);

        validTest(index, 0, 0, 2, true);
        validTest(index, 0, 1, 2, false);
        validTest(index, 0, 2, 2, false);
        validTest(index, 0, 3, 2, true);
        validTest(index, 0, 4, 2, true);
        validTest(index, 0, 5, 2, false);
        validTest(index, 0, 6, 2, false);
        validTest(index, 0, 7, 2, false);
        validTest(index, 0, 8, 2, true);
        validTest(index, 0, 9, 2, false);
        validTest(index, 0, 10, 2, false);
        validTest(index, 0, 11, 2, false);
        validTest(index, 0, 12, 2, false);
        validTest(index, 0, 33, 2, false);

        validTest(index, 0, 0, 3, true);
        validTest(index, 0, 1, 3, true);
        validTest(index, 0, 2, 3, false);
        validTest(index, 0, 3, 3, false);
        validTest(index, 0, 4, 3, true);
        validTest(index, 0, 5, 3, true);
        validTest(index, 0, 6, 3, false);
        validTest(index, 0, 7, 3, false);
        validTest(index, 0, 8, 3, false);
        validTest(index, 0, 9, 3, false);
        validTest(index, 0, 10, 3, false);
        validTest(index, 0, 11, 3, false);
        validTest(index, 0, 12, 3, false);
        validTest(index, 0, 33, 3, false);


        validTest(index, 33, 0, 0, false);
        validTest(index, 33, 1, 0, false);
        validTest(index, 33, 2, 0, false);
        validTest(index, 33, 3, 0, false);
        validTest(index, 33, 4, 0, false);
        validTest(index, 33, 5, 0, false);
        validTest(index, 33, 6, 0, false);
        validTest(index, 33, 7, 0, false);
        validTest(index, 33, 8, 0, true);
        validTest(index, 33, 9, 0, false);
        validTest(index, 33, 10, 0, false);
        validTest(index, 33, 11, 0, true);
        validTest(index, 33, 12, 0, true);
        validTest(index, 33, 33, 0, true);

        validTest(index, 33, 0, 1, false);
        validTest(index, 33, 1, 1, false);
        validTest(index, 33, 2, 1, false);
        validTest(index, 33, 3, 1, false);
        validTest(index, 33, 4, 1, false);
        validTest(index, 33, 5, 1, true);
        validTest(index, 33, 6, 1, false);
        validTest(index, 33, 7, 1, false);
        validTest(index, 33, 8, 1, false);
        validTest(index, 33, 9, 1, true);
        validTest(index, 33, 10, 1, false);
        validTest(index, 33, 11, 1, false);
        validTest(index, 33, 12, 1, true);
        validTest(index, 33, 33, 1, true);

        validTest(index, 33, 0, 2, false);
        validTest(index, 33, 1, 2, false);
        validTest(index, 33, 2, 2, false);
        validTest(index, 33, 3, 2, false);
        validTest(index, 33, 4, 2, false);
        validTest(index, 33, 5, 2, false);
        validTest(index, 33, 6, 2, true);
        validTest(index, 33, 7, 2, false);
        validTest(index, 33, 8, 2, false);
        validTest(index, 33, 9, 2, true);
        validTest(index, 33, 10, 2, true);
        validTest(index, 33, 11, 2, false);
        validTest(index, 33, 12, 2, false);
        validTest(index, 33, 33, 2, true);

        validTest(index, 33, 0, 3, false);
        validTest(index, 33, 1, 3, false);
        validTest(index, 33, 2, 3, false);
        validTest(index, 33, 3, 3, false);
        validTest(index, 33, 4, 3, false);
        validTest(index, 33, 5, 3, false);
        validTest(index, 33, 6, 3, false);
        validTest(index, 33, 7, 3, true);
        validTest(index, 33, 8, 3, false);
        validTest(index, 33, 9, 3, false);
        validTest(index, 33, 10, 3, true);
        validTest(index, 33, 11, 3, true);
        validTest(index, 33, 12, 3, false);
        validTest(index, 33, 33, 3, true);

        validTest(index, 13, 14, 1, true);

        validTest(index, 66, 66, 3, true);

        /*
        validTest(index, 13, 0, 0, false);
        validTest(index, 13, 1, 0, false);
        validTest(index, 13, 2, 0, false);
        validTest(index, 13, 3, 0, false);
        validTest(index, 13, 4, 0, false);
        validTest(index, 13, 5, 0, false);
        validTest(index, 13, 6, 0, false);
        validTest(index, 13, 7, 0, false);
        validTest(index, 13, 8, 0, true);
        validTest(index, 13, 9, 0, false);
        validTest(index, 13, 10, 0, false);
        validTest(index, 13, 11, 0, true);
        validTest(index, 13, 12, 0, true);
        validTest(index, 13, 13, 0, true);

        validTest(index, 13, 0, 1, false);
        validTest(index, 13, 1, 1, false);
        validTest(index, 13, 2, 1, false);
        validTest(index, 13, 3, 1, false);
        validTest(index, 13, 4, 1, false);
        validTest(index, 13, 5, 1, true);
        validTest(index, 13, 6, 1, false);
        validTest(index, 13, 7, 1, false);
        validTest(index, 13, 8, 1, false);
        validTest(index, 13, 9, 1, true);
        validTest(index, 13, 10, 1, false);
        validTest(index, 13, 11, 1, false);
        validTest(index, 13, 12, 1, true);
        validTest(index, 13, 13, 1, true);

        validTest(index, 13, 0, 2, false);
        validTest(index, 13, 1, 2, false);
        validTest(index, 13, 2, 2, false);
        validTest(index, 13, 3, 2, false);
        validTest(index, 13, 4, 2, false);
        validTest(index, 13, 5, 2, false);
        validTest(index, 13, 6, 2, true);
        validTest(index, 13, 7, 2, false);
        validTest(index, 13, 8, 2, false);
        validTest(index, 13, 9, 2, true);
        validTest(index, 13, 10, 2, true);
        validTest(index, 13, 11, 2, false);
        validTest(index, 13, 12, 2, false);
        validTest(index, 13, 13, 2, true);

        validTest(index, 13, 0, 3, false);
        validTest(index, 13, 1, 3, false);
        validTest(index, 13, 2, 3, false);
        validTest(index, 13, 3, 3, false);
        validTest(index, 13, 4, 3, false);
        validTest(index, 13, 5, 3, false);
        validTest(index, 13, 6, 3, false);
        validTest(index, 13, 7, 3, true);
        validTest(index, 13, 8, 3, false);
        validTest(index, 13, 9, 3, false);
        validTest(index, 13, 10, 3, true);
        validTest(index, 13, 11, 3, true);
        validTest(index, 13, 12, 3, false);
        validTest(index, 13, 13, 3, true);*/

        /*validTest(index, 8, 0, 0, true);
        validTest(index, 8, 1, 0, false);
        validTest(index, 8, 2, 0, false);
        validTest(index, 8, 3, 0, false);
        validTest(index, 8, 4, 0, false);
        validTest(index, 8, 5, 0, false);
        validTest(index, 8, 6, 0, false);
        validTest(index, 8, 7, 0, false);
        validTest(index, 8, 8, 0, true);
        validTest(index, 8, 9, 0, false);
        validTest(index, 8, 10, 0, false);
        validTest(index, 8, 11, 0, true);
        validTest(index, 8, 12, 0, true);
        validTest(index, 8, 13, 0, false);

        validTest(index, 8, 0, 1, false);
        validTest(index, 8, 1, 1, true);
        validTest(index, 8, 2, 1, false);
        validTest(index, 8, 3, 1, false);
        validTest(index, 8, 4, 1, false);
        validTest(index, 8, 5, 1, false);
        validTest(index, 8, 6, 1, false);
        validTest(index, 8, 7, 1, false);
        validTest(index, 8, 8, 1, true);
        validTest(index, 8, 9, 1, false);
        validTest(index, 8, 10, 1, true);
        validTest(index, 8, 11, 1, false);
        validTest(index, 8, 12, 1, true);
        validTest(index, 8, 13, 1, false);

        validTest(index, 8, 0, 2, false);
        validTest(index, 8, 1, 2, false);
        validTest(index, 8, 2, 2, false);
        validTest(index, 8, 3, 2, true);
        validTest(index, 8, 4, 2, false);
        validTest(index, 8, 5, 2, false);
        validTest(index, 8, 6, 2, false);
        validTest(index, 8, 7, 2, false);
        validTest(index, 8, 8, 2, true);
        validTest(index, 8, 9, 2, false);
        validTest(index, 8, 10, 2, true);
        validTest(index, 8, 11, 2, false);
        validTest(index, 8, 12, 2, false);
        validTest(index, 8, 13, 2, true);

        validTest(index, 8, 0, 3, false);
        validTest(index, 8, 1, 3, false);
        validTest(index, 8, 2, 3, true);
        validTest(index, 8, 3, 3, false);
        validTest(index, 8, 4, 3, false);
        validTest(index, 8, 5, 3, false);
        validTest(index, 8, 6, 3, false);
        validTest(index, 8, 7, 3, false);
        validTest(index, 8, 8, 3, true);
        validTest(index, 8, 9, 3, false);
        validTest(index, 8, 10, 3, false);
        validTest(index, 8, 11, 3, true);
        validTest(index, 8, 12, 3, false);
        validTest(index, 8, 13, 3, true);*/
    }
    private void validTest(AdjacencyIndex index, int a, int b, int dir, bool expected)
    {
        if (index.isValid(indexToTile(a), indexToTile(b), dir) != expected)
        {
            Debug.Log("ERROR: TILE " + a + " -> TILE " + b + " IN DIRECTION " + dir + " EXPECTED: " + expected);
        }
    }

    private void drawBullshit(Vector3 startingLocation)
    {
        float zStart = startingLocation.z;
        startingLocation.x -= tileSize;
        for (int o = 1; o < NUMBER_OF_TILES; o++)
        {
            TilePiece currentPiece = tiles[o];
            GameObject newlyCreatedTile = Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier, Quaternion.identity);
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
            startingLocation.z += tileSize;
            if(o%33 == 0)
            {
                startingLocation.x -= tileSize;
                startingLocation.z = zStart;
            }
        }
    }

    private void propagate(bool[,][] island, List<Vector2Int> updated, AdjacencyIndex index)
    {
        //defn Propagate(coefficient_matrix):
        //Loop until no more cells are left to be update:
        while (updated.Count > 0)
        {
            Vector2Int current = updated[0];
            updated.RemoveAt(0);

            int x = current.x;
            int y = current.y;
            //For each neighboring cell:

            //Bottom 2
            if (y < island.GetLength(1) - 1)
            {
                if (makeArcConsistent(x, y + 1, x, y, 2, island, index))
                {
                    Vector2Int other = new Vector2Int(x, y + 1);
                    if (!updated.Contains(other))
                    {
                        updated.Add(other);
                    }
                }
            }
            //Left 3
            if (x > 0)
            {
                if (makeArcConsistent(x - 1, y, x, y, 3, island, index))
                {
                    Vector2Int other = new Vector2Int(x - 1, y);
                    if (!updated.Contains(other))
                    {
                        updated.Add(other);
                    }
                }
            }
            //Top 0
            if (y > 0)
            {
                if (makeArcConsistent(x, y - 1, x, y, 0, island, index))
                {
                    Vector2Int other = new Vector2Int(x, y - 1);
                    if (!updated.Contains(other))
                    {
                        updated.Add(other);
                    }
                }
            }
            //Right 1
            if (x < island.GetLength(0) - 1)
            {
                if (makeArcConsistent(x + 1, y, x, y, 1, island, index))
                {
                    Vector2Int other = new Vector2Int(x + 1, y);
                    if (!updated.Contains(other))
                    {
                        updated.Add(other);
                    }
                }
            }
        }
    }

    //Update (x, y)'s values based on what is allowed in (otherX, otherY)
    //Returns whether or not (x,y) had an update
    private bool makeArcConsistent(int x, int y, int otherX, int otherY, int direction, bool[,][] island, AdjacencyIndex index)
    {
        if (countTrues(island[x, y]) <= 1)
        {
            return false;
        }
        bool changed = false;
        for (int i = 0; i < island[x, y].Length; i++)
        {
            if (island[x, y][i])
            {
                bool possible = false;
                //For each pattern that is still potentially valid:
                for (int j = 0; j < island[otherX, otherY].Length; j++)
                {
                    if (island[otherX, otherY][j] && index.isValid(indexToTile(j), indexToTile(i), direction))
                    {
                        possible = true;
                        j = island[otherX, otherY].Length;
                    }
                }
                //If this point in the pattern no longer matches:
                //Set the array in the wave to false for this pattern
                changed = changed || (possible != island[x, y][i]);
                island[x, y][i] = possible;

                //if possible == false
                //Flag this cell as needing to be updated in the next iteration
                //Go other way - from the other guy to self?
            }
        }
        return changed;
    }

    private void observe(bool[,][] island, List<Vector2Int> updated)
    {
        //defn Observe(coefficient_matrix):
        Vector3Int cell = findLowestEntropy(island);
        int m_index = cell.x;
        int n_index = cell.y;

        //If there is a contradiction, throw an error and quit
        //If all cells are at entropy 0, processing is complete:
        //Return CollapsedObservations()
        //Else:
        //Choose a pattern by a random sample, weighted by the pattern frequency in the source data
        //Set the boolean array in this cell to false, except for the chosen pattern
        List<int> unchosenIndecies = new List<int>();
        for (int o = 0; o < island[m_index, n_index].Length; o++)
        {
            if (island[m_index, n_index][o])
            {
                unchosenIndecies.Add(o);
            }
        }

        if (unchosenIndecies.Count == 0)
        {
            return;
        }

        Vector2Int changed = new Vector2Int(m_index, n_index);
        if (!updated.Contains(changed))
        {
            updated.Add(changed);
        }
        int tileIndex = unchosenIndecies[Random.Range(0, unchosenIndecies.Count)];

        for(int r = 0; r < LESS_THAN_LAND_REGEN_COUNT; r++)
        {
            if (tileIndex < LAND_INDEX)
            {
                tileIndex = unchosenIndecies[Random.Range(0, unchosenIndecies.Count)];
            }
        }
        //if (tileIndex != LAND_INDEX)
        //{
        //    tileIndex = unchosenIndecies[Random.Range(0, unchosenIndecies.Count)];
        //}
        for (int o = 0; o < island[m_index, n_index].Length; o++)
        {
            island[m_index, n_index][o] = (o == tileIndex);
        }
    }

    //defn FindLowestEntropy(coefficient_matrix):
    //Return the cell that has the lowest greater-than-zero
    //entropy, defined as:
    //A cell with one valid pattern has 0 entropy
    //A cell with no valid patterns is a contradiction
    //Else: the entropy is based on the sum of the frequency
    //that the patterns appear in the source data, plus
    //Use some random noise to break ties and
    //near-ties.
    private Vector3Int findLowestEntropy(bool[,][] island)
    {
        int minEntropy = NUMBER_OF_TILES + 1;
        int m_index = 0;
        int n_index = 0;
        for (int m = 0; m < island.GetLength(0); m++)
        {
            for (int n = 0; n < island.GetLength(1); n++)
            {
                int entropy = 0;
                foreach (bool b in island[m, n])
                {
                    if (b)
                    {
                        entropy++;
                    }
                }
                if (entropy > 1 && entropy < minEntropy)
                {
                    minEntropy = entropy;
                    n_index = n;
                    m_index = m;
                    //short circuit on zero entropy
                }
            }
        }
        return new Vector3Int(m_index, n_index, minEntropy);
    }

    private bool finished(bool[,][] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (countTrues(map[i, j]) > 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private int indexToTile(int index)
    {
        return index;
    }
}
