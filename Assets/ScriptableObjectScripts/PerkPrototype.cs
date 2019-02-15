using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;
using CCC.Abilities;

[CreateAssetMenu]
public class PerkPrototype : ScriptableObject
{
    public List<PerkPrototype> Require;
    public List<PerkPrototype> BlockedBy;
    public List<PerkPrototype> Children;
    public List<PerkPrototype> Blocks;
    public bool RequireAll;
    public List<PerkStatEntry> Stats;
    public string Name;
    public string Desc;
    public List<Ability> grants;
    public Vector2 uiCoords;
    public Sprite sprite;

    public void Awake()
    {
        Debug.Log("awake");
        Children.Clear();
        Blocks.Clear();
    }

    void OnEnable()
    {
        Debug.Log("enable");
        foreach (PerkPrototype p in Require)
        {
            p.Children.Add(this);
        }
        foreach (PerkPrototype p in BlockedBy)
        {
            p.Blocks.Add(this);
        }
    }
}
