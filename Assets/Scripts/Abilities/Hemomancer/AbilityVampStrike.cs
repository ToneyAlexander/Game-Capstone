using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

public class AbilityVampStrike : AbilityBase
{
    private readonly string AbilName = "Vampire Strike";

    private List<Stat> abilStats;
    private MousePositionDetector mpd;
    private GameObject projectile;
    
    private float dmgMin;
    private float dmgMax;

    public static TimedBuffPrototype ignite;


    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
    }

    protected override void Activate()
    {

    }

    public override void Callback()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        mpd = GetComponent<MousePositionDetector>();
        stats = GetComponent<StatBlock>();
        PlayerClass pc = GetComponent<PlayerClass>();

        abil = pc.abilities.Set[AbilName];
        projectile = abil.Prefab;
        abilStats = abil.Stats;
        abil.cdRemain = 0f;
        UpdateStats();
    }
}
