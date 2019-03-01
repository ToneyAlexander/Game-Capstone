using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacencyIndex
{
    Dictionary<int, Dictionary<int, HashSet<int>>> adjacency;
    public AdjacencyIndex()
    {
        adjacency = new Dictionary<int, Dictionary<int, HashSet<int>>>();
    }

    public void Add(int ATileID, int BTileID, int Direction)
    {
        if (!adjacency.ContainsKey(ATileID))
        {
            adjacency.Add(ATileID, new Dictionary<int, HashSet<int>>());
        }
        if (!adjacency[ATileID].ContainsKey(Direction))
        {
            adjacency[ATileID].Add(Direction, new HashSet<int>());
        }
        if (!adjacency[ATileID][Direction].Contains(BTileID))
        {
            adjacency[ATileID][Direction].Add(BTileID);
        }
    }

    public bool isValid(int ATileID, int BTileID, int Direction)
    {
        if (!adjacency.ContainsKey(ATileID))
        {
            return false;
        }
        if (!adjacency[ATileID].ContainsKey(Direction))
        {
            return false;
        }
        return adjacency[ATileID][Direction].Contains(BTileID);
    }

    public int indexToTile(int index)
    {
        return index;
    }
}