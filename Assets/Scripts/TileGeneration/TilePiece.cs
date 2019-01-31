using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for tilePieces to be laid
 */
public class TilePiece
{

    public GameObject prefab;
    public string colorID;
    public int rotation;
    public Vector3 modif;

    public TilePiece(GameObject _prefab, string _colorID, int _rotiation, Vector3 _modif)
    {
        prefab = _prefab;
        colorID = _colorID;
        rotation = _rotiation;
        modif = _modif;
    }
}
