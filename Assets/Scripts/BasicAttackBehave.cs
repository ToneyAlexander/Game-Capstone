using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
public class BasicAttackBehave : MonoBehaviour
{
    private GameObject target;
    private StatBlock stats;
    private float tta;
    private float ttaBase;

    private float weaponDmgMin;
    private float weaponDmgMax;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player_Paddle");
        stats = GetComponent<StatBlock>();
        tta = ttaBase = 0.75f;
        weaponDmgMin = 35f;
        weaponDmgMax = 45f;
        stats.RangedAttackMult = 0.23f;
        stats.DamageMult = 0.08f;
        stats.CritChance = 0.1f;
        stats.CritChanceMult = 1.5f;
        stats.CritDamage = 2.3f;
    }

    // Update is called once per frame
    void Update()
    {
        tta -= Time.deltaTime;

        while(tta < 0)
        {
            tta += ttaBase;
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        o.transform.position = transform.position;
        o.transform.LookAt(target.transform);
        o.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        Damage dmg = new Damage(Random.Range(weaponDmgMin, weaponDmgMax), 0f, true, false, false);
        dmg = stats.RealDamage(dmg);
        ProjectileBehave pb = o.AddComponent<ProjectileBehave>();
        pb.dmg = dmg;
        pb.speed = 5f;
        pb.ttl = 5f;
        o.GetComponent<SphereCollider>().isTrigger = true;
        Rigidbody rb = o.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}
