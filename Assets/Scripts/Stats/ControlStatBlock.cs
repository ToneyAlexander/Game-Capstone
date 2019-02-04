using System.Collections;
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

    private StatBlock stats;
    private float oldHpPrecent;
    private InventoryUser inv;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatBlock>();
        inv = GetComponent<InventoryUser>();
        Str = 10f;
        Dex = 10f;
        Myst = 10f;
        Fort = 10f;

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
        stats.AfflictRes = 0f;
        stats.MagicRes = 0f;
        stats.StatusRec = 0f;
        stats.CdrMult = 0f;
        stats.SpellMult = 0f;
        stats.AttackSpeedMult = 0f;
        stats.MoveSpeedMult = 0f;
        stats.RangedAttackMult = 0f;
        stats.HealthBase = 0f;
        stats.HealthRegen = 0f;
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
        //switch statemnt
    }

    void StatsChanged()
    {
        if (oldHpPrecent >= -1000f)
            oldHpPrecent = stats.HealthCur / stats.HealthMax;
        else
            oldHpPrecent = 1f;

        ResetBaseStats();

        TestStatIncrease();

        if(inv != null)
        {
            foreach (Item item in inv.Inventory.Items)
            {
                foreach (Stat s in item.Stats)
                {
                    ApplyStat(s);
                }
            }
        }

        //loop through class traits
        //loop through buffs
        //loop through debuffs

        float strReal = Str * (1 + StrMult);
        stats.HealthBase += strReal * 10f;
        stats.HealthRegen += strReal / 20f;
        stats.MeleeAttackMult += strReal / 1000f;

        float dexReal = Dex * (1 + DexMult);
        stats.AttackSpeedMult += dexReal / 1000f;
        stats.MoveSpeedMult += dexReal / 500f;
        stats.RangedAttackMult += dexReal / 1000f;

        float mystReal = Myst * (1 + MystMult);
        stats.CdrMult += mystReal / 2000f;
        stats.SpellMult += mystReal / 1000f;

        float fortReal = Fort * (1 + FortMult);
        stats.MagicRes += fortReal / 5f;
        stats.AfflictRes += fortReal / 5f;
        stats.StatusRec += fortReal / 1000f;

        stats.HealthCur = oldHpPrecent * stats.HealthBase * (1 + stats.HealthMult);
    }

    // Update is called once per frame
    void Update()
    {
        //update buff timers
        //update debuff timers
        //remove expired buffs
        //remove expired debuffs
    }
}
