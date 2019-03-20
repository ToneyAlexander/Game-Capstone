using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;
using CCC.Abilities;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class EdgySlash : AbilityBase
{
    private readonly string AbilName = "Slash";

    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;
    private GameObject projectile;

    private float projSpeed;
    private float dmgMin;
    private float dmgMax;
    private float slowdownMult;
    private float slowdownDur;
    private bool slowdownStack;

    public static TimedBuffPrototype slowdown;

    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        projSpeed = abilStats.Find(item => item.Name == Stat.AS_PROJ_SPEED).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
        slowdownMult = abilStats.Find(item => item.Name == Stat.AS_IGNITE_MULT).Value;
        slowdownDur = abilStats.Find(item => item.Name == Stat.AS_DUR).Value;
        slowdownStack = abilStats.Find(item => item.Name == Stat.AS_IGNITE_STACK).Value > 1f;
    }

    protected override void Activate()
    {
        GameObject obj = Instantiate(projectile, gameObject.transform.position + new Vector3(0f, 2f,0f), new Quaternion());
        HorizontalSwipe pbh = obj.GetComponent<HorizontalSwipe>();
        obj.transform.LookAt(mpd.CalculateWorldPosition());
        var lookPos = mpd.CalculateWorldPosition() - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        obj.transform.localScale = new Vector3(-5f, -5f, -5f);
        pbh.speed = projSpeed;
        Damage dmg = new Damage(0f, Random.Range(dmgMin, dmgMax), false, false, true);
        pbh.dmg = stats.RealDamage(dmg);
        TimedBuff tb = slowdown.Instance;
        Stat stat = tb.Stats.Find(item => item.Name == Stat.MOVE_SPEED_MULT);
        //Debug.Log("just like the white winged dove : " + stat.Name+ stat.Value+ slowdownMult);
        stat = new Stat(stat.Name, StatBlock.CalcMult(stat.Value, slowdownMult));
        tb.Stats.Remove(new Stat(Stat.MOVE_SPEED_MULT));
        tb.Stats.Add(stat);
        tb.Duration += slowdownDur;
        tb.IsUnique = !slowdownStack;
        pbh.dmg.buffs.Add(tb);
        pbh.friendly = true;
        pbh.ttl = 60f;
        pbh.transform.RotateAround(transform.position, Vector3.up, 150);
        pbh.player = gameObject;
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
