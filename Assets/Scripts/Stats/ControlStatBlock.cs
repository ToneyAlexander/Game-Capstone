﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Items;
using CCC.Stats;

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

    private List<TimedBuff> buffs;
    private StatBlock stats;
    private float oldHpPrecent;
    private EquipmentUser inv;
    private PlayerClass pClass;

    public StatBlock GetStatBlock()
    {
        return stats;
    }

    public void ApplyBuff(TimedBuff tb)
    {
        if (!tb.IsUnique || !buffs.Contains(tb))
        {
            buffs.Add(tb);
            StatsChanged();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatBlock>();
        inv = GetComponent<EquipmentUser>();
        pClass = GetComponent<PlayerClass>();

        buffs = new List<TimedBuff>();
        oldHpPrecent = -10000f;

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
        Dex = 10;
        DexMult = 0;
        Str = 10;
        StrMult = 0;
        Fort = 10;
        FortMult = 0;
        Myst = 10;
        MystMult = 0;
        stats.AfflictRes = 0f;
        stats.MagicRes = 0f;
        stats.StatusRec = 0f;
        stats.CdrMult = 0f;
        stats.Spell = 0f;
        stats.SpellMult = 0f;
        stats.AttackSpeedMult = 0f;
        stats.MoveSpeedMult = 0f;
        stats.RangedAttack = 0f;
        stats.RangedAttackMult = 0f;
        stats.HealthBase = 0f;
        stats.HealthRegen = 0f;
        stats.MeleeAttack = 0f;
        stats.MeleeAttackMult = 0f;
        stats.MoveSpeed = 10f;
        stats.HealthRegenMult = 0f;
        stats.HealthMult = 0f;
        stats.AttackSpeed = 1f;
        stats.Armor = 0f;
        stats.ArmorMult = 0f;
        stats.AfflictResMult = 0f;
        stats.CritChance = 0.05f;
        stats.CritChanceMult = 0f;
        stats.CritDamage = 1.5f;
        stats.CritDamageMult = 0f;
        stats.DamageMult = 0f;
        stats.MagicResMult = 0f;
        stats.StatusRecMult = 0f;
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
            case Stat.HEALTH_REGEN:
                stats.HealthRegen += stat.Value;
                break;
            case Stat.HEALTH_REGEN_MULT:
                stats.HealthRegenMult += stat.Value;
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
        }
    }

    public void StatsChanged()
    {
        if (oldHpPrecent >= -1000f)
            oldHpPrecent = stats.HealthCur / stats.HealthMax;
        else
            oldHpPrecent = 1f;

        ResetBaseStats();

        TestStatIncrease();

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
            foreach (PerkPrototype perk in pClass.takenPerks)
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

        float strReal = StatBlock.CalcMult(Str, StrMult);
        stats.HealthBase += strReal * 10f;
        stats.HealthRegen += strReal / 20f;
        stats.MeleeAttackMult += strReal / 1000f;

        float dexReal = StatBlock.CalcMult(Dex, DexMult);
        stats.AttackSpeedMult += dexReal / 1000f;
        stats.MoveSpeedMult += dexReal / 500f;
        stats.RangedAttackMult += dexReal / 1000f;

        float mystReal = StatBlock.CalcMult(Myst, MystMult);
        stats.CdrMult += mystReal / 2000f;
        stats.SpellMult += mystReal / 1000f;

        float fortReal = StatBlock.CalcMult(Fort, FortMult);
        stats.MagicRes += fortReal / 5f;
        stats.AfflictRes += fortReal / 5f;
        stats.StatusRec += fortReal / 1000f;

        stats.HealthCur = oldHpPrecent * StatBlock.CalcMult(stats.HealthBase, stats.HealthMult);
    }

    // Update is called once per frame
    void Update()
    {
        bool needsUpdate = false;
        for(int i = buffs.Count - 1; i > -1; --i)
        {
            TimedBuff tb = buffs[i];
            tb.DurationLeft -= Time.deltaTime;
            Debug.Log(tb.BuffName + " at " + i + " has " + tb.DurationLeft + " left.");
            if (tb.DurationLeft <= 0f)
            {
                buffs.RemoveAt(i);
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
