using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for tilePieces to be laid
 */
 [System.Serializable]
public class TilePiece
{

    public GameObject prefab;
    public int colorID;
    public int rotation;
    public Vector3 modifier;

    public TilePiece(GameObject _prefab, int _colorID, int _rotiation, Vector3 _modifier)
    {
        prefab = _prefab;
        colorID = _colorID;
        rotation = _rotiation;
        modifier = _modifier;  
    }
}
