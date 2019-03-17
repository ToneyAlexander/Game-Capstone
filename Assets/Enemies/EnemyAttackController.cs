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

    private void Start()
    {
        // Target is object tagged "Player" by default
        target = GameObject.FindWithTag("Player");
        
        // Get enemy(player)'s statBlock
        statBlock = GetComponent<StatBlock>();

        if (projectile == null)
        {
            projectile = new GameObject("empty projectile");
            projectile.transform.position = Vector3.zero;
            projectile.AddComponent<Rigidbody>();
            projectile.AddComponent<SphereCollider>();
            // TODO: add projectile script
        }

        // Min and max damage
        dmgMin = 35f;
        dmgMax = 45f;
    }

    public void SingleAttack()
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
