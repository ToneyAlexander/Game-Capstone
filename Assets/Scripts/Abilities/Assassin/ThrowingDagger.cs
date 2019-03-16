using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class ThrowingDagger : AbilityBase
{
    // throw daggers at your foe

    private readonly string AbilName = "Throwing Dagger";

    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;
    private GameObject projectile;

    private float projCount;
    private float projSpeed;
    private float dmgMin;
    private float dmgMax;

    public override void UpdateStats()
    {
        abilStats = abil.Stats;
        Debug.Log("updating stats");
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        projCount = abilStats.Find(item => item.Name == Stat.AS_PROJ_COUNT).Value;
        projSpeed = abilStats.Find(item => item.Name == Stat.AS_PROJ_SPEED).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
    }

    protected override void Activate()
    {
        //StartCoroutine(FireAsync());
    }

    IEnumerator FireAsync()
    {
        Debug.Log("Min: " + dmgMin + "; Max: " + dmgMax);
        int projCast = 0;
        while (projCast < projCount)
        {
            ++projCast;
            FireProjectile();
            yield return new WaitForSeconds(cdBase / (projCount * 1.75f));
        }
    }

    void FireProjectile()
    {
        GameObject obj = Instantiate(projectile, gameObject.transform.position + new Vector3(0, 2f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
        var lookPos = mpd.CalculateWorldPosition() - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        obj.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        pbh.speed = projSpeed;
        Damage dmg = new Damage(0f, Random.Range(dmgMin, dmgMax), false, false, true);
        pbh.dmg = stats.RealDamage(dmg);
        pbh.friendly = true;
        pbh.ttl = 1f;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
