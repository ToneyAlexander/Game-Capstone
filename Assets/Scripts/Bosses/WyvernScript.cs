using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyvernScript : BaseBoss
{
    private Animator animator;
    public GameObject FireballPrefab;
    private int farTeleportCount = 3;
    private int farTeleportCircleCount = 8;
    private float farTeleportDistInner = 5.0f;
    private float farTeleportRad = 5.0f;
    private float farTeleportDistOuter = 15.0f;
    private float attackCooldown = 3.0f;
    private float teleportCooldown = 1.0f;
    private float innerCircleRadius = 1.0f;
    private float outerCircleRadius = 3.0f;
    private float innerCircleCount = 4.0f;
    private float outerCircleCount = 6.0f;
    private float innerShootCount = 6.0f;
    private float outerShootCount = 4.0f;
    private float shootStartRadius = 1.0f;
    private float shootSpaceTiming = 0.5f;
    private int shootCircles = 3;
    private float shootCooldown = 10.0f;
    private int defenseCircles = 3;
    private float beforeAttackTiming = 1.0f;
    private int closeTeleportCount = 3;
    private float closeTeleportDist = 7.0f;
    private float closeTeleportTravelDist = 10.0f;
    private float closeTeleportCooldown = 5.0f;
    private float betweenCloseTeleport = 1.0f;
    private bool innerCircle = false;
    private float cooldown = 4.0f;
    private float projectileSize = 3.0f;
    private bool closeTeleportOn = false;
    private float closeTeleportSpeed = 3.0f;
    private float closeDistanceMoved = 0.0f;

    public Vector3 lastPlayerLocation = new Vector3();
  //  private GameObject player;
  
    private enum WyvernAttack
    {
        teleport,
        closeteleport,
        burst
    }
    private WyvernAttack nextAttack;


    private TrackingBehave playerTracker;
    private bool isDead;
    void Awake()
    {
        base.Awake();
        nextAttack = WyvernAttack.teleport;
        cooldown = teleportCooldown;
        inUse = false;
        isDead = false;
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

    void Start()
    {
        base.Start();
        playerTracker.Target = GameObject.FindGameObjectWithTag("Player");
        expValue = 330 * Level;
        player = GameObject.FindGameObjectWithTag("Player");
        // collideDmg = new Damage(7.5f * Level, 7.5f * Level, false, true, false);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (active)
        {
            if (cooldown < 0)
            {
                nextAttack = (WyvernAttack)Random.Range(0, 2);
                switch (nextAttack)
                {
                    case WyvernAttack.teleport:
                        FarTeleport();
                        break;
                    case WyvernAttack.closeteleport:
                        CloseTeleport();
                        break;
                    case WyvernAttack.burst:
                        Burst();
                        break;
                }
                
                cooldown -= Time.deltaTime;
            }
            else if (closeTeleportOn)
            {
                Vector3.MoveTowards(transform.position, lastPlayerLocation, closeTeleportSpeed*Time.deltaTime);
                closeDistanceMoved += closeTeleportSpeed * Time.deltaTime;
                if (closeDistanceMoved > closeTeleportDist)
                {
                    closeTeleportMove();
                }
            }
            
            

            

        }
    }
    void FarTeleport()
    {
        cooldown = teleportCooldown;
        StartCoroutine(AbilFarTeleport());
    }
    void CloseTeleport()
    {
        cooldown = closeTeleportCooldown;
        StartCoroutine(AbilCloseTeleport());
    }
    void closeTeleportMove()
    {
        lastPlayerLocation = player.transform.position;
       // transform.position = 
        
    }
    void Burst()
    {
        cooldown = shootCooldown;
        StartCoroutine(AbilShoot());
        StartCoroutine(AbilDefenseCircle());
    }

    IEnumerator AbilFarTeleport()
    {

        int x = Random.Range(-25,25);
        int z = Random.Range(-6, 21);
        transform.position = new Vector3(x, 0, z);
        ProjectileBehave[] projectiles = new ProjectileBehave[farTeleportCircleCount];
        for (int i = 0; i < farTeleportCircleCount; i++)
        {
            GameObject obj = Instantiate(FireballPrefab, gameObject.transform.position + new Vector3(farTeleportDistInner, 0.5f, 0), new Quaternion());
            ProjectileBehave pbh = obj.GetComponentInChildren<ProjectileBehave>();
            obj.transform.RotateAround(transform.position, Vector3.up, 360 / farTeleportCircleCount * i);
            Damage dmg = new Damage(0f, Random.Range(82.5f * Level, 97.5f * Level), true, true, false);
            pbh.dmg = stats.RealDamage(dmg);
            pbh.speed = 0.0f;
            projectiles[i] = pbh;

        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].speed = 0.5f;
        }

    }
    IEnumerator AbilCloseTeleport()
    {

        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator AbilShoot()
    {
        int circleRad = (int)outerShootCount;
        if (innerCircle)
        {
            circleRad = (int)innerShootCount;
        }
        for (int i = 0; i < circleRad; i++)
         {
                GameObject obj = Instantiate(FireballPrefab, gameObject.transform.position + new Vector3(shootStartRadius, 0.5f, 0), new Quaternion());
                ProjectileBehave pbh = obj.GetComponentInChildren<ProjectileBehave>();
                obj.transform.RotateAround(transform.position, Vector3.up, 360 / circleRad * i);
                Damage dmg = new Damage(0f, Random.Range(82.5f * Level, 97.5f * Level), true, true, false);
                pbh.dmg = stats.RealDamage(dmg);
                pbh.speed = 0.5f;


        }
        yield return new WaitForSeconds(0.1f);

    }
    IEnumerator AbilDefenseCircle()
    {
        for (int i = 0; i < defenseCircles; i++)
        {
            int circleRad = (int)innerCircleRadius;
            if (i % 2 != 0)
            {
                circleRad = (int)outerCircleRadius;
            }
            for (int j = 0; j < circleRad; j++)
            {
                
            }
        }
        yield return new WaitForSeconds(0.1f);
    }
}
