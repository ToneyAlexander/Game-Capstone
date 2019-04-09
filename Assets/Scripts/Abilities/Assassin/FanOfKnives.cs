using CCC.Inputs;
using CCC.Stats;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class FanOfKnives : AbilityBase
{

    //medium, short ranged frontal cone attack; all enemies in range and within cone are hit by these knives

    private readonly string AbilName = "Fan Of Knives";

    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;
    private GameObject projectile;

    private float projCount;
    private float size;
    private float dmgMin;
    private float dmgMax;

    private float direction;


    protected override void Activate()
    {
        StartCoroutine(FireProjectile());
    }

	IEnumerator FireProjectile()
    {
		int currRot = (int)size;
		int currCast = 0;
		while (currCast < projCount)
		{
			//needs to file projectile along cone shape
			GameObject obj = Instantiate(projectile, gameObject.transform.position + new Vector3(0, 2f, 0), new Quaternion());
			ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
			obj.transform.rotation = this.transform.rotation;
			obj.transform.Rotate(new Vector3(0, currRot, 0));
			currRot += (int)((2*size) / projCount);
			obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			pbh.speed = 10f;
			Damage dmg = new Damage(0f, Random.Range(dmgMin, dmgMax), true, false, false);
			pbh.dmg = stats.RealDamage(dmg);
			pbh.ttl = 2f;
			pbh.friendly = true;
			++currCast;
		}
		yield return null;
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

	public override void UpdateStats()
	{
		abilStats = abil.Stats;
		Debug.Log("updating stats");
		cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
		projCount = abilStats.Find(item => item.Name == Stat.AS_PROJ_COUNT).Value;
		size = abilStats.Find(item=> item.Name == Stat.AS_SIZE).Value;
		dmgMin = abilStats.Find(item => item.Name == Stat.AS_DMG_MIN).Value;
		dmgMax = abilStats.Find(item => item.Name == Stat.AS_DMG_MAX).Value;
	}
}
