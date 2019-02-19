using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;
using CCC.Abilities;

public class Ability
{
    public static Ability nullAbility = new Ability();
    public string AbilityName;
    public string TypeString;
    public bool use;
    public Sprite Icon;
    public GameObject Prefab;
    public List<Stat> Stats;

    private Ability()
    {

    }

    public Ability(AbilityPrototype ap)
    {
        AbilityName = ap.AbilityName;
        Icon = ap.Icon;
        Prefab = ap.Prefab;
        Stats = ap.Stats;
        TypeString = ap.TypeString;
        use = false;//TODO replace with proper listener
       // Script = ap.Script;
    }
}
