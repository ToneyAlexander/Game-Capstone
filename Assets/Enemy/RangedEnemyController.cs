using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : MonoBehaviour
{
    // Animator stuff
    Animator animator;

    // NavMesh stuff
    private NavMeshAgent agent;
    private NavMeshPath path;
    
    private bool inCoroutine;

    // Each enemy moves within a circle centered at spawnPos with a radius of movingRange.
    private Vector3 spawnPos;
    private float movingRange;

    // Field of view
    private float visionAngle;
    private float visionDistance;
    private float attackDistance;

    /* Note: attackDistance <= visionDistance <= movingRange */
    
    private GameObject player;

    private Vector3 targetPos;
    private bool targetFound;

    void Start()
    {
        // Set up animator
        animator = GetComponent<Animator>();
        animator.SetBool("meleeAttack", false);
        animator.SetBool("rangedAttack", false);

        // Set up NavMesh
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5f;
        path = new NavMeshPath();

        inCoroutine = false;

        player = GameObject.Find("Player");
        targetPos = Vector3.zero;
        targetFound = false;

        // Default spawnPos and movingRange
        spawnPos = transform.position;
        movingRange = 20f;

        // Default vision
        visionAngle = 90f;
        visionDistance = 15;
        attackDistance = 10f;
    }

    void Update()
    {
        // Get player's current position
        Vector3 playerPos = player.transform.position;

        agent.isStopped = false;
        animator.SetBool("rangedAttack", false);
        GetComponent<BasicAttackController>().SetAttack(false);

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

        // Display field of view and moving area only in Scene (not in Game)
        DisplayVisionAndRange();
    }

    // Gets a random position within enemy's moving area
    private Vector3 GetRandomPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * movingRange;
        return spawnPos + new Vector3(randomPoint.x, 0.0f, randomPoint.y);
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

    private void Attack(Vector3 playerPos)
    {
        // Look at target (player character)
        transform.rotation = Quaternion.LookRotation((playerPos - transform.position).normalized);

        // Stop and attack target (player character)
        agent.isStopped = true;

        // Change to attack animation
        animator.SetBool("rangedAttack", true);

        // TODO: Maybe other stuff...
        GetComponent<BasicAttackController>().SetAttack(true);
    }

    // Checks if pos is within enemy's moving area
    private bool InRange(Vector3 pos)
    {
        float distanceToCenter = Vector3.Distance(pos, spawnPos);
        return (distanceToCenter <= movingRange);
    }

    // Checks if pos is within enemy's vision field
    private bool InVision(Vector3 pos)
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

    /* Debugging code */

    private void DisplayVisionAndRange()
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

}
