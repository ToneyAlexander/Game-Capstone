using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBoss : BaseBoss
{
    private readonly float AbilityZeroCd = 2f;
    private readonly float AbilityOneCd = 2f;
    private readonly float AbilityTwoCd = 1f;

    private TrackingBehave playerTracker;
    private Animator animator;
    public GameObject LargeFlamePrefab;
    public GameObject WarningPrefab;
    private float timeSinceUse;
    private float cooldown;
    private int nextAttack;

    private float landY, floatY;
    private bool flyUp;

    void AbilityZero()
    {
        cooldown = AbilityZeroCd;
        animator.SetTrigger("spreadFire");
        StartCoroutine(AbilZeroAsync());
    }

    IEnumerator AbilZeroAsync()
    {
        playerTracker.pause = true;
        Vector3 rotateOffset = new Vector3();
        int made = 0;
        while (made < 24)
        {
            GameObject obj = Instantiate(LargeFlamePrefab, gameObject.transform.position + new Vector3(0, 2f, 0), new Quaternion());
            ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
            obj.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            obj.transform.rotation = transform.rotation;
            obj.transform.Rotate(rotateOffset);
            obj.transform.Translate(Vector3.forward * 5);
            pbh.speed = 11f + Level / 2;
            Damage dmg = new Damage(0f, Random.Range(82.5f * Level, 97.5f * Level), true, false, false);
            pbh.dmg = stats.RealDamage(dmg);
            pbh.ttl = 5f;
            if(made > 2 && made < 8)
            {

                rotateOffset += new Vector3(0, 7f, 0);
            } else if(made > 16 && made < 22)
            {
                rotateOffset += new Vector3(0, 4f, 0);
            } else if(made > 2 && made < 22)
            {
                rotateOffset += new Vector3(0, -7f, 0);
            }
            ++made;
            yield return new WaitForSeconds(0.166f);
        }

        playerTracker.pause = false;
        inUse = false;
        float choice = Random.Range(0f, 1f);
        if (choice < 0.2)
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
        animator.SetTrigger("fly");
        StartCoroutine(AbilOneAsync());
    }

    IEnumerator AbilOneAsync()
    {
        yield return new WaitForSeconds(0.25f);
        flyUp = true;
        int expectedCount = 30;
        for (int i = 0; i < expectedCount; ++i)
        {
            StartCoroutine(RainFire(Random.Range(0f,1f) > 0.6f));
            yield return new WaitForSeconds(6f/expectedCount);
        }
        flyUp = false;
        yield return new WaitForSeconds(0.8f);
        animator.SetTrigger("land");
        yield return new WaitForSeconds(0.25f);
        inUse = false;
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

    IEnumerator RainFire(bool aim)
    {
        float range = 4.5f;
        Vector3 target;
        if(aim)
        {
            target = player.transform.position + new Vector3(Random.Range(-range, range), 0.3f, Random.Range(-range, range));
        } else
        {
            target = new Vector3(Random.Range(arenaStart.x + 2, arenaEnd.x - 2), 0.3f, Random.Range(arenaStart.z + 2, arenaEnd.z - 2));
        }
        GameObject o = Instantiate(WarningPrefab, target, Quaternion.Euler(90, 0, 0));
        yield return new WaitForSeconds(2.25f);
        GameObject obj = Instantiate(LargeFlamePrefab, target, Quaternion.Euler(90, 0, 0));
        ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
        obj.GetComponent<SphereCollider>().radius = 0.31f;
        obj.transform.localScale = new Vector3(6.5f, 6.5f, 6.5f);
        pbh.speed = 0f;
        pbh.destroyable = false;
        Damage dmg = new Damage(0f, Random.Range(35f * Level, 65f * Level), false, false, true);
        pbh.dmg = stats.RealDamage(dmg);
        pbh.ttl = 0.2f;
        Destroy(o);
    }

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
        expValue = 330 * Level;
        collideDmg = new Damage(7.5f * Level, 7.5f * Level, false, true, false);

        landY = transform.position.y + 0.01f;
        floatY = transform.position.y + 4.99f;
    }

    protected override IEnumerator Die()
    {
        playerTracker.pause = true;
        animator.SetTrigger("death");
        yield return new WaitForSeconds(4.5f);
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
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                {
                    Debug.Log("set walk");
                    animator.SetTrigger("walk");
                }
                transform.Translate(Vector3.forward * Time.deltaTime * StatBlock.CalcMult(stats.MoveSpeed, stats.MoveSpeedMult));
            }

            if(flyUp && transform.position.y < floatY)
                transform.Translate(Vector3.up * Time.deltaTime * 5f);
            if (!flyUp && transform.position.y > landY)
                transform.Translate(Vector3.down * Time.deltaTime * 5f);

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
                        //AbilityTwo();
                        break;
                    default:
                        AbilityOne();
                        break;
                }
            }
        }
    }
}
