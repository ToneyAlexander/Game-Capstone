using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

public class DragonTracking : MagicFairBall
{

    public GameObject aoeObj;
    public TimedBuffPrototype Ignite;
    public float realIgniteDmg;

    new void Update()
    {
        base.Update();
        speed += Time.deltaTime * 2.5f;
    }

    new void OnDeath()
    {
        GameObject obj = Instantiate(aoeObj, transform.position - new Vector3(0, 1.2f, 0), new Quaternion());
        Debug.Log("spawn tracker at " + (transform.position - new Vector3(0, 1.2f, 0)));
        obj.transform.localScale = new Vector3(5.5f, 1f, 5.5f);
        AoeBehave ab = obj.GetComponent<AoeBehave>();
        ab.friendly = false;
        ab.ttl = -10;
        ab.Friend = new List<TimedBuff>();
        ab.Enemy = new List<TimedBuff>();
        TimedBuff ignite = Ignite.Instance;
        Stat stat = ignite.Stats.Find(item => item.Name == Stat.HEALTH_REGEN);
        stat.Value = realIgniteDmg;
        ab.Enemy.Add(ignite);
    }
}
