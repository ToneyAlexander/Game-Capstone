using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;
using CCC.Abilities;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class FireballIgnite : AbilityBase
{
    private readonly string AbilName = "Fireball Ignite";

    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;
    private GameObject projectile;
    
    private float projSpeed;
    private float dmgMin;
    private float dmgMax;
    private float igniteMult;
    private float igniteDur;
    private bool igniteStack;

    public static TimedBuffPrototype ignite;

    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        projSpeed = abilStats.Find(item => item.Name == Stat.AS_PROJ_SPEED).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
        igniteMult = abilStats.Find(item => item.Name == Stat.AS_IGNITE_MULT).Value;
        igniteDur = abilStats.Find(item => item.Name == Stat.AS_DUR).Value;
        igniteStack = abilStats.Find(item => item.Name == Stat.AS_IGNITE_STACK).Value > 1f;
    }

    protected override void Activate()
    {
        GameObject obj = Instantiate(projectile, gameObject.transform.position + new Vector3(0, 2f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
        //obj.transform.LookAt(mpd.CalculateWorldPosition());
        var lookPos = mpd.CalculateWorldPosition() - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        obj.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        pbh.speed = projSpeed;
        Damage dmg = new Damage(0f, Random.Range(dmgMin, dmgMax), false, false, true);
        pbh.dmg = stats.RealDamage(dmg);
        TimedBuff tb = ignite.Instance;
        Stat stat = tb.Stats.Find(item => item.Name == Stat.HEALTH_REGEN);
        stat = new Stat(stat.Name, StatBlock.CalcMult(stat.Value, igniteMult));
        tb.Stats.Remove(new Stat(Stat.HEALTH_REGEN));
        tb.Stats.Add(stat);
        tb.Duration += igniteDur;
        tb.IsUnique = !igniteStack;
        pbh.dmg.buffs.Add(tb);
        pbh.friendly = true;
        pbh.ttl = 2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Detect ability stat changes
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
