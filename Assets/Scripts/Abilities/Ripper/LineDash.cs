﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;
using CCC.Abilities;


public class LineDash : AbilityBase
{
    private List<Stat> abilStats;
    private StatBlock stats;
    private MousePositionDetector mpd;

    private float cdBase;
    private float speed;
    private float fastness = 0;
    private float duration;
    private float ttl = 0;
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
    void LateUpdate()
    {
        if (ttl > 0)
        {
           // Debug.Log("big if true");
            ttl -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, fastness * Time.deltaTime);

        }
        if (Input.GetMouseButtonDown(0) || ttl <= 0)
        {
            fastness = 0;
        }
    }
}
