using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{   

    // NavMesh stuff
    protected NavMeshAgent agent;
    protected NavMeshPath path;
    
    protected bool inCoroutine;

    // Each enemy moves within a circle centered at spawnPos with a radius of movingRange.
    protected Vector3 spawnPos;
    protected float movingRange;
    protected bool movable;

    // Field of view
    protected float visionAngle;
    protected float visionDistance;
    protected float attackDistance;

    // Attack controller
    protected BasicAttackController attackController;

    // Enemy health
    protected float healthPoints;

    /* Note: attackDistance <= visionDistance <= movingRange */
    
    protected GameObject player;

    protected Vector3 targetPos;
    protected bool targetFound;

    void Start()
    {
        // Set up NavMesh
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5f;
        path = new NavMeshPath();

        // Set up attack controller
        attackController = GetComponent<BasicAttackController>();

        inCoroutine = false;

        player = GameObject.Find("remy");
        targetPos = Vector3.zero;
        targetFound = false;

        Initialize();
    }

    void Update()
    {
        UniqueUpdate();

        // Get player's current position
        Vector3 playerPos = player.transform.position + new Vector3(0.0f, 2.0f, 0.0f);

        if (movable)
        {
            // If the player character is within the enemy's moving area and is within 
            // enemy's field of view, the enemy chases player character
            if (InVision(playerPos) && InRange(playerPos)) 
            {
                targetFound = true;
                targetPos = playerPos;
                agent.speed = 20f;

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

        // Under attack and death animations
        UnderAttack();
        if (healthPoints <= 0.0f)
        {
            StartCoroutine(Die());
        }

        // Display field of view and moving area only in Scene (not in Game)
        // DisplayVisionAndRange();
    }

    // Gets a random position within enemy's moving area
    private Vector3 GetRandomPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * movingRange;
        return spawnPos + new Vector3(randomPoint.x, 1.0f, randomPoint.y);
    }

    private IEnumerator Move()
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
        while (!InAttackRange(targetPos))
        {
            agent.SetDestination(targetPos);
            yield return null;        
        }

        inCoroutine = false;
    }

    // Checks if pos is within enemy's moving area
    private bool InRange(Vector3 pos)
    {
        float distanceToCenter = Vector3.Distance(pos, spawnPos);
        return (distanceToCenter <= movingRange);
    }

    // Checks if pos is within enemy's vision field
    protected bool InVision(Vector3 pos)
    {
        Vector3 movingDirection = (targetPos - transform.position).normalized;
        Vector3 directToPos = (pos - transform.position).normalized;
        float distanceToPos = Vector3.Distance(transform.position, pos);

        return (Vector3.Angle(movingDirection, directToPos) <= visionAngle / 2)
            && (distanceToPos <= visionDistance);
    }

    private bool InAttackRange(Vector3 pos)
    {
        return Vector3.Distance(transform.position, pos) <= attackDistance;
    }

    /* Debugging code (Don't delete them) */

    // private void DisplayVisionAndRange()
    // {
    //     // View
    //     int stepCount = Mathf.RoundToInt(visionAngle * 5f);
    //     float stepAngleSize = visionAngle / stepCount;

    //     for (int i = 0; i < stepCount; i++)
    //     {
    //         float angle = transform.eulerAngles.y - visionAngle / 2 + stepAngleSize * i;
    //         Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * visionDistance, Color.red);
    //     }

    //     // Moving Range
    //     int stepCount2 = Mathf.RoundToInt(360f * 5f);
    //     float stepAngleSize2 = 360f / stepCount2;

    //     for (int i = 0; i < stepCount2; i++)
    //     {
    //         float angle2 = 360f / 2 + stepAngleSize2 * i;
    //         Debug.DrawLine(spawnPos, spawnPos + DirFromAngle(angle2, true) * movingRange, Color.yellow);
    //     }
    // }

    // private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    // {
    //     if (!angleIsGlobal)
    //     {
    //         angleInDegrees += transform.eulerAngles.y;
    //     }
    //     return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    // }

    /* Abstract methods */

    protected abstract void Initialize();

    protected abstract void UniqueUpdate();

    protected abstract void Attack(Vector3 pos);

    protected abstract void UnderAttack();

    protected abstract IEnumerator Die();
}
