using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandData : ScriptableObject
{
    public struct Island
    {
        public Vector3 position;
        public string name;
        public int theme;
        public int level;
        public GameObject transfer;
        public Island(int i, Vector3 pos, string str, GameObject obj, int lev)
        {
            position = pos;
            name = str;
            theme = i;
            transfer = obj;
            level = lev;

        }
        public Island(int i, Vector3 pos, string str, GameObject obj)
        {
            position = pos;
            name = str;
            theme = i;
            transfer = obj;
            level = 99;
        }
    }
    Island currentIsland { get; set; }
}
