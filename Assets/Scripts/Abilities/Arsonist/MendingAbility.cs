using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

public class MendingAbility : AbilityBase
{
    private readonly string AbilName = "Empowered Mending";

    private StatBlock stats;
    private ControlStatBlock controlStats;

    public static TimedBuffPrototype Mending;

    public override void UpdateStats()
    {
        cdBase = abil.Stats.Find(item => item.Name == Stat.AS_CD).Value;
    }

    protected override void Activate()
    {
        TimedBuff tb = Mending.Instance;
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
