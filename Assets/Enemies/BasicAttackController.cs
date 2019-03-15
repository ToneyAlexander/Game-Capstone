using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
public class BasicAttackController : MonoBehaviour
{
    public GameObject projectile;

    private GameObject target;
    private StatBlock stats;
    private float tta;
    private float ttaBase;

    private float weaponDmgMin;
    private float weaponDmgMax;

    private bool meleeAttack;
    private bool rangedAttack;

    private const string melee = "meleeAttack";
    private const string ranged = "rangedAttack";


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        stats = GetComponent<StatBlock>();
        tta = ttaBase = 0.5f;
        weaponDmgMin = 35f;
        weaponDmgMax = 45f;
        stats.RangedAttackMult = 0.23f;
        stats.DamageMult = 0.08f;
        stats.CritChance = 0.1f;
        stats.CritChanceMult = 1.5f;
        stats.CritDamage = 2.3f;

        meleeAttack = false;
        rangedAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        tta -= Time.deltaTime;

        while(tta < 0)
        {
            tta += ttaBase;
            if (meleeAttack)
            {
                MeleeAttack();
            }
            if (rangedAttack)
            {
                ProjectileAttack();
            }
        }
    }

    void MeleeAttack()
    {
        Damage dmg = new Damage(Random.Range(weaponDmgMin, weaponDmgMax), 0f, true, false, false);
        dmg = stats.RealDamage(dmg);
        GetComponent<MeleeEnemyController>().dmg = dmg;
        GameObject remy = GameObject.Find("remy");
        StatBlock enemy = remy.GetComponent<StatBlock>();
        if (enemy != null)
        {
            enemy.TakeDamage(dmg, remy);            
        }
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

        Damage dmg = new Damage(Random.Range(weaponDmgMin, weaponDmgMax), 0f, true, false, false);
        pbh.dmg = stats.RealDamage(dmg);
        pbh.ttl = 3f;
    }

    public void SetAttack(string attackMode, bool attack)
    {
        if (attackMode == melee)
        {
            meleeAttack = attack;
        }
        else if (attackMode == ranged)
        {
            rangedAttack = attack;
        }
    }
}
