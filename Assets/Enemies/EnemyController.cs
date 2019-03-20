using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private PlayerClass enemyClass;
    protected float expValue;

    private bool inCoroutine;
    private bool isAlive;

    protected void Awake()
    {
        destinationMover = GetComponent<IDestinationMover>();
        expValue = 40;//default override for various enemies

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

        // Set up attack controller
        attackController = GetComponent<EnemyAttackController>();

        inCoroutine = false;
        isAlive = true;

        player = GameObject.FindWithTag("Player");
        targetPos = Vector3.zero;
        targetFound = false;
        enemyClass.TakePerk(initialStats);
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
                if (InRange(playerPos) && InVision(playerPos)) 
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
        return spawnPos + new Vector3(randomPoint.x, 0.0f, randomPoint.y);
    }

    protected IEnumerator Move()
    {
        inCoroutine = true;
        // Find a random destination if the player character is not the target
        if (!targetFound)
        {
            // Make sure the path is valid
            do
            {
                targetPos = GetRandomPosition();
            } 
            while (!agent.CalculatePath(targetPos, path));
        }
        // Enemy moves until it reaches closely enough to targetPos 
        // (until the player target is within enemy's attacking distance)
        if (!InAttackRange(targetPos))
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

    public abstract IEnumerator Die();
}
