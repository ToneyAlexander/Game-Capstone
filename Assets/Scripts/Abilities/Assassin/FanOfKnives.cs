using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

public class FanOfKnives : AbilityBase
{

    //medium, short ranged frontal cone attack; all enemies in range and within cone are hit by these knives

    private readonly string AbilName = "Fan of Knives";

    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;
    private GameObject projectile;

    private float projCount;
    private float size;
    private float dmgMin;
    private float dmgMax;

    private float direction;

    public override void UpdateStats()
    {
        throw new System.NotImplementedException();
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
        //needs to file projectile along cone shape
        GameObject obj = Instantiate(projectile, gameObject.transform.position + new Vector3(0, 2f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
        Vector3 lookPos = mpd.CalculateWorldPosition() - transform.position; //
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        obj.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        Damage dmg = new Damage(0f, Random.Range(dmgMin, dmgMax), false, false, true);
        pbh.dmg = stats.RealDamage(dmg);
        pbh.friendly = true;
        pbh.ttl = 1f;
    }



    // Start is called before the first frame update
    void Start()
    {
        abilStats = abil.Stats;
        Debug.Log("updating stats");
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        projCount = abilStats.Find(item => item.Name == Stat.AS_PROJ_COUNT).Value;
        size = abilStats.Find(item => item.Name == Stat.AS_SIZE).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
