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
        GameObject obj = Instantiate(projectile, gameObject.transform.position + new Vector3(0, 1.5f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
        var lookPos = mpd.CalculateWorldPosition() - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        obj.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
        obj.transform.Translate(Vector3.forward * 0.8f);
        obj.transform.localScale = new Vector3(2.3f, 1f, 2.3f);
        Damage dmg = new Damage(Random.Range(dmgMin, dmgMax), Random.Range(dmgMin, dmgMax), true, false, false);
        pbh.dmg = stats.RealDamage(dmg);
        pbh.dmg.callback = this;
    }

    public override void Callback(Damage dmg)
    {
        stats.HealthCur += dmg.magicDmgReal / 2f + stats.HealthCur / 2f;
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
