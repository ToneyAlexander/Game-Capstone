using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IslandGeneratorTesting
{
    public static void testIndex(AdjacencyIndex index, int layersAboveBeach)
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
        if (layersAboveBeach > 0)
        {
            validTest(index, 66, 66, 3, true);
        }
    }
    public static void validTest(AdjacencyIndex index, int a, int b, int dir, bool expected)
    {
        if (index.isValid(index.indexToTile(a), index.indexToTile(b), dir) != expected)
        {
            Debug.Log("ERROR: TILE " + a + " -> TILE " + b + " IN DIRECTION " + dir + " EXPECTED: " + expected);
        }
    }

    public static void drawTileset(List<TilePiece> tiles, Vector3 startingLocation, int tileSize)
    {
        float zStart = startingLocation.z;
        startingLocation.x -= tileSize;
        for (int o = 1; o < tiles.Count; o++)
        {
            TilePiece currentPiece = tiles[o];
            GameObject newlyCreatedTile = Object.Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier, Quaternion.identity);
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
            if (o % 33 == 0)
            {
                startingLocation.x -= tileSize;
                startingLocation.z = zStart;
            }
        }
    }
}
