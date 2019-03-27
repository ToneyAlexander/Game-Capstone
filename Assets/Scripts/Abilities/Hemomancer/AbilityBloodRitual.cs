using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

public class AbilityBloodRitual : AbilityBase
{
    private readonly string AbilName = "Blood Ritual";

    private List<Stat> abilStats;
    private ControlStatBlock controlStats;

    public static TimedBuffPrototype Mend;

    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
    }

    protected override void Activate()
    {
        TimedBuff tb = Mend.Instance;
        Stat stat = tb.Stats.Find(item => item.Name == Stat.HEALTH_REGEN);
        stat.Value = StatBlock.CalcMult(stats.HealthMax * 1.01f, stats.PhantomHpMult) / tb.Duration;
        controlStats.ApplyBuff(tb);
        stats.HealthCur = 1f;
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
