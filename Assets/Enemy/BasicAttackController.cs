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

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("remy");
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
        if (meleeAttack)
        {
            MeleeAttack();
        }

        if (rangedAttack)
        {
            tta -= Time.deltaTime;

            while(tta < 0)
            {
                tta += ttaBase;
                ProjectileAttack();
            }
        }
    }

    void MeleeAttack()
    {
        Damage dmg = new Damage(Random.Range(weaponDmgMin, weaponDmgMax), 0f, true, false, false);
        dmg = stats.RealDamage(dmg);
        GetComponent<MeleeEnemyController>().dmg = dmg;
    }

    void ProjectileAttack()
    {
        // Old code - to be removed...
        // GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // o.transform.position = transform.position;
        // o.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        // o.transform.LookAt(target.transform);
        // o.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        // o.transform.parent = transform;
        // Damage dmg = new Damage(Random.Range(weaponDmgMin, weaponDmgMax), 0f, true, false, false);
        // dmg = stats.RealDamage(dmg);
        // ProjectileController pb = o.AddComponent<ProjectileController>();
        // pb.dmg = dmg;
        // pb.speed = 10f;
        // pb.ttl = 5f;
        // o.GetComponent<SphereCollider>().isTrigger = true;
        // Rigidbody rb = o.AddComponent<Rigidbody>();
        // rb.useGravity = false;
        // rb.isKinematic = true;

        // Generates projectiles
        var projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
        ProjectileMover pm = projectileInstance.GetComponent<ProjectileMover>();
        projectileInstance.GetComponent<SphereCollider>().isTrigger = true;
        projectileInstance.transform.parent = transform;

        Damage dmg = new Damage(Random.Range(weaponDmgMin, weaponDmgMax), 0f, true, false, false);
        dmg = stats.RealDamage(dmg);
        pm.dmg = dmg;
    }

    public void SetAttack(string attackMode, bool attack)
    {
        if (attackMode == MeleeEnemyController.AttackMode)
        {
            meleeAttack = attack;
        }
        else if (attackMode == RangedEnemyController.AttackMode)
        {
            rangedAttack = attack;
        }
    }
}
