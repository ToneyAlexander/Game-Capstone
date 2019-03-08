using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Behaviors;

public class StatBlock : MonoBehaviour
{

    //lower to make armor/magic res better, visa versa
    //armor/magic res lower than this value has no effect
    private const int LOG_BASE = 19;

    //All mults are based off base of 1
    //ie: mult of 0.65 = 165%

    //Str subest
    public float HealthBase { get; set; }
    public float HealthMult { get; set; }
    public float HealthCur { get; set; }
    public float HealthMax { get; set; }
    public float HealthRegen { get; set; }
    public float HealthRegenMult { get; set; }
    public float MeleeAttack { get; set; }
    public float MeleeAttackMult { get; set; }

    //Dex subset
    public float MoveSpeed { get; set; }
    public float MoveSpeedMult { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackSpeedMult { get; set; }
    public float RangedAttack { get; set; }
    public float RangedAttackMult { get; set; }

    //Myst subset
    public float CdrMult { get; set; }
    public float Spell { get; set; }
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
    public float Damage { get; set; }
    public float DamageMult { get; set; }
    public float PhysicalDamage { get; set; }
    public float PhysicalDamageMult { get; set; }
    public float MagicDamage { get; set; }
    public float MagicDamageMult { get; set; }
    public float CritDamage { get; set; }
    public float CritDamageMult { get; set; }
    public float CritChance { get; set; }
    public float CritChanceMult { get; set; }

    public bool Friendly;
    private IKillable killable;

    [SerializeField]
    private CommandProcessor commandProcessor;

    private bool isDead = false;

    public static float CalcMult(float baseV, float multV)
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

    [System.Obsolete("CalcLog is deprecated, please use ApplyReduction instead.")]
    public static float CalcLog(float n)
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

    public float ApplyReduction(float ToReduce, float Reducer)
    {
        if(ToReduce <= 0)
        {
            return 0f;
        }
        if(Reducer < -1.5f*ToReduce)
        {
            Reducer = -1.5f * ToReduce;
        }
        Debug.Log(ToReduce + " " + Reducer + " " + (2 * Mathf.Pow(ToReduce, 2)) / (Reducer + 2 * ToReduce));
        return (2*Mathf.Pow(ToReduce,2))/(Reducer + 2*ToReduce);
    }

    void Awake()
    {
        killable = GetComponent<IKillable>();
    }

    void Start()
    {
    }

    void Update()
    {
        HealthCur += CalcMult(HealthRegen,HealthRegenMult) * Time.deltaTime;

        HealthMax = CalcMult(HealthBase, HealthMult);
        if (HealthCur > HealthMax)
            HealthCur = HealthMax;

        if(HealthCur <= 0.00001f)
        {
            if (killable != null && !isDead)
            {
                ICommand com = new DieCommand(killable);
                //TODO: Should be sent to command processor when die is ready
                commandProcessor.ProcessCommand(com);
                isDead = true;
            }

        }

    }

    public float TakeDamage(Damage dmg)
    {
        float total = 0;

        float armorL = CalcMult(Armor, ArmorMult);
        float mrL = CalcMult(MagicRes, MagicResMult);

        total += ApplyReduction(dmg.magicDmgReal, mrL);
        total += ApplyReduction(dmg.physicalDmgReal, armorL);

        HealthCur -= total;

        Debug.Log(name + " took " + total + " damage.");

        return total;
    }

    public float RealDotDamage(float baseVal, float baseMult, bool phys, bool magic, bool ranged, bool melee, bool spell)
    {
        if(magic)
            baseMult += MagicDamageMult;
        if (phys)
            baseMult += PhysicalDamageMult;
        if (spell)
            baseMult += SpellMult;
        if (ranged)
            baseMult += RangedAttackMult;
        if (melee)
            baseMult += MeleeAttackMult;
        baseMult += DamageMult;

        return CalcMult(baseVal, baseMult);
    }

    public Damage RealDamage(Damage dmg)
    {
        float phys = dmg.physicalDmg;
        float physMult = 0;
        float magic = dmg.magicDmg;
        float magicMult = 0;
        if (dmg.rangedAttack)
        {
            phys += RangedAttack;
            magic += RangedAttack;
            physMult += RangedAttackMult;
            magicMult += RangedAttackMult;
        }
        if (dmg.meleeAttack)
        {
            phys += MeleeAttack;
            magic += MeleeAttack;
            physMult += MeleeAttackMult;
            magicMult += MeleeAttackMult;
        }
        if (dmg.spell)
        {
            phys += Spell;
            magic += Spell;
            physMult += SpellMult;
            magicMult += SpellMult;
        }
        phys += Damage;
        phys += PhysicalDamage;
        magic += Damage;
        magic += MagicDamage;
        physMult += DamageMult;
        physMult += PhysicalDamageMult;
        magicMult += DamageMult;
        magicMult += MagicDamageMult;

        dmg.physicalDmgReal = CalcMult(phys, physMult);
        dmg.magicDmgReal = CalcMult(magic, magicMult);

        if(Random.Range(0f, 1f) < CalcMult(CritChance, CritChanceMult)) {
            float critDM = CalcMult(CritDamage, CritDamageMult);
            dmg.physicalDmgReal *= critDM;
            dmg.magicDmgReal *= critDM;
        }

        return dmg;
    }
}
