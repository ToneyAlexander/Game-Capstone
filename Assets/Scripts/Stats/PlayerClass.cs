using CCC.Abilities;
using CCC.Combat.Perks;
using CCC.Stats;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ControlStatBlock))]
public class PlayerClass : MonoBehaviour
{
    [HideInInspector]
    public List<PerkPrototype> allPerks;

    [HideInInspector]
    public List<PerkPrototype> TakenPerks;

    //public IList<PerkPrototype> TakenPerks
    //{
    //    get { return takenPerks.Perks; }
    //}

    /// <summary>
    /// The perks that the player currently has taken.
    /// </summary>
    [SerializeField]
    private PerkList takenPerks = null;

    [HideInInspector]
    public PerkPrototype onLevelUp;
    public AbilitySet abilities;
    public AbilitySlotDictionary abilDict;

    public BloodlineController bloodlineController;

    private ControlStatBlock stats;
    private InitAbilities init;

    public LevelExpStore PlayerLevelExp;
    
    private readonly float expToLevelInc = 1.25f;

    public static bool CheckPrereq(PerkPrototype p, IList<PerkPrototype> taken)
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
        if (o.GetComponent(type) == null)
        {
            Component fv = o.AddComponent(type);
        }
    }

    private void Awake()
    {
        // NOTE: This Component's TakenPerks field gets its value from the 
        // takenPerks field. That field must be loaded (the Load method must 
        // have been called on it) before its Perks property will have a 
        // non-null value in it. Therefore, the Load method of that PerkList is 
        // called right here. This will work so long as no other Component in 
        // the same scene as this Component requires the same PerkList to be 
        // loaded for its Awake method. Normally, this PerkList would be 
        // loaded in a GameState's Enter method but because a GameState's 
        // Enter method is only guaranteed to run before any Component's Start 
        // method, we must load it here. If any other Component's Awake method 
        // in the same scene as this Component required this PerkList to be 
        // loaded, it must have its Load method called on it in the GameState 
        // just before the GameState that all those Components exist in. For 
        // example, it would probably be loaded in HubGameState's Enter method.
        // If this logic (besides the takenPerks.Load() which could go in a 
        // GameState's Enter method) was moved to Start, it may possibly work 
        // but I don't know enough about the other Components that may rely on 
        // PlayerClass's Awake method doing some set up that they need for 
        // their own Start methods.
        // - Wesley
        takenPerks.Load();

        // If takenPerks is empty, it means that we're either looking at the 
        // null perk list or a new player perk list that is empy.
        if (takenPerks.Perks.Count <= 0)
        {
            // We have to make an entirely new list otherwise the null perk 
            // list's list would be overwritten.
            TakenPerks = new List<PerkPrototype>();
        }
        else
        {
            // Not a null perk list
            TakenPerks = takenPerks.Perks;
        }

        stats = GetComponent<ControlStatBlock>();
        init = GetComponent<InitAbilities>();
    }

    public void IncreaseAge()
    {
        bloodlineController.ageUp();

        if (bloodlineController.Age < 3)
        {
            TakenPerks.Add(init.Young);
        }
        else if (bloodlineController.Age < 5)
        {
            TakenPerks.Add(init.Middle);
        }
        else
        {
            TakenPerks.Add(init.Old);
        }

        stats.AgeUp();

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
        if (PlayerLevelExp != null)
        {
            ++PlayerLevelExp.PerkPoints;
            ++PlayerLevelExp.Level;
        }
        if (onLevelUp != null)
        {
            TakenPerks.Add(onLevelUp);
            stats.StatsChanged();
        }
    }

    public void TakeDefaults(List<PerkPrototype> perks)
    {
        foreach(PerkPrototype p in perks)
        {
            if (!takenPerks.Perks.Contains(p))
            {
                bool succ = TakePerk(p, false);
            }

            GrantLoadedAbilities();
        }
    }

    public bool TakePerk(PerkPrototype p, bool needsPerkPoint = true)
    {
        if (!needsPerkPoint || (PlayerLevelExp != null && PlayerLevelExp.PerkPoints > 0))
        {
            if (CheckPrereq(p, TakenPerks))
            {
                if(needsPerkPoint)
                {
                    --PlayerLevelExp.PerkPoints;
                }
                TakenPerks.Add(p);
                stats.StatsChanged();
                if (abilities != null && abilDict != null)
                {
                    foreach (AbilityPrototype a in p.grants)
                    {
                        var ability = Ability.FromPrototype(a);
                        abilities.Set.Add(ability.AbilityName, ability);

                        //TODO: repalce with ui thing
                        if (abilDict.GetAbility(AbilitySlot.One).AbilityName == Ability.Null.AbilityName)
                        {
                            abilDict.SetSlotAbility(AbilitySlot.One, ability);
                            AddAbilityToParent(gameObject, ability.TypeString);
                        }
                        else if (abilDict.GetAbility(AbilitySlot.Two).AbilityName == Ability.Null.AbilityName)
                        {
                            abilDict.SetSlotAbility(AbilitySlot.Two, ability);
                            AddAbilityToParent(gameObject, ability.TypeString);
                        }
                        else if (abilDict.GetAbility(AbilitySlot.Three).AbilityName == Ability.Null.AbilityName)
                        {
                            abilDict.SetSlotAbility(AbilitySlot.Three, ability);
                            AddAbilityToParent(gameObject, ability.TypeString);
                        }
                        else if (abilDict.GetAbility(AbilitySlot.Four).AbilityName == Ability.Null.AbilityName)
                        {
                            abilDict.SetSlotAbility(AbilitySlot.Four, ability);
                            AddAbilityToParent(gameObject, ability.TypeString);
                        }
                        else if (abilDict.GetAbility(AbilitySlot.Five).AbilityName == Ability.Null.AbilityName)
                        {
                            abilDict.SetSlotAbility(AbilitySlot.Five, ability);
                            AddAbilityToParent(gameObject, ability.TypeString);
                        }
                        else if (abilDict.GetAbility(AbilitySlot.Six).AbilityName == Ability.Null.AbilityName)
                        {
                            abilDict.SetSlotAbility(AbilitySlot.Six, ability);
                            AddAbilityToParent(gameObject, ability.TypeString);
                        }
                    }
                    foreach (AbilityModifier aMod in p.Changes)
                    {
                        if (abilities.Set.ContainsKey(aMod.AbilityName))
                        {
                            Ability abil = abilities.Set[aMod.AbilityName];
                            Stat stat = abil.Stats.Find(item => item.Name == aMod.StatName.InternalStatName);
                            stat.Value += aMod.Value;
                            abil.update = true;
                        }

                    }
                }
                return true;
            }
        }
        return false;
    }

    private void GrantLoadedAbilities()
    {
        foreach (var ability in abilDict.Abilities)
        {
            if (ability.AbilityName != Ability.Null.AbilityName)
            {
                AddAbilityToParent(gameObject, ability.TypeString);
            }
        }
    }
}
