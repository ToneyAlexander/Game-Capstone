using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;
using CCC.Abilities;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class FireballIgnite : MonoBehaviour, IAbilityBase
{
    private List<Stat> abilStats;
    private StatBlock stats;
    private Ability abil;
    private MousePositionDetector mpd;
    private GameObject projectile;

    private float cdRemain;

    private float cdBase;
    private float projSpeed;
    private float dmgMin;
    private float dmgMax;
    private float igniteMult;

    public static TimedBuffPrototype ignite;

    public float CooldownLeft()
    {
        return cdRemain;
    }

    public bool Use()
    {
        if (cdRemain <= 0.0001f)
        {
            cdRemain = cdBase;
            FireProjectile();
            return true;
        }
        //else
        return false;
    }

    void FireProjectile()
    {
        GameObject obj = Instantiate(projectile, gameObject.transform.position, new Quaternion());
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
        abil = pc.abilDict.GetAbility(AbilitySlot.Two);//TODO find by name not hardcoded slot.
        projectile = abil.Prefab;
        abilStats = abil.Stats;
        cdRemain = 0;
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        projSpeed = abilStats.Find(item => item.Name == Stat.AS_PROJ_SPEED).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
        igniteMult = abilStats.Find(item => item.Name == Stat.AS_IGNITE_MULT).Value;
    }

    // Update is called once per frame
    void Update()
    {
        if (cdRemain > 0f)
        {
            cdRemain -= Time.deltaTime;
        }
        if (abil.use)
        {
            abil.use = false;
            Use();
        }
    }
}
