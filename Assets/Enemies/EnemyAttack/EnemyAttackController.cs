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

    public void ProjectileAttack(GameObject projectile)
    {
        // Generate a projectile instance and get its behave script
        GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
        ProjectileBehave projectileBehave = projectileInstance.GetComponent<ProjectileBehave>();

        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        projectileInstance.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);

        // Cause damage
        Damage dmg = new Damage(Random.Range(dmgMin, dmgMax), 0f, true, false, false);
        projectileBehave.dmg = statBlock.RealDamage(dmg);
    }

    public void MeleeAttack()
    {
        
    }

    public void AoeAttack(float range)
    {
        GameObject aoeAttack = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        aoeAttack.transform.localScale = new Vector3(range, 4.5f, range);
        CapsuleCollider collider = aoeAttack.AddComponent<CapsuleCollider>();
        collider.isTrigger = true;
        collider.radius = 0.5f;

        // Generate an AOE instance and get its behave script
        GameObject aoeInstance = Instantiate(aoeAttack, transform.position, transform.rotation);
        AoeAttackBehave aoeBehave = aoeInstance.GetComponent<AoeAttackBehave>();

        aoeBehave.ttl = 1f;

        // Cause damage
        Damage dmg = new Damage(Random.Range(dmgMin, dmgMax), 0f, true, false, false);
        aoeBehave.dmg = statBlock.RealDamage(dmg);
    }
}
