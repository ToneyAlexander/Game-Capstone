using CCC.Inputs;
using CCC.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class RainOfDeath : AbilityBase
{

    private readonly string AbilName = "Rain Of Death";

    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;
    private GameObject projectile;

    private float projCount;
    private float dmgMin;
    private float dmgMax;


    protected override void Activate()
    {
        StartCoroutine(FireProjectile());
    }

    // creates an AOE at the mouse position, sending in a brief rain of daggers

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

    public override void UpdateStats()
    {
        abilStats = abil.Stats;
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
        dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
        projCount = abilStats.Find(item => item.Name == Stat.AS_PROJ_COUNT).Value;
    }

    IEnumerator FireProjectile()
    {
        Vector3 posMod = Vector3.zero;
        int rotMod = 0;
        int tempCount = 1;
        int currCount = 0;
        while(currCount < projCount){
            GameObject obj = Instantiate(projectile, mpd.CalculateWorldPosition() + new Vector3(0,6,0), new Quaternion()); //at mouse position, 6m up.
            ProjectileBehave phb = obj.GetComponent<ProjectileBehave>();
            obj.transform.Rotate(90, rotMod, 0); //rotate around pivot
            obj.transform.Translate(posMod); //adjust forward
            obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            currCount++;
            rotMod = rotMod + (360 / tempCount);
            if(currCount == 1){
                posMod = new Vector3(2,0,0);
                if(projCount != 7){
                    tempCount =  (int)((projCount - 1) / 3);
                }else{
                    tempCount = 6;
                }
            }else if(currCount == ((projCount - 1) / 3) + 1 && projCount != 7){
                posMod = new Vector3(4,0,0);
                tempCount =   (int)(projCount - 1) - tempCount;
                rotMod = 0;
            }
            phb.friendly = true;
            phb.speed = 10;
            Damage dmg = new Damage(0f, Random.Range(dmgMin, dmgMax), true, false, false);
			phb.dmg = stats.RealDamage(dmg);
			phb.ttl = 2f;
        }
        yield return null;
    }
}
