using CCC.Items;
using CCC.ItemManagement;
using CCC.Stats;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
public class ControlStatBlock : MonoBehaviour
{
    public float Str { get; set; }
    public float StrMult { get; set; }


    public float Dex { get; set; }
    public float DexMult { get; set; }


    public float Myst { get; set; }
    public float MystMult { get; set; }


    public float Fort { get; set; }
    public float FortMult { get; set; }

    [HideInInspector]
    public List<TimedBuff> buffs;
    [HideInInspector]
    public List<Affliction> afflictions;
    private List<TimedBuff> buffsToAdd;
    private StatBlock stats;
    private float oldHpPrecent;
    private EquipmentUser inv;
    private PlayerClass pClass;
    private InitAbilities init;
    private bool applyTestStats;

    public void ApplyBuff(TimedBuff tb)
    {
        if(!buffsToAdd.Contains(tb))
            buffsToAdd.Add(tb);
    }

    public StatBlock getStats()
    {
        return stats;
    }

    public void AgeUp()
    {
        float rand = Random.Range(0f, pClass.bloodlineController.Age/2);

        Debug.Log("Age: " + pClass.bloodlineController.Age + " Rand: " + rand + " Fort: " + StatBlock.CalcMult(stats.AfflictRes, stats.AfflictResMult));
        if (rand > StatBlock.CalcMult(stats.AfflictRes, stats.AfflictResMult))
        {
            Affliction aff = init.BadAffList.afflictions[Random.Range(0, init.BadAffList.afflictions.Count)];
            Debug.Log("Gained affliction: " + aff.AfflictionName);
            afflictions.Add(aff);
        }
        else if (rand + 1f < StatBlock.CalcMult(stats.AfflictRes, stats.AfflictResMult))
        {
            Affliction aff = init.GoodAffList.afflictions[Random.Range(0, init.GoodAffList.afflictions.Count)];
            Debug.Log("Gained affliction: " + aff.AfflictionName);
            afflictions.Add(aff);
        }
    }

    void Awake()
    {
        applyTestStats = false;
        if (afflictions == null)
            afflictions = new List<Affliction>();
        init = GetComponent<InitAbilities>();
        stats = GetComponent<StatBlock>();
        inv = GetComponent<EquipmentUser>();
        pClass = GetComponent<PlayerClass>();

        buffs = new List<TimedBuff>();
        buffsToAdd = new List<TimedBuff>();
        oldHpPrecent = -10000f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        if (stats != null && stats.Friendly)
        {
            //applyTestStats = true;
        }

        StatsChanged();
    }

    void TestStatIncrease()
    {
        Str += 500;
        Dex += 500;
        Myst += 500;
        Fort += 500;
        stats.Armor = +1000f;
    }

    void ResetBaseStats()
    {
        Dex = 0;
        DexMult = 0;
        Str = 0;
        StrMult = 0;
        Fort = 0;
        FortMult = 0;
        Myst = 0;
        MystMult = 0;
        stats.CdrMult = 0f;
        stats.Spell = 0f;
        stats.SpellMult = 0f;
        stats.RangedAttack = 0f;
        stats.RangedAttackMult = 0f;
        stats.HealthBase = 0f;
        stats.HealthRegen = 0f;
        stats.MeleeAttack = 0f;
        stats.MeleeAttackMult = 0f;
        stats.MoveSpeed = 6.5f;
        stats.MoveSpeedMult = 0f;
        stats.HealthRegenMult = 0f;
        stats.HealthMult = 0f;
        stats.PhantomHpMult = 0f;
        stats.BloodDamage = 0f;
        stats.AttackSpeed = 0f;
        stats.AttackSpeedMult = 0f;
        stats.Armor = 0f;
        stats.ArmorMult = 0f;
        stats.AfflictResMult = 0f;
        stats.AfflictRes = 2f;
        stats.CritChance = 0.05f;
        stats.CritChanceMult = 0f;
        stats.CritDamage = 1f;
        stats.CritDamageMult = 0f;
        stats.Damage = 0f;
        stats.DamageMult = 0f;
        stats.PhysicalDamage = 0f;
        stats.PhysicalDamageMult = 0f;
        stats.MagicDamage = 0f;
        stats.MagicDamageMult = 0f;
        stats.MagicRes = 0f;
        stats.MagicResMult = 0f;
        stats.StatusRec = 0f;
        stats.StatusRecMult = 0f;
        stats.FlatDmgReduction = 0f;
        stats.FlatDmgReductionMult = 0f;
    }

    void ApplyStat(Stat stat)
    {
        switch(stat.Name)
        {
            case Stat.AFFLICT_RES:
                stats.AfflictRes += stat.Value;
                break;
            case Stat.AFFLICT_RES_MULT:
                stats.AfflictResMult += stat.Value;
                break;
            case Stat.ARMOR:
                stats.Armor += stat.Value;
                break;
            case Stat.ARMOR_MULT:
                stats.ArmorMult += stat.Value;
                break;
            case Stat.ATTACK_SPEED:
                stats.AttackSpeed += stat.Value;
                break;
            case Stat.ATTACK_SPEED_MULT:
                stats.AttackSpeedMult += stat.Value;
                break;
            case Stat.CDR:
                stats.Cdr += stat.Value;
                break;
            case Stat.CDR_MULT:
                stats.CdrMult += stat.Value;
                break;
            case Stat.CRIT_CHANCE:
                stats.CritChance += stat.Value;
                break;
            case Stat.CRIT_CHANCE_MULT:
                stats.CritChanceMult += stat.Value;
                break;
            case Stat.CRIT_DMG:
                stats.CritDamage += stat.Value;
                break;
            case Stat.CRIT_DMG_MULT:
                stats.CritDamageMult += stat.Value;
                break;
            case Stat.DEX:
                Dex += stat.Value;
                break;
            case Stat.DEX_MULT:
                DexMult += stat.Value;
                break;
            case Stat.DMG:
                stats.Damage += stat.Value;
                break;
            case Stat.DMG_MULT:
                stats.DamageMult += stat.Value;
                break;
            case Stat.FORT:
                Fort += stat.Value;
                break;
            case Stat.FORT_MULT:
                FortMult += stat.Value;
                break;
            case Stat.HEALTH:
                stats.HealthBase += stat.Value;
                break;
            case Stat.HEALTH_MULT:
                stats.HealthMult += stat.Value;
                break;
            case Stat.HEMO_PHANTOM_HP_MULT:
                stats.PhantomHpMult += stat.Value;
                break;
            case Stat.HEMO_BLOOD_POWER:
                stats.BloodDamage += stat.Value;
                break;
            case Stat.HEALTH_REGEN:
                stats.HealthRegen += stat.Value;
                break;
            case Stat.HEALTH_REGEN_MULT:
                stats.HealthRegenMult += stat.Value;
                break;
            case Stat.MAGIC_DMG:
                stats.MagicDamage += stat.Value;
                break;
            case Stat.MAGIC_DMG_MULT:
                stats.MagicDamageMult += stat.Value;
                break;
            case Stat.MAGIC_RES:
                stats.MagicRes += stat.Value;
                break;
            case Stat.MAGIC_RES_MULT:
                stats.MagicResMult += stat.Value;
                break;
            case Stat.MELEE_ATTACK:
                stats.MeleeAttack += stat.Value;
                break;
            case Stat.MELEE_ATTACK_MULT:
                stats.MeleeAttackMult += stat.Value;
                break;
            case Stat.MOVE_SPEED:
                stats.MoveSpeed += stat.Value;
                break;
            case Stat.MOVE_SPEED_MULT:
                stats.MoveSpeedMult += stat.Value;
                break;
            case Stat.MYST:
                Myst += stat.Value;
                break;
            case Stat.MYST_MULT:
                MystMult += stat.Value;
                break;
            case Stat.FLAT_DMG_REDUCTION:
                stats.FlatDmgReduction += stat.Value;
                break;
            case Stat.FLAT_DMG_REDUCTION_MULT:
                stats.FlatDmgReductionMult += stat.Value;
                break;
            case Stat.PHYS_DMG:
                stats.PhysicalDamage += stat.Value;
                break;
            case Stat.PHYS_DMG_MULT:
                stats.PhysicalDamageMult += stat.Value;
                break;
            case Stat.RANGED_ATTACK:
                stats.RangedAttack += stat.Value;
                break;
            case Stat.RANGED_ATTACK_MULT:
                stats.RangedAttackMult += stat.Value;
                break;
            case Stat.SPELL:
                stats.Spell += stat.Value;
                break;
            case Stat.SPELL_MULT:
                stats.SpellMult += stat.Value;
                break;
            case Stat.STATUS_REC:
                stats.StatusRec += stat.Value;
                break;
            case Stat.STATUS_REC_MULT:
                stats.StatusRecMult += stat.Value;
                break;
            case Stat.STR:
                Str += stat.Value;
                break;
            case Stat.STR_MULT:
                StrMult += stat.Value;
                break;
            default:
                Debug.LogError("Unknown Stat: " + stat.Name);
                break;
        }
    }

    public void StatsChanged()
    {
        if (oldHpPrecent >= -1000f && stats.HealthMax != 0.0f)
            oldHpPrecent = stats.HealthCur / stats.HealthMax;
        else
            oldHpPrecent = 1f;

        ResetBaseStats();

        if (applyTestStats)
        {
            TestStatIncrease();
        }

        if(inv != null)
        {
            foreach (Item item in inv.Equipment.Equipment.Values)
            {
                foreach (Stat s in item.Stats)
                {
                    ApplyStat(s);
                }
            }
        }

        if (pClass != null)
        {
            foreach (PerkPrototype perk in pClass.TakenPerks)
            {

                foreach (PerkStatEntry sp in perk.Stats)
                {
                    
                    ApplyStat(sp.StatInst);
                }
            }
        }
        
        foreach (TimedBuff tb in buffs)
        {
            foreach (Stat s in tb.Stats)
            {
                ApplyStat(s);
            }
        }

        foreach (Affliction aff in afflictions)
        {
            foreach (Stat s in aff.Stats)
            {
                ApplyStat(s);
            }
        }

        float strReal = StatBlock.CalcMult(Str, StrMult);
        stats.HealthBase += strReal * 10f;
        stats.HealthRegen += strReal / 20f;
        stats.MeleeAttackMult += strReal / 1000f;

        float dexReal = StatBlock.CalcMult(Dex, DexMult);
        stats.AttackSpeedMult += dexReal / 1000f;
        stats.MoveSpeedMult += dexReal / 750f;
        stats.RangedAttackMult += dexReal / 1000f;

        float mystReal = StatBlock.CalcMult(Myst, MystMult);
        stats.CdrMult += mystReal / 2000f;
        stats.SpellMult += mystReal / 1000f;

        float fortReal = StatBlock.CalcMult(Fort, FortMult);
        stats.MagicRes += fortReal / 5f;
        stats.AfflictRes += fortReal / 250f;
        stats.StatusRec += fortReal / 2000f;

        stats.HealthCur = oldHpPrecent * StatBlock.CalcMult(stats.HealthBase, stats.HealthMult);
    }

    // Update is called once per frame
    void Update()
    {
        bool needsUpdate = false;
        foreach (TimedBuff tb in buffsToAdd)
        {
            if (tb.IsUnique && buffs.Contains(tb))
            {
                buffs.Remove(tb);
            }
            buffs.Add(tb);
            needsUpdate = true;
        }
        buffsToAdd.Clear();
        for (int i = buffs.Count - 1; i > -1; --i)
        {
            TimedBuff tb = buffs[i];
            tb.DurationLeft -= Time.deltaTime * ( 1 + StatBlock.CalcMult(stats.StatusRec, stats.StatusRecMult));
            //Debug.Log(tb.BuffName + " at " + i + " has " + tb.DurationLeft + " left.");
            if (tb.DurationLeft <= 0f)
            {
                buffs.RemoveAt(i);
                Debug.Log(tb.BuffName + " expired.");
                needsUpdate = true;

            }
        }
        if(needsUpdate)
        {
            StatsChanged();
        }
    }

    public void OnHit(Damage dmg)
    {
        foreach(TimedBuff tb in dmg.buffs)
        {
            ApplyBuff(tb);
        }
    }
}
