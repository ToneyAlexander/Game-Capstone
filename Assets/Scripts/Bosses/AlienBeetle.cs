using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBeetle : BaseBoss
{
    private readonly float AbilityZeroCd = 2f;
    private readonly float AbilityOneCd = 1f;
    private readonly float AbilityTwoCd = 1f;

    private TrackingBehave playerTracker;
    private Animator animator;
    public GameObject EggPrefab;
    public GameObject VolleyPrefab;
    public GameObject TrackerPrefab;
    private float timeSinceUse;
    private float cooldown;
    private int nextAttack;

    public static TimedBuffPrototype Ooze;

    new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        playerTracker = GetComponent<TrackingBehave>();

        timeSinceUse = 0f;
        cooldown = AbilityZeroCd;
        nextAttack = 0;
        inUse = false;
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        playerTracker.Target = GameObject.FindGameObjectWithTag("Player");
        expValue = 280 * Level;
        collideDmg = new Damage(15f * Level, 0f, false, true, false);
    }

    void AbilityZero()
    {
        cooldown = AbilityZeroCd;
        animator.SetTrigger("attack1");
        StartCoroutine(AbilZeroAsync());
    }

    IEnumerator AbilZeroAsync()
    {
        yield return new WaitForSeconds(0.8f);
        int projCast = 0;
        float rangeX = (arenaEnd.x - arenaStart.x) * 0.3f, rangeZ = (arenaEnd.z - arenaStart.z) * 0.3f;
        Vector3 target = new Vector3(Random.Range(arenaStart.x+rangeX, arenaEnd.x-rangeX), arenaStart.y, Random.Range(arenaStart.z+rangeZ, arenaEnd.z-rangeZ));
        while (projCast < rangeX*rangeZ/2)
        {
            ++projCast;
            Vector3 pos = new Vector3(target.x + Random.Range(-rangeX, rangeX), target.y, target.z + Random.Range(-rangeZ, rangeZ));
            GameObject o = Instantiate(EggPrefab, pos, new Quaternion());
            EggBehave eb = o.GetComponent<EggBehave>();
            eb.ttl = 4.5f;
            eb.maxScale = 3f + Level/4f;
            Damage dmg = stats.RealDamage(new Damage(2f * Level, 2f * Level, false, true, true));
            dmg.buffs.Add(Ooze.Instance);
            eb.dmg = dmg;
            yield return new WaitForSeconds(2f/(rangeX * rangeZ / 2));
        }
        yield return new WaitForSeconds(1f);
        inUse = false;
        float choice = Random.Range(0f, 1f);
        if(choice < 0.5)
        {
            nextAttack = 1;
        } else if(choice < 0.95)
        {
            nextAttack = 2;
        } else
        {
            nextAttack = 0;
        }
    }

    void AbilityOne()
    {
        cooldown = AbilityOneCd;
        animator.SetTrigger("attack2");
        animator.SetBool("attack2bool", true);
        StartCoroutine(AbilOneAsync());
    }

    IEnumerator AbilOneAsync()
    {
        yield return new WaitForSeconds(0.7f);
        int projCast = 0;
        while (projCast < 15)
        {
            ++projCast;
            GameObject obj = Instantiate(VolleyPrefab, gameObject.transform.position + new Vector3(0, 1.5f, 0), new Quaternion());
            ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            obj.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);

            obj.transform.Rotate(Vector3.up * 90 * Random.Range(-0.03f, 0.03f), Space.World);
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            pbh.speed = 35f;
            Damage dmg = new Damage(0f, Random.Range(17.5f * Level, 22.5f * Level), true, false, false);
            pbh.dmg = stats.RealDamage(dmg);
            pbh.ttl = 3f;
            
            yield return new WaitForSeconds(0.15f);
        }
        animator.SetBool("attack2bool", false);
        inUse = false;
        float choice = Random.Range(0f, 1f);
        if (choice < 0.6)
        {
            nextAttack = 2;
        }
        else if (choice < 0.85)
        {
            nextAttack = 0;
        }
        else
        {
            nextAttack = 1;
        }
    }

    void AbilityTwo()
    {
        cooldown = AbilityTwoCd;
        animator.SetTrigger("attack2");
        animator.SetBool("attack2bool", true);
        StartCoroutine(AbilTwoAsync());
    }

    IEnumerator AbilTwoAsync()
    {
        yield return new WaitForSeconds(0.7f);
        GameObject obj = Instantiate(TrackerPrefab, gameObject.transform.position + new Vector3(0, 2f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
        obj.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        pbh.speed = 9f + Level / 5;
        Damage dmg = new Damage(0f, Random.Range(82.5f * Level, 97.5f * Level), true, false, true);
        pbh.dmg = stats.RealDamage(dmg);
        pbh.ttl = 5f /*+ Level/2f*/;//had to remove scaling due to fixed duration of projectile particle system.
        TrackingBehave tbh = obj.GetComponent<TrackingBehave>();
        tbh.RotSpeed = 2.5f + Level / 5f;
        tbh.Target = player;


        inUse = false;
        animator.SetBool("attack2bool", false);
        float choice = Random.Range(0f, 1f);
        if (choice < 0.8)
        {
            nextAttack = 0;
        }
        else
        {
            nextAttack = 1;
        }
    }

    protected override IEnumerator Die()
    {
        playerTracker.pause = true;
        animator.SetTrigger("death2");
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
        SpawnTeleportOut();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (active)
        {
            if (!inUse)
            {
                timeSinceUse += Time.deltaTime;
                if(!animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                {
                    Debug.Log("set walk");
                    animator.SetTrigger("walk");
                }
                transform.Translate(Vector3.forward * Time.deltaTime * StatBlock.CalcMult(stats.MoveSpeed, stats.MoveSpeedMult));
            }

            if (timeSinceUse > cooldown)
            {
                animator.SetTrigger("idle");
                timeSinceUse = 0;
                inUse = true;
                switch (nextAttack)
                {
                    case 0:
                        AbilityZero();
                        break;
                    case 1:
                        AbilityOne();
                        break;
                    case 2:
                        AbilityTwo();
                        break;
                }
            }
        }
    }
}
