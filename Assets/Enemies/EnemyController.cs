﻿using CCC.Movement;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public abstract class EnemyController : MonoBehaviour
{   
    // NavMesh stuff
    protected NavMeshAgent agent;
    protected NavMeshPath path;

    // Each enemy moves within a circle centered at spawnPos with a radius of movingRange.
    protected Vector3 spawnPos;
    protected float movingRange;
    protected float chaseSpeed;
    protected bool movable;

    // Field of view
    protected float visionAngle;
    protected float visionDistance;
    protected float attackDistance;
    protected bool attack;

    // Attack controller
    protected EnemyAttackController attackController;
    
    protected GameObject player;
    protected Vector3 targetPos;
    protected bool targetFound;

    /// <summary>
    /// The CommandProcessor that this EnemyController sends ICommands to.
    /// </summary>
    [SerializeField]
    protected CommandProcessor commandProcessor;

    /// <summary>
    /// The IDestinationMover Component that 
    /// </summary>
    private IDestinationMover destinationMover;

    [SerializeField]
    private PerkPrototype initialStats;
    [SerializeField]
    private PerkPrototype onLevelUpPerk;

    [SerializeField]
    protected GameObject healthBar;

    private PlayerClass enemyClass;
    protected float expValue;

    private bool inCoroutine;
    private bool isAlive;

    // maxTimes is the max times that a navMeshAgent looks for a new path, so that it won't lead to infinite loop
    private int maxTimes;

    protected void Awake()
    {
        destinationMover = GetComponent<IDestinationMover>();

        if (destinationMover == null)
        {
            Debug.LogError("[" + gameObject.name + " 's EnemyController] " +
                "has no IDestinationMover Component attached!");
        }
    }

    protected void Start()
    {
        gameObject.tag = "Enemy";

        enemyClass = GetComponent<PlayerClass>();

        // Set up NavMesh
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5f;
        path = new NavMeshPath();

        // Add health bar
        GameObject hpBar = Instantiate(healthBar, transform);
        Vector3 barScale = hpBar.GetComponent<RectTransform>().localScale;
        hpBar.GetComponent<RectTransform>().localScale = new Vector3(
            (1.0f / transform.localScale.x) * barScale.x,
            (1.0f / transform.localScale.y) * barScale.y,
            (1.0f / transform.localScale.z) * barScale.z
        );

        // Set up attack controller
        attackController = GetComponent<EnemyAttackController>();
        attack = true;

        inCoroutine = false;
        isAlive = true;

        player = GameObject.FindWithTag("Player");
        targetPos = Vector3.zero;
        targetFound = false;

        // Get enemy perk
        int level = GameObject.Find("Generator").GetComponent<GenerateIsland>().islandStorage.level;
        level = ((level) > 1 ? (level) : 1);
        expValue = Random.Range(35, 45) * (1 + (0.248f - ((0.002f * level) < 0.124f ? (0.002f * level) : 0.124f)) * (level < 62 ? level : 62));
        enemyClass.TakePerk(initialStats, false);
        enemyClass.onLevelUp = onLevelUpPerk;
        for (int i = 0; i < level; ++i)
        {
            enemyClass.LevelUp();
        }

        maxTimes = 100;
    }

    protected void Update()
    {
        if (isAlive)
        {
            UniqueUpdate();

            // Get player's current position
            Vector3 playerPos = player.transform.position;

            if (movable)
            {
                // If the player character is within the enemy's moving area and is within 
                // enemy's field of view, the enemy chases player character
                if (attack && InRange(playerPos) && InVision(playerPos)) 
                {
                    targetFound = true;
                    targetPos = playerPos;
                    agent.speed = chaseSpeed;

                    // If the player character is within the enemy's attacking range, the 
                    // enemy stops near the target and attacks it
                    if (InAttackRange(playerPos))
                    {
                        Attack(playerPos);
                    }
                }
                // Otherwise, enemy moves randomly without a target
                else
                {
                    targetFound = false;
                    agent.speed = 5f;
                }

                if (!inCoroutine)
                {
                    StartCoroutine(Move());
                }
            }
        }

        // Display field of view and moving area only in Scene (not in Game)
        // DisplayVisionAndRange();
    }

    // Gets a random position within enemy's moving area
    private Vector3 GetRandomPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * movingRange;
        Vector3 randomPos = spawnPos + new Vector3(randomPoint.x, 0.0f, randomPoint.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, 5f, 1)) 
        {
            randomPos = hit.position;            
        }

        return randomPos;
    }

    protected IEnumerator Move()
    {
        inCoroutine = true;
        // Find a random destination if the player character is not the target
        if (!targetFound)
        {
            int i = 0;
            // Make sure the path is valid
            do
            {
                targetPos = GetRandomPosition();
                i += 1;
            }
            while (!agent.CalculatePath(targetPos, path) && i < maxTimes);

            if (i >= maxTimes)
            {
                gameObject.SetActive(false);
                Debug.Log(gameObject.name + " is deactivated.");
                inCoroutine = false;
                yield return null;
            }
        }
        // Enemy moves until it reaches closely enough to targetPos 
        // (until the player target is within enemy's attacking distance)
        while (!InAttackRange(targetPos) && !agent.isStopped)
        {
            Vector3 destination = targetPos;
            ICommand command = new MoveToCommand(destinationMover, transform.position, destination);
            commandProcessor.ProcessCommand(command);
            yield return null;        
        }

        inCoroutine = false;
    }

    // Checks if pos is within enemy's moving area
    protected bool InRange(Vector3 pos)
    {
        float distanceToCenter = Vector3.Distance(pos, spawnPos);
        return (distanceToCenter <= movingRange);
    }

    // Checks if pos is within enemy's vision field
    protected virtual bool InVision(Vector3 pos)
    {
        Vector3 movingDirection = (targetPos - transform.position).normalized;
        Vector3 directToPos = (pos - transform.position).normalized;
        float distanceToPos = Vector3.Distance(transform.position, pos);

        return (Vector3.Angle(movingDirection, directToPos) <= visionAngle / 2)
            && (distanceToPos <= visionDistance);
    }

    protected bool InAttackRange(Vector3 pos)
    {
        return Vector3.Distance(transform.position, pos) <= attackDistance;
    }

    public void isKilled()
    {
        isAlive = false;
        Collider col = GetComponent<Collider>();
        if (col != null)
            Destroy(col);
        if (player != null)
            player.GetComponent<PlayerClass>().ApplyExp(expValue);
        StartCoroutine(Die());
    }

    /* Debugging code */

    protected void DisplayVisionAndRange()
    {
        // View
        int stepCount = Mathf.RoundToInt(visionAngle * 5f);
        float stepAngleSize = visionAngle / stepCount;

        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - visionAngle / 2 + stepAngleSize * i;
            Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * visionDistance, Color.red);
        }

        // Moving Range
        int stepCount2 = Mathf.RoundToInt(360f * 5f);
        float stepAngleSize2 = 360f / stepCount2;

        for (int i = 0; i < stepCount2; i++)
        {
            float angle2 = 360f / 2 + stepAngleSize2 * i;
            Debug.DrawLine(spawnPos, spawnPos + DirFromAngle(angle2, true) * movingRange, Color.yellow);
        }
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    /* Abstract methods */

    protected abstract void UniqueUpdate();

    protected abstract void Attack(Vector3 pos);

    protected abstract void UnderAttack();

    protected abstract IEnumerator Die();
}
