using CCC.Inputs;
using CCC.Stats;
using UnityEngine;
using System.Collections.Generic;

public class LineDash : AbilityBase
{
    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;
    private bool fasttimes = true;
    private float cdBase;
    private float speed;
    private float fastness = 0;
    private float duration;
    private float ttl = 0;
    private float doubletapTolerance = 0.5f;
    private float taptime = 0;
    private float MajorCooldown = 8.0f;
    private Vector3 destination = new Vector3();

    private readonly string AbilName = "Line Dash";
    public override void UpdateStats()
    {
        cdBase = abilStats.Find(item => item.Name == Stat.AS_CD).Value;
        speed = abilStats.Find(item => item.Name == Stat.AS_PROJ_SPEED).Value;
        duration = abilStats.Find(item => item.Name == Stat.AS_DUR).Value;


    }
    // Start is called before the first frame update
    protected override void Activate()
    {
        fastness = speed;
        ttl = duration;
        if (fasttimes)
        {
            abil.cdRemain = 0.1f;
            taptime = doubletapTolerance;
        }
        else
        {
            abil.cdRemain = MajorCooldown;
            
        }
        fasttimes = !fasttimes;

        destination = mpd.CalculateWorldPosition();
        RemyMovement.destination = destination;
    }
    void Start()
    {
        mpd = GetComponent<MousePositionDetector>();
        stats = GetComponent<StatBlock>();
        PlayerClass pc = GetComponent<PlayerClass>();

       abil = pc.abilities.Set[AbilName];
       // projectile = abil.Prefab;
        abilStats = abil.Stats;
       // abil = pc.abilities.Set[AbilName];
        abil.cdRemain = 0f;
        UpdateStats();
    }

    // Update is called once per frame
     void Update()
    {
          if (taptime > 0 && !fasttimes)
        {
            taptime -= Time.deltaTime;
            if (taptime <= 0)
            {
                fasttimes = true;
                abil.cdRemain = MajorCooldown;
            }

        }
            if (abil.cdRemain > 0f)
            {
                float mult = 1;
                if (stats != null)
                {
                    mult += StatBlock.CalcMult(stats.Cdr, stats.CdrMult);
                    if (abil.isAttack)
                    {
                        mult += StatBlock.CalcMult(stats.AttackSpeed, stats.AttackSpeedMult);
                    }
                }
                abil.cdRemain -= Time.deltaTime * mult;
            }
            if (abil.use)
            {
                abil.use = false;
                Use();
            }
            if (abil.update)
            {
                abil.update = false;
                UpdateStats();
            }
        
        if (ttl > 0)
        {
           // Debug.Log("big if true");
            ttl -= Time.deltaTime;
            destination.y = transform.position.y;
            if (Vector3.Distance(destination, this.transform.position) < 1.0f)
            {
                destination = this.transform.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, destination, fastness * Time.deltaTime);

        }
        if (Input.GetMouseButtonDown(0) || ttl <= 0)
        {
            fastness = 0;
        }
    }
}
