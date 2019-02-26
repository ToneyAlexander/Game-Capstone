using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;


[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(ControlStatBlock))]
[RequireComponent(typeof(PlayerClass))]
public class DashAbility : AbilityBase
{
    private readonly string AbilName = "Dash";

    private List<Stat> abilStats;
    private StatBlock stats;
    private ControlStatBlock controlStats;

    private float duration;
    private float dashMult;

    public static TimedBuffPrototype Dash;

    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        duration = abilStats.Find(item => item.Name == Stat.AS_DUR).Value;
        dashMult = abilStats.Find(item => item.Name == Stat.AS_DASH_MULT).Value;
    }


    protected override void Activate()
    {
        TimedBuff tb = Dash.Instance;
        tb.Duration += duration;
        Stat stat = tb.Stats.Find(item => item.Name == Stat.MOVE_SPEED_MULT);
        stat = new Stat(stat.Name, StatBlock.CalcMult(stat.Value, dashMult));
        tb.Stats.Remove(new Stat(Stat.MOVE_SPEED_MULT));
        tb.Stats.Add(stat);
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
