using CCC.Abilities;
using CCC.Inputs;
using CCC.Stats;
using UnityEngine;
using System.Collections.Generic;

public class AbilityVampStrike : AbilityBase
{
    private readonly string AbilName = "Vampire Strike";

    private List<Stat> abilStats;
    private MousePositionDetector mpd;
    private GameObject projectile;
    
    private float dmgMin;
    private float dmgMax;
    private float vampRate;
    private float cost;

    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
        vampRate = abilStats.Find(item => item.Name == Stat.AS_VAMP).Value;
        cost = abilStats.Find(item => item.Name == Stat.AS_COST).Value;
    }

    protected override void Activate()
    {
        GameObject obj = Instantiate(projectile, gameObject.transform.position + new Vector3(0, 1.5f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponentInChildren<ProjectileBehave>();
        var lookPos = mpd.CalculateWorldPosition() - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        obj.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
        obj.transform.Translate(Vector3.forward * 1.3f);
        obj.transform.localScale = new Vector3(3.3f, 1f, 3.3f);
        Damage dmg;
        if (cost < 0.00001)
        {
            dmg = new Damage(Random.Range(dmgMin, dmgMax), 0, false, true, false);
        } else
        {
            float hplost = StatBlock.CalcMult(stats.HealthCur, stats.PhantomHpMult) * cost;
            stats.HealthCur -= hplost;
            Debug.Log("Cost player " + hplost + " hp.");
            dmg = new Damage(Random.Range(dmgMin, dmgMax), hplost / 2f, false, true, true);
        }
        pbh.dmg = stats.RealDamage(dmg);
        pbh.dmg.callback = this;
    }

    public override void Callback(float dmgTaken)
    {
        Debug.Log("Heal: " + dmgTaken * vampRate + " from " + dmgTaken);
        stats.HealthCur += dmgTaken * vampRate;
    }

    #region MonoBehaviour Messages
    private void Awake()
    {
        mpd = GetComponent<MousePositionDetector>();
        stats = GetComponent<StatBlock>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        var pc = GetComponent<PlayerClass>();
        abil = pc.abilities.Set[AbilName];
        projectile = abil.Prefab;
        abilStats = abil.Stats;
        abil.cdRemain = 0f;
        UpdateStats();
    }
    #endregion
}
