using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMiniBoss : MonoBehaviour
{

    bool active;
    private TrackingBehave playerTracker;
    private Animator animator;
    private float timeSinceUse;
    private float cooldown;
    private float abilityZeroCd;
    private bool targetRandomly;
    private StatBlock stats;
    private int Level;
    private Quaternion targetRot;

    public GameObject BulletHellPrefab;

    void Awake()
    {
        playerTracker = GetComponent<TrackingBehave>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        abilityZeroCd = 3.3f;
        timeSinceUse = 0f;
        cooldown = abilityZeroCd;
        active = false;
        playerTracker.pause = true;
        targetRandomly = false;
        targetRot = Quaternion.Euler(0, Random.Range(0,360), 0);
    }

    public void BecomeWoke(StatBlock stats, int level, GameObject player)
    {
        Level = level;
        this.stats = stats;
        playerTracker.Target = player;
        animator.SetTrigger("awake");
        StartCoroutine(FinishWake());
    }

    IEnumerator FinishWake()
    {
        yield return new WaitForSeconds(0.3f);
        active = true;
        playerTracker.pause = false;
    }

    public void PhaseTwo()
    {
        abilityZeroCd = 1.1f;
        targetRandomly = true;
        playerTracker.pause = true;
    }

    private void FireAttack()
    {
        animator.SetTrigger("attack");
        StartCoroutine(FireAttackAsync());
    }


    IEnumerator FireAttackAsync()
    {
        yield return new WaitForSeconds(0.15f);
        GameObject obj = Instantiate(BulletHellPrefab, gameObject.transform.position + new Vector3(0, 1f, 0), new Quaternion());
        DragonBulletHell pbh = obj.GetComponent<DragonBulletHell>();
        obj.transform.localScale = new Vector3(2f, 2f, 2f);
        obj.transform.rotation = transform.rotation;
        obj.transform.Translate(Vector3.forward * 1);
        pbh.speed = 16f;
        Damage dmg = new Damage(0f, 0f, true, false, false);
        pbh.dmg = stats.RealDamage(dmg);
        pbh.ttl = 5f;
        Damage dmgChild = new Damage(0f, Random.Range(25f * Level, 50f * Level), true, false, true);
        pbh.childDamage = stats.RealDamage(dmgChild);
        yield return new WaitForSeconds(0.2f);
        if(targetRandomly)
        {
            targetRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
    }

    public void Kill()
    {
        playerTracker.pause = true;
        active = false;
        StartCoroutine(AsyncKill());
    }

    private IEnumerator AsyncKill()
    {
        animator.SetTrigger("death");
        yield return new WaitForSeconds(4.5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if(targetRandomly)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 90*Time.deltaTime);
            }

            timeSinceUse += Time.deltaTime;

            if(timeSinceUse > cooldown)
            {
                timeSinceUse = 0;
                FireAttack();
            }
        }
    }
}
