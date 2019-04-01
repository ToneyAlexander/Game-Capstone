using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBoss : BaseBoss
{
    private readonly float AbilityZeroCd = 2f;
    private readonly float AbilityOneCd = 2f;
    private readonly float AbilityTwoCd = 2f;
    private readonly float AbilityBallCd = 6.5f;

    private TrackingBehave playerTracker;
    private Animator animator;
    public GameObject LargeFlamePrefab;
    public GameObject WarningPrefab;
    public GameObject TrackerPrefab;
    public GameObject BulletHellPrefab;
    private float timeSinceUse;
    private float cooldown;
    private int nextAttack;
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
                        //AbilityZero();
                        break;
                    case 1:
                        //AbilityOne();
                        break;
                    case 2:
                        //AbilityTwo();
                        break;
                    default:
                        //AbilityZero();
                        break;
                }
            }
        }
    }
}
