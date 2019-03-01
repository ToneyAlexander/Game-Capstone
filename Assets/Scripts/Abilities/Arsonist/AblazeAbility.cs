using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class AblazeAbility : AbilityBase
{
    private readonly string AbilName = "Dash";

    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;
    private GameObject aoeObj;

    private float duration;
    private float size;

    public static TimedBuffPrototype Friendly;
    public static TimedBuffPrototype Enemy;

    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        duration = abilStats.Find(item => item.Name == Stat.AS_DUR).Value;
        size = abilStats.Find(item => item.Name == Stat.AS_SIZE).Value;
    }

    protected override void Activate()
    {
        GameObject obj = Instantiate(aoeObj, mpd.CalculateWorldPosition(), new Quaternion());
        obj.transform.localScale = new Vector3(size, 0.5f, size);
        AoeBehave ab = obj.GetComponent<AoeBehave>();
        ab.friendly = true;
        ab.ttl = duration;
        Damage dmgFri = new Damage(0f,0f,false,false,false);
        dmgFri.buffs.Add(Friendly.Instance);
        ab.friendDmg = dmgFri;
        Damage dmgEnm = new Damage(0f, 0f, false, false, false);
        dmgEnm.buffs.Add(Enemy.Instance);
        ab.dmg = dmgEnm;
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatBlock>();
        mpd = GetComponent<MousePositionDetector>();
        PlayerClass pc = GetComponent<PlayerClass>();

        abil = pc.abilities.Set[AbilName];
        abilStats = abil.Stats;
        aoeObj = abil.Prefab;
        abil.cdRemain = 0f;
        UpdateStats();
    }
    
}
