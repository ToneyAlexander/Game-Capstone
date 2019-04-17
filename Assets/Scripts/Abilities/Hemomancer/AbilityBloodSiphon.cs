using CCC.Inputs;
using CCC.Stats;
using UnityEngine;
using System.Collections.Generic;

public class AbilityBloodSiphon : AbilityBase
{
    private readonly string AbilName = "Soul Siphon";

    private List<Stat> abilStats;
    private MousePositionDetector mpd;
    private GameObject projectile;

    private float dmgRatio;
    private float cost;


    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        dmgRatio = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        cost = abilStats.Find(item => item.Name == Stat.AS_COST).Value;
    }

    public override bool Use()
    {
        if (abil.cdRemain <= 0.0001f && stats.HealthCur > StatBlock.CalcMult(stats.HealthMax, stats.PhantomHpMult) * cost)
        {
            abil.cdRemain = cdBase;
            Activate();
            return true;
        }
        //else
        return false;
    }


    protected override void Activate()
    {
        GameObject obj = Instantiate(projectile, gameObject.transform.position + new Vector3(0, 1.5f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponentInChildren<ProjectileBehave>();
        obj.transform.localScale = new Vector3(10.3f, 1f, 10.3f);
        Damage dmg;
        float hplost = StatBlock.CalcMult(stats.HealthMax, stats.PhantomHpMult) * cost;
        stats.HealthCur -= hplost;
        Debug.Log("Cost player " + hplost + " hp.");
        dmg = new Damage(0f, hplost * dmgRatio, false, false, true);
        pbh.dmg = stats.RealDamage(dmg);
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
