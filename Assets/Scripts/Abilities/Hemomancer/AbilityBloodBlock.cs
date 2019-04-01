using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

public class AbilityBloodBlock : AbilityBase
{
    private readonly string AbilName = "Ego Sanguine";

    private List<Stat> abilStats;
    private ControlStatBlock controlStats;

    public static TimedBuffPrototype Block;

    private float mult;

    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        mult = abilStats.Find(item => item.Name == Stat.AS_BUFFS).Value;
    }

    protected override void Activate()
    {
        TimedBuff tb = Block.Instance;
        Stat stat = tb.Stats.Find(item => item.Name == Stat.FLAT_DMG_REDUCTION);
        float hplost = StatBlock.CalcMult(stats.HealthCur, stats.PhantomHpMult) * 0.5f;
        stats.HealthCur -= hplost;
        stat.Value = hplost/1000f * mult;
        Debug.Log("Cost player " + hplost + " hp; Dmg Reduction: " + stat.Value + ".");
        controlStats.ApplyBuff(tb);
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatBlock>();
        controlStats = GetComponent<ControlStatBlock>();
        PlayerClass pc = GetComponent<PlayerClass>();

        abil = pc.abilities.Set[AbilName];
        abilStats = abil.Stats;
        abil.cdRemain = 0f;
        UpdateStats();
    }
}
