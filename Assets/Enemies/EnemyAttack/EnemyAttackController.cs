using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
public class EnemyAttackController : MonoBehaviour
{
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

        // Min and max damage
        dmgMin = 35f;
        dmgMax = 45f;
    }

    public void SetDamageRange(float min, float max)
    {
        dmgMin = min;
        dmgMax = max;
    }

    public void ProjectileAttack(GameObject projectile, float ttl, float height = 0f)
    {
        // Generate a projectile instance and get its behave script
        GameObject projectileInstance = Instantiate(projectile, transform.position + new Vector3(0f, height, 0f), transform.rotation);
        ProjectileBehave projectileBehave = projectileInstance.GetComponent<ProjectileBehave>();
        projectileBehave.ttl = ttl;

        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        projectileInstance.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);

        // Cause damage
        Damage dmg = new Damage(Random.Range(dmgMin, dmgMax), 0f, false, true, false);
        projectileBehave.dmg = statBlock.RealDamage(dmg);
    }

    public void MeleeAttack()
    {
        
    }

    public void AoeAttack(float range, float ttl)
    {
        // Generate an AOE instance
        GameObject aoeInstance = new GameObject("AOE");
        aoeInstance.transform.parent = transform;
        aoeInstance.transform.position = transform.position;
        aoeInstance.transform.localScale = new Vector3(range, 1f, range);
        // Add collider
        CapsuleCollider collider = aoeInstance.AddComponent<CapsuleCollider>();
        collider.isTrigger = true;
        collider.radius = 0.5f;
        // Add behave script
        AoeBehave aoeBehave = aoeInstance.AddComponent<AoeAttackBehave>();
        aoeBehave.ttl = ttl;

        // Cause damage
        Damage dmg = new Damage(Random.Range(dmgMin, dmgMax), 0f, true, false, false);
        aoeBehave.dmg = statBlock.RealDamage(dmg);
    }
}
