using CCC.Inputs;
using CCC.Stats;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class CorruptedEarth : AbilityBase
{

    private readonly string AbilName = "Corrupted Earth";

    private List<Stat> abilStats;
    private GameObject aoeEffect;
    private MousePositionDetector mpd;

    private float corruptionMult;
    private float duration;
    private float size;
    private bool buffs;
    private bool debuffs;

    //public static TimedBuffPrototype Corruption;

    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        duration = abilStats.Find(item => item.Name == Stat.AS_DUR).Value;
        size = abilStats.Find(item => item.Name == Stat.AS_SIZE).Value;
        buffs = abilStats.Find(item => item.Name == Stat.AS_BUFFS).Value > 1f;
        debuffs = abilStats.Find(item => item.Name == Stat.AS_DEBUFFS).Value > 1f;
    }

    protected override void Activate()
    {
        GameObject obj = Instantiate(aoeEffect, mpd.CalculateWorldPosition(), new Quaternion());
        obj.transform.localScale = new Vector3(size, 1f, size);
        AoeBehave ab = obj.GetComponent<AoeBehave>();
        ab.friendly = true;
        ab.ttl = duration;
        ab.Friend = new List<TimedBuff>();
        ab.Enemy = new List<TimedBuff>();
        //TimedBuff corrupt = Corruption.Instance;
        //Stat stat = corrupt.Stats.Find(item => item.Name == Stat.HEALTH_REGEN);
        //stat.Value = stats.RealDotDamage(stat.Value, corruptionMult, false, true, false, false, true);
    }

    // creates an AOE around the player, causing physical damage to enemies within (long term AOE)

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatBlock>();
        mpd = GetComponent<MousePositionDetector>();
        PlayerClass pc = GetComponent<PlayerClass>();

        abil = pc.abilities.Set[AbilName];
        abilStats = abil.Stats;
        aoeEffect = abil.Prefab;
        abil.cdRemain = 0f;
        UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
