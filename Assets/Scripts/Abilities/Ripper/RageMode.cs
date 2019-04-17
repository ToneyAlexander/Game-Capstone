using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

public class RageMode : AbilityBase
{
    private readonly string AbilName = "Rage Mode";

    private StatBlock stats;
    private ControlStatBlock controlStats;

    public static TimedBuffPrototype RipperFrenzy;

    public override void UpdateStats()
    {
        cdBase = abil.Stats.Find(item => item.Name == Stat.AS_CD).Value;
    }

    protected override void Activate()
    {
        TimedBuff tb = RipperFrenzy.Instance;
        controlStats.ApplyBuff(tb);
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatBlock>();
        controlStats = GetComponent<ControlStatBlock>();
        PlayerClass pc = GetComponent<PlayerClass>();

        abil = pc.abilities.Set[AbilName];
        abil.cdRemain = 0f;
        UpdateStats();
    }
}