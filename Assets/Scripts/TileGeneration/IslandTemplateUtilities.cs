using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class IslandTemplateUtilities
{
    public static bool[,][] initializeIslandFromTemplate(Texture template, int size, int tileCount, int fillerIndex, List<Vector2Int> updated)
    {
        GameObject canvas = Object.Instantiate(Resources.Load<GameObject>("IslandTemplateScreen"));
        canvas.GetComponent<Renderer>().material.SetTexture("_MainTex", template);

        int canvasSize = 100;

        float innerSize = size - 2;
        bool[,][] island = new bool[size, size][];

        float rayEnd = canvasSize > innerSize ? canvasSize : innerSize;

        Vector3 src = Vector3.zero;

        bool[] basic = new bool[tileCount];
        for (int b = 0; b < basic.Length; b++)
        {
            basic[b] = true;
        }
        bool[] filler = new bool[tileCount];
        filler[fillerIndex] = true;

        //Fill inner area with tiles corresponding to the image
        for (float i = -innerSize / 2f; i < innerSize / 2f; i++)
        {
            for (float j = -innerSize / 2f; j < innerSize / 2f; j++)
            {
                Vector2Int coords = new Vector2Int((int)(i + innerSize / 2f + 1), (int)(innerSize / 2f - j));
                if (isBlack(src, new Vector3(rayEnd / innerSize * (i + .5f), rayEnd / innerSize * (j + .5f), rayEnd) - src))
                {
                    island[coords.x, coords.y] = (bool[])basic.Clone();
                }
                else
                {
                    island[coords.x, coords.y] = (bool[])filler.Clone();
                    updated.Add(coords);
                }
            }
        }
        Object.Destroy(canvas);
        //Surround island with filler tiles
        for (int i = 0; i < size; i++)
        {
            island[0, i] = (bool[])filler.Clone();
            island[i, 0] = (bool[])filler.Clone();
            island[size - 1, i] = (bool[])filler.Clone();
            island[i, size - 1] = (bool[])filler.Clone();

            updated.Add(new Vector2Int(0, i));
            updated.Add(new Vector2Int(i, 0));
            updated.Add(new Vector2Int(size - 1, i));
            updated.Add(new Vector2Int(i, size - 1));
        }
        return island;
    }

    private static bool isBlack(Vector3 src, Vector3 dest)
    {
        RaycastHit hit;
        if (!Physics.Raycast(src, dest, out hit))
            return false;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return false;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        Color black = new Color(0, 0, 0, 1);
        
        if (tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).Equals(black))
        {
            //Debug.DrawRay(src, dest, Color.cyan, 200);
            return true;
        }
        else
        {
            //Debug.DrawRay(src, dest, Color.magenta, 200);
        }
        return false;
    }

    public static void floodFill(Vector2Int start, bool[,][] island, List<Vector2Int> updated, int tileCount, int floodIndex)
    {
        Queue<Vector2Int> locations = new Queue<Vector2Int>();
        locations.Enqueue(start);
        while (locations.Count > 0)
        {
            Vector2Int current = locations.Dequeue();
            if (!isTile(island[current.x, current.y], floodIndex))
            {
                island[current.x, current.y] = makeTile(floodIndex, tileCount);
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

    public static bool[] makeTile(int tileID, int tileCount)
    {
        bool[] r = new bool[tileCount];
        r[tileID] = true;
        return r;
    }
    public static bool[] makeBlankTile(int tileCount)
    {
        bool[] basic = new bool[tileCount];
        for (int b = 0; b < basic.Length; b++)
        {
            basic[b] = true;
        }
        return basic;
    }

    public static int countTrues(bool[] arr)
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

    public static bool isTile(bool[] Slot, int TileID)
    {
        return countTrues(Slot) == 1 && Slot[TileID] == true;
    }
    public static bool canBeTile(bool[] Slot, HashSet<int> TileIDs)
    {
        foreach(int i in TileIDs)
        {
            if (Slot[i])
            {
                return true;
            }
        }
        return false;
    }

    //https://en.wikipedia.org/wiki/Midpoint_circle_algorithm
    public static void drawcircle(int x0, int y0, int radius, bool[,][] island, List<Vector2Int> updated, int tileCount, int floodIndex)
    {
        int x = radius - 1;
        int y = 0;
        int dx = 1;
        int dy = 1;
        int err = dx - (radius << 1);

        while (x >= y)
        {
            island[x0 + x, y0 + y] = makeTile(floodIndex, tileCount);
            island[x0 + x, y0 + y] = makeTile(floodIndex, tileCount);
            island[x0 + y, y0 + x] = makeTile(floodIndex, tileCount);
            island[x0 - y, y0 + x] = makeTile(floodIndex, tileCount);
            island[x0 - x, y0 + y] = makeTile(floodIndex, tileCount);
            island[x0 - x, y0 - y] = makeTile(floodIndex, tileCount);
            island[x0 - y, y0 - x] = makeTile(floodIndex, tileCount);
            island[x0 + y, y0 - x] = makeTile(floodIndex, tileCount);
            island[x0 + x, y0 - y] = makeTile(floodIndex, tileCount);

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

    public static void drawcircleFlexible(int x0, int y0, int radius, bool[,][] island, List<Vector2Int> updated, int tileCount, int lineTileIndex)
    {
        int x = radius - 1;
        int y = 0;
        int dx = 1;
        int dy = 1;
        int err = dx - (radius << 1);

        while (x >= y)
        {
            try
            {
                island[x0 + x, y0 + y] = makeTile(lineTileIndex, tileCount);
                updated.Add(new Vector2Int(x0 + x, y0 + y));
            }
            catch { }
            try
            {
                island[x0 + y, y0 + x] = makeTile(lineTileIndex, tileCount);
                updated.Add(new Vector2Int(x0 + y, y0 + x));
            }
            catch { }

            try
            {
                island[x0 - y, y0 + x] = makeTile(lineTileIndex, tileCount);
                updated.Add(new Vector2Int(x0 - y, y0 + x));
            }
            catch { }

            try
            {
                island[x0 - x, y0 + y] = makeTile(lineTileIndex, tileCount);
                updated.Add(new Vector2Int(x0 - x, y0 + y));
            }
            catch { }

            try
            {
                island[x0 - x, y0 - y] = makeTile(lineTileIndex, tileCount);
                updated.Add(new Vector2Int(x0 - x, y0 - y));
            }
            catch { }

            try
            {
                island[x0 - y, y0 - x] = makeTile(lineTileIndex, tileCount);
                updated.Add(new Vector2Int(x0 - y, y0 - x));
            }
            catch { }

            try
            {
                island[x0 + y, y0 - x] = makeTile(lineTileIndex, tileCount);
                updated.Add(new Vector2Int(x0 + y, y0 - x));
            }
            catch { }

            try
            {
                island[x0 + x, y0 - y] = makeTile(lineTileIndex, tileCount);
                updated.Add(new Vector2Int(x0 + x, y0 - y));
            }
            catch { }

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

    public static void initializeCircleMap(bool[,][] island, List<Vector2Int> updated, int tileCount, int width, int height, int drawingTileID)
    {
        initializeEmptyMap(island, tileCount);

        drawcircle(width / 2, height / 2, (int)Mathf.Ceil(width / 2f), island, updated, tileCount, drawingTileID);
        floodFill(new Vector2Int(0, 0), island, updated, tileCount, drawingTileID);
        floodFill(new Vector2Int(0, island.GetLength(1) - 1), island, updated, tileCount, drawingTileID);
        floodFill(new Vector2Int(island.GetLength(0) - 1, 0), island, updated, tileCount, drawingTileID);
        floodFill(new Vector2Int(island.GetLength(0) - 1, island.GetLength(1) - 1), island, updated, tileCount, drawingTileID);
    }

    public static void initializeEmptyMap(bool[,][] island, int tileCount)
    {
        //initialize map
        bool[] basic = new bool[tileCount];
        for (int b = 0; b < basic.Length; b++)
        {
            basic[b] = true;
        }
        //basic.Clone();
        for (int m = 0; m < island.GetLength(0); m++)
        {
            for (int n = 0; n < island.GetLength(1); n++)
            {
                island[m, n] = (bool[])basic.Clone();
            }
        }
    }

    public static void initializeSquareMap(bool[,][] island, List<Vector2Int> updated, int tileCount, int width, int height, int drawingTileID)
    {
        //initialize map
        bool[] basic = new bool[tileCount];
        for (int b = 0; b < basic.Length; b++)
        {
            basic[b] = true;
        }
        //basic.Clone();
        for (int m = 0; m < island.GetLength(0); m++)
        {
            for (int n = 0; n < island.GetLength(1); n++)
            {
                if ((m == 0 || n == 0 || m == island.GetLength(0) - 1 || n == island.GetLength(1) - 1))
                {
                    island[m, n] = makeTile(drawingTileID, tileCount);
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
    }

    public static void initializeCrescentMap(bool[,][] island, List<Vector2Int> updated, int tileCount, int width, int height, int drawingTileID)
    {
        initializeCircleMap(island, updated, tileCount, width, height, drawingTileID);

        drawcircleFlexible(width, height / 2, (int)Mathf.Ceil(width / 2f), island, updated, tileCount, drawingTileID);
        floodFill(new Vector2Int(island.GetLength(0) - 2, island.GetLength(1) / 2), island, updated, tileCount, drawingTileID);
    }

    public static void initializeAntiCrescentMap(bool[,][] island, List<Vector2Int> updated, int tileCount, int width, int height, int drawingTileID)
    {
        initializeCircleMap(island, updated, tileCount, width, height, drawingTileID);

        drawcircleFlexible(width, height / 2, (int)Mathf.Ceil(width / 2f), island, updated, tileCount, drawingTileID);
        floodFill(new Vector2Int(2, island.GetLength(1) / 2), island, updated, tileCount, drawingTileID);
    }
}
