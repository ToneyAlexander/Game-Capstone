using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttackBehave : AoeBehave
{
    protected override void OnTriggerStay(Collider col)
    {
        StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
        ControlStatBlock enemyControl = col.gameObject.GetComponent<ControlStatBlock>();

        if (enemy != null)
        {
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
