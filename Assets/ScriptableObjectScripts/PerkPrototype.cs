using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

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
    public Vector2 uiCoords;
    public Sprite sprite;
}
