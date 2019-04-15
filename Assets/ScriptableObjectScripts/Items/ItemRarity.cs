using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Items;

[System.Serializable]
public class ItemRarity
{
    public ItemPrototype Item
    {
        get { return item; }
    }

    public float Rarity
    {
        get { return rarity; }
    }

    [SerializeField]
    private ItemPrototype item;

    [SerializeField]
    private float rarity;
}
