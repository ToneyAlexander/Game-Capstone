using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
public class EnemyAttackController : MonoBehaviour
{
    public GameObject projectile;

    public bool IsAttacking { get; set; }
    public enum AttackMode { ProjectileAttack, AoeAttack };

    private GameObject target;
    private StatBlock statBlock;

    private float dmgMin;
    private float dmgMax;

    void Start()
    {
        IsAttacking = false;

        // Target is object tagged "Player" by default
        target = GameObject.FindWithTag("Player");
        
        // Get enemy's statBlock and animator
        statBlock = GetComponent<StatBlock>();

        // Min and max damage
        dmgMin = 35f;
        dmgMax = 45f;
    }

    void ProjectileAttack()
    {
        // Generates projectiles
        GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
        ProjectileBehave pbh = projectileInstance.GetComponent<ProjectileBehave>();
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        projectileInstance.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);

        Damage dmg = new Damage(Random.Range(dmgMin, dmgMax), 0f, true, false, false);
        pbh.dmg = statBlock.RealDamage(dmg);
        pbh.ttl = 3f;
    }
}
