using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
public class EnemyAttackController : MonoBehaviour
{
    public GameObject projectile;

    // public enum AttackMode { ProjectileAttack, AoeAttack };

    private GameObject target;
    private StatBlock statBlock;

    private float dmgMin;
    private float dmgMax;

    void Start()
    {
        // Target is object tagged "Player" by default
        target = GameObject.FindWithTag("Player");
        
        // Get enemy's statBlock
        statBlock = GetComponent<StatBlock>();

        // Min and max damage
        dmgMin = 35f;
        dmgMax = 45f;
    }

    void ProjectileAttack()
    {
        // Generate a projectile instance and get its behave script
        GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
        ProjectileBehave projectileBehave = projectileInstance.GetComponent<ProjectileBehave>();

        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        projectileInstance.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);

        // Cause damage
        DealDamage(projectileBehave);
    }

    private void DealDamage(ProjectileBehave projectileBehave)
    {
        Damage dmg = new Damage(Random.Range(dmgMin, dmgMax), 0f, true, false, false);
        projectileBehave.dmg = statBlock.RealDamage(dmg);
        projectileBehave.ttl = 5f;
    }
}
