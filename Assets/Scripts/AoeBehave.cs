using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeBehave : MonoBehaviour, IAttackIgnored
{
    public Damage dmg;
    public float ttl;
    public bool friendly = false;
    public List<TimedBuff> Friend;
    public List<TimedBuff> Enemy;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            //Debug.Log("Destoryed due to ttl");
            Destroy(gameObject);
        }
    }


    protected virtual void OnTriggerStay(Collider col)
    {
        StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
        ControlStatBlock enemyControl = col.gameObject.GetComponent<ControlStatBlock>();

        if (enemy != null)
        {
            if (friendly != enemy.Friendly)
            {

                if (enemyControl != null && Enemy != null)
                {
                    foreach (TimedBuff tb in Enemy)
                    {
                        enemyControl.ApplyBuff(tb.ShallowClone());
                    }
                }
            } else
            {
                if (enemyControl != null && Friend != null)
                {
                    foreach (TimedBuff tb in Friend)
                    {
                        enemyControl.ApplyBuff(tb.ShallowClone());
                    }
                }
            }
        }
    }
}
