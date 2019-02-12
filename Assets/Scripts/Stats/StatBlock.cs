using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBlock : MonoBehaviour
{

    //lower to make armor/magic res better, visa versa
    //armor/magic res lower than this value has no effect
    private const int LOG_BASE = 14;

    //All mults are based off base of 1
    //ie: mult of 0.65 = 165%

    //Str subest
    public float HealthBase { get; set; }
    public float HealthMult { get; set; }
    public float HealthCur { get; set; }
    public float HealthMax { get; set; }
    public float HealthRegen { get; set; }
    public float HealthRegenMult { get; set; }
    public float MeleeAttackMult { get; set; }

    //Dex subset
    public float MoveSpeed { get; set; }
    public float MoveSpeedMult { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackSpeedMult { get; set; }
    public float RangedAttackMult { get; set; }

    //Myst subset
    public float CdrMult { get; set; }
    public float SpellMult { get; set; }

    //Fort subset
    public float MagicRes { get; set; }
    public float MagicResMult { get; set; }
    public float StatusRec { get; set; }
    public float StatusRecMult { get; set; }
    public float AfflictRes { get; set; }
    public float AfflictResMult { get; set; }

    //misc subset
    public float Armor { get; set; }
    public float ArmorMult { get; set; }
    public float DamageMult { get; set; }
    public float CritDamage { get; set; }
    public float CritDamageMult { get; set; }
    public float CritChance { get; set; }
    public float CritChanceMult { get; set; }

    private float CalcMult(float baseV, float multV)
    {
        if (multV > 0)
        {
            return baseV * (1 + multV);
        }
        else
        {
            return baseV * (100 / (100 - (multV * 100)));
        }
    }

    private float CalcLog(float n)
    {
        if(n >= LOG_BASE)
        {
            return Mathf.Log(n, LOG_BASE);
        } else if(n > -LOG_BASE)
        {
            return 1;
        } else
        {
            return 1 / Mathf.Log(Mathf.Abs(n), LOG_BASE);
        }
    }

    void Update()
    {
        HealthCur += CalcMult(HealthRegen,HealthRegenMult) * Time.deltaTime;

        HealthMax = CalcMult(HealthBase, HealthMult);
        if (HealthCur > HealthMax)
            HealthCur = HealthMax;


    }

    public float TakeDamage(Damage dmg)
    {
        float total = 0;

        float armorL = CalcMult(Armor, ArmorMult);
        float mrL = CalcMult(MagicRes, MagicResMult);

        if (mrL > LOG_BASE)
            total += dmg.magicDmgReal / Mathf.Log(mrL, LOG_BASE);
        else
            total += dmg.magicDmgReal;
        if (armorL > LOG_BASE)
            total += dmg.physicalDmgReal / Mathf.Log(armorL, LOG_BASE);
        else
            total += dmg.physicalDmgReal;

        HealthCur -= total;

        Debug.Log("Took " + total + " damage.");

        return total;
    }

    public Damage RealDamage(Damage dmg)
    {
        float physMult = 0;
        float magicMult = 0;
        if (dmg.rangedAttack)
        {
            physMult += RangedAttackMult;
            magicMult += RangedAttackMult;
        }
        if (dmg.meleeAttack)
        {
            physMult += MeleeAttackMult;
            magicMult += MeleeAttackMult;
        }
        if (dmg.spell)
        {
            physMult += SpellMult;
            magicMult += SpellMult;
        }
        physMult += DamageMult;
        magicMult += DamageMult;

        dmg.physicalDmgReal = CalcMult(dmg.physicalDmg, physMult);
        dmg.magicDmgReal = CalcMult(dmg.magicDmg, magicMult);

        if(Random.Range(0f, 1f) < CalcMult(CritChance, CritChanceMult)) {
            float critDM = CalcMult(CritDamage, CritDamageMult);
            dmg.physicalDmgReal *= critDM;
            dmg.magicDmgReal *= critDM;
        }

        return dmg;
    }
}
