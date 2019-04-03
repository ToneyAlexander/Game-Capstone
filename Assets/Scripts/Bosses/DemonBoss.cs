using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBoss : BaseBoss
{
    private readonly float AbilityZeroCd = 4f;
    private readonly float AbilityOneCd = 4f;

    private TrackingBehave playerTracker;
    private Animator animator;
    public GameObject MeleeAttackPrefab;
    public GameObject FireballPrefab;
    public GameObject MiniBossPrefab;
    private float timeSinceUse;
    private float timeSinceMelee;
    private float cooldown;
    private int nextAttack;
    private DemonMiniBoss miniBoss;
    private bool isDead;

    private int miniPhase;
    new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        playerTracker = GetComponent<TrackingBehave>();

        timeSinceUse = 0f;
        timeSinceMelee = 0f;
        cooldown = AbilityZeroCd;
        nextAttack = 0;
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

        PlaceMiniBoss();
    }

    private void PlaceMiniBoss()
    {

        Vector3 target = new Vector3(Random.Range(arenaStart.x + 6, arenaEnd.x - 6), arenaStart.y + 1, Random.Range(arenaStart.z + 6, arenaEnd.z - 6));
        GameObject obj = Instantiate(MiniBossPrefab, target, new Quaternion());

        miniBoss = obj.GetComponent<DemonMiniBoss>();
        miniPhase = 0;
    }

    protected override IEnumerator Die()
    {
        playerTracker.pause = true;
        isDead = true;
        miniBoss.Kill();
        animator.SetTrigger("death");
        yield return new WaitForSeconds(4.5f);
        Destroy(gameObject);
        SpawnTeleportOut();
    }

    void MeleeAttackPlayer()
    {
        inUse = true;
        playerTracker.pause = true;
        StartCoroutine(AsyncMelee());
    }

    IEnumerator AsyncMelee ()
    {
        animator.SetTrigger("swordattack");
        yield return new WaitForSeconds(0.65f);
        GameObject obj = Instantiate(MeleeAttackPrefab, gameObject.transform.position + new Vector3(0, 0.5f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponentInChildren<ProjectileBehave>();
        obj.transform.rotation = transform.rotation;
        obj.transform.Translate(Vector3.forward * 4.3f);
        Damage dmg = new Damage(Random.Range(65, 85) * Level, Random.Range(25, 35) * Level, false, true, false);
        pbh.dmg = stats.RealDamage(dmg);
        yield return new WaitForSeconds(1.2f);
        if(!isDead)
            playerTracker.pause = false;
        inUse = false;
    }

    void AbilityZero()
    {
        cooldown = AbilityZeroCd;
        animator.SetTrigger("whipattack");
        StartCoroutine(AbilZeroAsync());
    }

    IEnumerator AbilZeroAsync()
    {
        playerTracker.pause = true;
        yield return new WaitForSeconds(0.4f);
        int made = 0;
        while (made < 10)
        {
            GameObject obj = Instantiate(MeleeAttackPrefab, gameObject.transform.position + new Vector3(0, 0.5f, 0), new Quaternion());
            ProjectileBehave pbh = obj.GetComponentInChildren<ProjectileBehave>();
            obj.transform.rotation = transform.rotation;
            obj.transform.Translate(Vector3.forward * (3 + made * 3.5f));
            Damage dmg = new Damage(0f, Random.Range(82.5f * Level, 97.5f * Level), true, true, false);
            pbh.dmg = stats.RealDamage(dmg);
            ++made;
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1f);

        if (!isDead)
            playerTracker.pause = false;
        inUse = false;
        float choice = Random.Range(0f, 1f);
        if (choice < 0.4)
        {
            nextAttack = 0;
        }
        else
        {
            nextAttack = 1;
        }
    }

    void AbilityOne()
    {
        cooldown = AbilityOneCd;
        animator.SetTrigger("roar");
        StartCoroutine(AbilOneAsync());
    }

    IEnumerator AbilOneAsync()
    {
        yield return new WaitForSeconds(0.36f);


        int made = 0;
        while (made < 75)
        {
            GameObject obj = Instantiate(FireballPrefab, gameObject.transform.position + new Vector3(0, 7.5f, 0), Quaternion.Euler(0, 0, 0));
            obj.transform.rotation = transform.rotation;
            obj.transform.Rotate(new Vector3(Random.Range(20,60), Random.Range(-45,45), 0));
            ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
            float size = Random.Range(0.95f, 1.35f);
            obj.transform.localScale = new Vector3(size, size, size);
            obj.transform.Translate(Vector3.forward * 3);
            pbh.speed = 18f;
            Damage dmg = new Damage(0f, Random.Range(55f * size * Level, 90f * size * Level), true, false, true);
            pbh.dmg = stats.RealDamage(dmg);
            pbh.ttl = 0.9f + Random.Range(-0.05f, 0.1f);
            ++made;
            yield return new WaitForSeconds(0.016f);
        }


        yield return new WaitForSeconds(0.4f);

        inUse = false;
        float choice = Random.Range(0f, 1f);
        if (choice < 0.4)
        {
            nextAttack = 1;
        }
        else
        {
            nextAttack = 0;
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (active)
        {
            if(miniPhase == 0 && stats.HealthCur/stats.HealthMax < 0.5)
            {
                miniPhase = 1;
                miniBoss.BecomeWoke(stats, Level, player);
            } else if (miniPhase == 1 && stats.HealthCur / stats.HealthMax < 0.25)
            {
                miniPhase = 2;
                miniBoss.PhaseTwo();
            }

            if (!inUse)
            {
                timeSinceUse += Time.deltaTime;
                timeSinceMelee += Time.deltaTime;
                if (Vector3.Distance(transform.position, player.transform.position) > 5)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * StatBlock.CalcMult(stats.MoveSpeed, stats.MoveSpeedMult));

                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                    {
                        //Debug.Log("set walk");
                        animator.SetTrigger("walk");
                    }
                }
                else
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        //Debug.Log("set walk");
                        animator.SetTrigger("idle");
                    }
                    if (timeSinceMelee > 0.5f)
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
