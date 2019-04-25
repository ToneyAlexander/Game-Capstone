using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulBoss : BaseBoss
{
    private readonly float AbilityZeroCd = 2f;
    private readonly float AbilityOneCd = 4f;
    private float spawnCd;

    private TrackingBehave playerTracker;
    private Animator animator;
    public GameObject MeleeAttackPrefab;
    public GameObject RangedAttackPrefab;
    public GameObject AddPrefab;
    public TimedBuffPrototype poison;


    private float timeSinceUse;
    private float timeSinceMelee;
    private float timeSinceSpawn;
    private float cooldown;
    private int nextAttack;
    private bool isDead;

    private float bonusDmg;

    new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        playerTracker = GetComponent<TrackingBehave>();

        timeSinceUse = 0f;
        timeSinceMelee = 0f;
        timeSinceSpawn = 0f;
        cooldown = AbilityZeroCd;
        nextAttack = 0;
        bonusDmg = 0;
        spawnCd = 9f;
        inUse = false;
        isDead = false;
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        playerTracker.Target = GameObject.FindGameObjectWithTag("Player");
        expValue = 330 * Level;
        collideDmg = new Damage(7.5f * Level, 7.5f * Level, false, true, false);
    }

    protected override IEnumerator Die()
    {
        playerTracker.pause = true;
        isDead = true;
        animator.SetTrigger("death");
        yield return new WaitForSeconds(4.5f);
        Destroy(gameObject);
        SpawnTeleportOut();
    }

    void AbilityZero()
    {
        cooldown = AbilityZeroCd;
        animator.SetTrigger("attack2");
        StartCoroutine(AbilZeroAsync());
    }

    IEnumerator AbilZeroAsync()
    {
        playerTracker.pause = true;
        yield return new WaitForSeconds(0.25f);
        Vector3 rotateOffset = new Vector3(0, -25f, 0);
        int made = 0;
        while (made < 3)
        {
            GameObject obj = Instantiate(MeleeAttackPrefab, gameObject.transform.position + new Vector3(0, 1.5f, 0), new Quaternion());
            ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
            obj.transform.rotation = transform.rotation;
            obj.transform.Rotate(rotateOffset);
            obj.transform.Translate(Vector3.forward * (transform.localScale.x/2f));
            Damage dmg = new Damage(bonusDmg + Random.Range(50 * Level, 60 * Level), 0f, true, true, false);
            dmg.buffs.Add(poison.Instance);
            pbh.dmg = stats.RealDamage(dmg);
            if (made == 0)
                rotateOffset = new Vector3(0, 25f, 0);
            if (made == 1)
                rotateOffset = new Vector3();
            ++made;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.2f);

        if (!isDead)
            playerTracker.pause = false;
        inUse = false;
        float choice = Random.Range(0f, 1f);
        nextAttack = 1;
    }

    void AbilityOne()
    {
        GameObject[] adds = GameObject.FindGameObjectsWithTag("Enemy");
        int index = -1;
        for(int i = adds.Length - 1; i >= 0; --i)
        {
            DummyAddKillable killable = adds[i].GetComponent<DummyAddKillable>();
            if (killable != null && !killable.IsDead)
            {
                index = i;
                break;
            } 
        }
        if (index >= 0)
        {
            cooldown = AbilityOneCd;
            animator.SetTrigger("bite");
            StartCoroutine(AbilOneAsync(adds[index]));
        } else
        {
            AbilityZero();
        }
    }

    IEnumerator AbilOneAsync(GameObject target)
    {
        target.GetComponent<StatBlock>().HealthCur = -100;
        bonusDmg += 20 * Level;
        playerTracker.pause = true;
        transform.rotation = new Quaternion();
        transform.position = target.transform.position + new Vector3(0f, 0f, -2f);
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 5; ++i)
        {
            for(int j = 0; j < 5; ++j)
            {
                GameObject obj = Instantiate(RangedAttackPrefab, target.transform.position, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
                ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
                obj.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                obj.transform.Translate(Vector3.forward * 0.5f);
                Damage dmg = new Damage(0f, bonusDmg / 3f + Random.Range(20 * Level, 30 * Level), true, false, true);
                dmg.buffs.Add(poison.Instance);
                pbh.dmg = stats.RealDamage(dmg);
                pbh.ttl = 2.5f;
                pbh.speed = 16f;
            }
            yield return new WaitForSeconds(0.2f);
        }
        transform.localScale = transform.localScale + new Vector3(0.3f, 0.3f, 0.3f);
        stats.HealthCur += 600 * Level;
        if (!isDead)
            playerTracker.pause = false;
        inUse = false;
        nextAttack = 0;
    }

    void MeleeAttackPlayer()
    {
        inUse = true;
        playerTracker.pause = true;
        animator.SetTrigger("attack1");
        StartCoroutine(AsyncMelee());
    }

    IEnumerator AsyncMelee()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject obj = Instantiate(MeleeAttackPrefab, gameObject.transform.position + new Vector3(0, 1.5f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
        obj.transform.rotation = transform.rotation;
        obj.transform.Translate(Vector3.forward * (transform.localScale.x/2f));
        Damage dmg = new Damage(bonusDmg + Random.Range(40, 45) * Level, 0, false, true, false);
        dmg.buffs.Add(poison.Instance);
        pbh.dmg = stats.RealDamage(dmg);
        yield return new WaitForSeconds(0.3f);
        if (!isDead)
            playerTracker.pause = false;
        inUse = false;
    }

    void SpawnAdd()
    {
        Vector3 target = transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        GameObject obj = Instantiate(AddPrefab, target, new Quaternion());
    }

    new void Update()
    {
        base.Update();
        if (active)
        {

            timeSinceSpawn += Time.deltaTime;

            if(!inUse && !isDead && timeSinceSpawn > spawnCd)
            {
                timeSinceSpawn = 0;
                spawnCd += 1.5f;
                cooldown += 1.5f;
                nextAttack = 0;
                SpawnAdd();
            }

            if (!inUse)
            {
                timeSinceUse += Time.deltaTime;
                timeSinceMelee += Time.deltaTime;
                if (Vector3.Distance(transform.position, player.transform.position) > transform.localScale.x + 0.8f)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * StatBlock.CalcMult(stats.MoveSpeed, stats.MoveSpeedMult));

                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
                    {
                        animator.SetTrigger("walk");
                    }
                }
                else
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        animator.SetTrigger("idle");
                    }
                    if (timeSinceMelee > 0.9f)
                    {
                        timeSinceMelee = 0;
                        MeleeAttackPlayer();
                    }
                }
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
                    default:
                        AbilityZero();
                        break;
                }
            }
        }
    }
}
