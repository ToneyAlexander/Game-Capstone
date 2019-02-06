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
    public int ID;
    public int rotation;
    public Vector3 modifier;

    public TilePiece(GameObject _prefab, int _ID, int _rotiation, Vector3 _modifier)
    {
        prefab = _prefab;
        ID = _ID;
        rotation = _rotiation;
        modifier = _modifier;  
    }
}
