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
    [HideInInspector]
    public PerkPrototype onLevelUp;
    public AbilitySet abilities;
    public AbilitySlotDictionary abilDict;

    public BloodlineController bloodlineController;

    private ControlStatBlock stats;
    private InitAbilities init;

    public LevelExpStore PlayerLevelExp;
    
    private readonly float expToLevelInc = 1.25f;

    public static bool CheckPrereq(PerkPrototype p, List<PerkPrototype> taken)
    {
        if(taken.Contains(p))
        {
            //Debug.LogError("Attempted to take perk that is already owned");
            return false;
        }
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
        init = GetComponent<InitAbilities>();
        
        if(PlayerLevelExp != null && PlayerLevelExp.Level < 1)
        {
            PlayerLevelExp.ExpToLevel = 100;
            PlayerLevelExp.PerkPoints = 1;
            PlayerLevelExp.Level = 1;
        }
    }

    public void IncreaseAge()
    {
        bloodlineController.ageUp();
        if (bloodlineController.Age < 3)
        {
            takenPerks.Add(init.Young);
        }
        else if (bloodlineController.Age < 5)
        {
            takenPerks.Add(init.Middle);
        }
        else
        {
            takenPerks.Add(init.Old);
        }
        stats.StatsChanged();
    }

    public void ApplyExp(float toAdd)
    {
        //Debug.Log("Gained " + toAdd + " exp.");
        PlayerLevelExp.Exp += toAdd;
        while (PlayerLevelExp.Exp > PlayerLevelExp.ExpToLevel)
        {
            LevelUp();
            PlayerLevelExp.Exp -= PlayerLevelExp.ExpToLevel;
            PlayerLevelExp.ExpToLevel *= expToLevelInc;
        }
    }

    public void LevelUp()
    {
        //Debug.Log("Leveled Up!");
        if (PlayerLevelExp != null)
        {
            ++PlayerLevelExp.PerkPoints;
            ++PlayerLevelExp.Level;
        }
        if (onLevelUp != null)
        {
            takenPerks.Add(onLevelUp);
            stats.StatsChanged();
        }
    }

    public void TakeDefaults(List<PerkPrototype> perks)
    {
        foreach(PerkPrototype p in perks)
        {
            bool succ = TakePerk(p, false);
            if (!succ)
            {
                Debug.LogError("Class Defaults Configured Incorrectly");
            }
        }
    }

    public bool TakePerk(PerkPrototype p, bool needsPerkPoint = true)
    {
        if (!needsPerkPoint || (PlayerLevelExp != null && PlayerLevelExp.PerkPoints > 0))
        {
            if (CheckPrereq(p, takenPerks))
            {
                if(needsPerkPoint)
                {
                    --PlayerLevelExp.PerkPoints;
                }
                takenPerks.Add(p);
                stats.StatsChanged();
                if (abilities != null && abilDict != null)
                {
                    foreach (AbilityPrototype a in p.grants)
                    {
                        Ability instA = a.Instance;
                        abilities.Set.Add(instA.AbilityName, instA);
                        //Debug.Log("Abil set len: " + abilities.Set.Count);
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
                            //Debug.Log("Ability Being changed: " + abil.AbilityName + " new: " + stat.Value);
                            abil.update = true;
                        }

                    }
                }
                return true;
            }
        }
        return false;
    }
}
