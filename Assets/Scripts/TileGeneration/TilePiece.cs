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
    public bool[] navigability;

    public TilePiece(GameObject _prefab, int _ID, int _rotiation, Vector3 _modifier, bool top, bool right, bool bottom, bool left)
    {
        prefab = _prefab;
        ID = _ID;
        rotation = _rotiation;
        modifier = _modifier;
        bool[] nav = { top, right, bottom, left };
        navigability = nav;
    }

    public TilePiece(GameObject _prefab, int _ID, int _rotiation, Vector3 _modifier, bool[] _navigability)
    {
        prefab = _prefab;
        ID = _ID;
        rotation = _rotiation;
        modifier = _modifier;
        navigability = _navigability;
    }
}
