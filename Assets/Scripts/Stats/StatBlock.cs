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
    
    void Start()
    {
        //real values will be set externally
        //all values inately init to 0.0f
    }

    void Update()
    {
        HealthCur += HealthRegen * (1 + HealthRegenMult) * Time.deltaTime;

        HealthMax = HealthBase * (1 + HealthMult);
        if (HealthCur > HealthMax)
            HealthCur = HealthMax;


    }

    public float TakeDamage(Damage dmg)
    {
        float total = 0;

        float armorL = Armor * (1 + ArmorMult);
        float mrL = MagicRes * (1 + MagicResMult);

        if (mrL > LOG_BASE)
            total += dmg.magicDmgReal * Mathf.Log(LOG_BASE, mrL);
        else
            total += dmg.magicDmgReal;
        if (armorL > LOG_BASE)
            total += dmg.physicalDmgReal * Mathf.Log(LOG_BASE, armorL);
        else
            total += dmg.physicalDmgReal;

        HealthCur -= total;

        Debug.Log("Took " + total + " damage.");

        return total;
    }

    public Damage RealDamage(Damage dmg)
    {
        float physMult = 1;
        float magicMult = 1;
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

        dmg.physicalDmgReal = dmg.physicalDmg * physMult;
        dmg.magicDmgReal = dmg.magicDmg * magicMult;

        if(Random.Range(0f, 1f) < CritChance * (1 + CritChanceMult)) {
            float critDM = CritDamage * (1 + CritDamageMult);
            dmg.physicalDmgReal *= critDM;
            dmg.magicDmgReal *= critDM;
        }

        return dmg;
    }
}
