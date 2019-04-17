using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/ClassPrototype")]
public class ClassPrototype : ScriptableObject
{
    public Sprite image;
    public string description;
    public List<PerkPrototype> Perks;
    public List<PerkPrototype> Defaults;
    public PerkPrototype OnLevel;
}
