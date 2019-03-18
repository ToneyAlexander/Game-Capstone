using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttackBehave : MonoBehaviour
{
    public float ttl;
    public Damage dmg;

    private bool friendly;

    void Start()
    {
        friendly = false;
    }

    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerStay(Collider col)
    {
        StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
        ControlStatBlock enemyControl = col.gameObject.GetComponent<ControlStatBlock>();

        if (enemy != null)
        {
            //Debug.Log("Enemy has stat block, enem is friendly: " + enemy.Friendly);
            if (friendly != enemy.Friendly) {
                enemy.TakeDamage(dmg, col.gameObject);
                if(enemyControl != null)
                {
                    enemyControl.OnHit(dmg);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
