using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Abilities;
using CCC.Stats;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class FireballVolley : MonoBehaviour, IAbilityBase
{
    private List<Stat> abilStats;
    private StatBlock stats;
    private Ability abil;
    private MousePositionDetector mpd;
    private GameObject projectile;

    private float cdRemain;

    private float cdBase;
    private float projCount;
    private float projSpread;
    private float projSpeed;
    private float dmgMin;
    private float dmgMax;

    public float CooldownLeft()
    {
        return cdRemain;
    }

    public void UpdateStats()
    {
        abilStats = abil.Stats;
        Debug.Log("updating stats");
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        projCount = abilStats.Find(item => item.Name == Stat.AS_PROJ_COUNT).Value;
        projSpeed = abilStats.Find(item => item.Name == Stat.AS_PROJ_SPEED).Value;
        projSpread = abilStats.Find(item => item.Name == Stat.AS_PROJ_SPREAD).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
    }

    public bool Use()
    {
        if(cdRemain <= 0.0001f)
        {
            cdRemain = cdBase;
            StartCoroutine(FireAsync());
            return true;
        }
        //else
        return false;
    }

    IEnumerator FireAsync()
    {
        Debug.Log("Min: " + dmgMin + "; Max: " + dmgMax);
        int projCast = 0;
        while (projCast < projCount)
        {
            ++projCast;
            FireProjectile();
            yield return new WaitForSeconds(cdBase/(projCount * 1.75f));
        }
    }

    void FireProjectile()
    {
        GameObject obj = Instantiate(projectile, gameObject.transform.position + new Vector3(0, 2f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
        //obj.transform.LookAt(mpd.CalculateWorldPosition());
        var lookPos = mpd.CalculateWorldPosition() - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        obj.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        obj.transform.Rotate(Vector3.up * 90 * Random.Range(-projSpread, projSpread), Space.World);
        pbh.speed = projSpeed;
        Damage dmg = new Damage(0f, Random.Range(dmgMin, dmgMax),false,false,true);
        //Debug.Log("Magic Dmg: " + dmg.magicDmgReal);
        pbh.dmg = stats.RealDamage(dmg);
        //Debug.Log("Real Magic Dmg: " + pbh.dmg.magicDmgReal);
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
        abil = pc.abilDict.GetAbility(AbilitySlot.One);//TODO find by name not hardcoded slot.
        projectile = abil.Prefab;
        abilStats = abil.Stats;
        cdRemain = 0;
        UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(cdRemain > 0f)
        {
            cdRemain -= Time.deltaTime;
        }
        if(abil.use)
        {
            abil.use = false;
            Use();
        }
        if (abil.update)
        {
            abil.update = false;
            UpdateStats();
        }
    }
}
