using CCC.Inputs;
using CCC.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class RainOfDeath : AbilityBase
{

    private readonly string AbilName = "Fan Of Knives";

    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;
    private GameObject projectile;

    private float projCount;
    private float size;
    private float dmgMin;
    private float dmgMax;


    protected override void Activate()
    {
        StartCoroutine(FireProjectile());
    }

    // creates an AOE at the mouse position, sending in a brief rain of daggers

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

    public override void UpdateStats()
    {
        abilStats = abil.Stats;
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        projCount = abilStats.Find(item => item.Name == Stat.AS_PROJ_COUNT).Value;
        size = abilStats.Find(item => item.Name == Stat.AS_SIZE).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
    }

    IEnumerator FireProjectile()
    {
        yield return null;
    }
}
