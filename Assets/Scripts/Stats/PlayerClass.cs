using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Abilities;
using CCC.Stats;

[RequireComponent(typeof(ControlStatBlock))]
public class PlayerClass : MonoBehaviour
{
    [HideInInspector]
    public List<PerkPrototype> allPerks;
    [HideInInspector]
    public List<PerkPrototype> takenPerks;
    public AbilitySet abilities;
    public AbilitySlotDictionary abilDict;
    private ControlStatBlock stats;

    public static bool CheckPrereq(PerkPrototype p, List<PerkPrototype> taken)
    {
        int matched = 0;
        foreach (PerkPrototype t in taken)
        {
            if (p.BlockedBy.Contains(t))
            {
                return false;
            }
            if (p.Require.Contains(t))
            {
                ++matched;
            }
        }
        if (p.RequireAll)
        {
            return matched >= p.Require.Count;
        }
        else
        {
            int req = p.Require.Count > 0 ? 1 : 0;
            return matched >= req;
        }
    }

    public static void AddAbilityToParent(GameObject o, string typeString)
    {
        System.Type type = System.Type.GetType(typeString);
        Component fv = o.AddComponent(type);
    }

    void Awake()
    {
        takenPerks = new List<PerkPrototype>();
        stats = GetComponent<ControlStatBlock>();
        Debug.Log("stats loaded " + gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public bool TakePerk(PerkPrototype p)
    {
        if(CheckPrereq(p, takenPerks))
        {
            takenPerks.Add(p);
            stats.StatsChanged();
            if (abilities != null && abilDict != null)
            {
                foreach (AbilityPrototype a in p.grants)
                {
                    Ability instA = a.Instance;
                    abilities.Set.Add(instA.AbilityName, instA);
                    Debug.Log("Abil set len: " + abilities.Set.Count);
                    //TODO: repalce with ui thing
                    if (abilDict.GetAbility(AbilitySlot.One).Equals(Ability.nullAbility))
                    {
                        abilDict.SetSlotAbility(AbilitySlot.One, instA);
                        AddAbilityToParent(gameObject, instA.TypeString);
                    }
                    else if (abilDict.GetAbility(AbilitySlot.Two).Equals(Ability.nullAbility))
                    {
                        abilDict.SetSlotAbility(AbilitySlot.Two, instA);
                        AddAbilityToParent(gameObject, instA.TypeString);
                    }
                    else if (abilDict.GetAbility(AbilitySlot.Three).Equals(Ability.nullAbility))
                    {
                        abilDict.SetSlotAbility(AbilitySlot.Three, instA);
                        AddAbilityToParent(gameObject, instA.TypeString);
                    }
                    else if (abilDict.GetAbility(AbilitySlot.Four).Equals(Ability.nullAbility))
                    {
                        abilDict.SetSlotAbility(AbilitySlot.Four, instA);
                        AddAbilityToParent(gameObject, instA.TypeString);
                    }
                    else if (abilDict.GetAbility(AbilitySlot.Five).Equals(Ability.nullAbility))
                    {
                        abilDict.SetSlotAbility(AbilitySlot.Five, instA);
                        AddAbilityToParent(gameObject, instA.TypeString);
                    }
                }
                foreach (AbilityModifier aMod in p.Changes)
                {
                    if (abilities.Set.ContainsKey(aMod.AbilityName))
                    {
                        Ability abil = abilities.Set[aMod.AbilityName];
                        Stat stat = abil.Stats.Find(item => item.Name == aMod.StatName.InternalStatName);
                        stat.Value += aMod.Value;
                        Debug.Log("Ability Being changed: " + abil.AbilityName + " new: " + stat.Value);
                        abil.update = true;
                    }
                    else
                    {
                        Debug.Log("Player does not know this ability (Not neccarily an error, perk might modify more than one ability)");
                    }

                }
            }
            return true;
        } else
        {
            return false;
        }
    }
}
