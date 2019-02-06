using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    [SerializeField]
    public List<TilePiece> TileMap;

    // Start is called before the first frame update
    public TileData()
    {
        TileMap = new List<TilePiece>();
    }
}
