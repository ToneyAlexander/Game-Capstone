using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBulletHell : MagicFairBall
{
    // Start is called before the first frame update
    public GameObject smallProj;
    public Damage childDamage;

    override public void OnDeath()
    {
        int expected = 12;
        for (int i = 0; i < expected; ++i)
        {
            GameObject obj = Instantiate(smallProj, transform.position, Quaternion.Euler(0, 360 / expected * i, 0));
            ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
            obj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            obj.transform.Translate(Vector3.forward * 0.5f);
            pbh.dmg = childDamage;
            pbh.ttl = 5f;
        }
    }
}
