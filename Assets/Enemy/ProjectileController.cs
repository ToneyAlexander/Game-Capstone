using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Damage dmg;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("remy")) {
            StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
            if (enemy != null)
            {
                enemy.TakeDamage(dmg);
            }
            Destroy(gameObject);
        }
    }
}
