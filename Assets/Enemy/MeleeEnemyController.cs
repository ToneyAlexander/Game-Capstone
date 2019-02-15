using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController
{
    protected override void Initialize()
    {
        // Default spawnPos and movingRange
        spawnPos = transform.position;
        movingRange = 20f;

        // Default vision
        visionAngle = 120f;
        visionDistance = 10f;
        attackDistance = 5f;
    }
    protected override void UniqueUpdate()
    {
        // Nothing here for melee enemy now...
    }

    protected override void Attack(Vector3 playerPos)
    {
        // Look at target (player character)
        transform.rotation = Quaternion.LookRotation((playerPos - transform.position).normalized);

        // Stop and attack target (player character)
        agent.isStopped = true;

        // Change to attack animation
        animator.SetBool("meleeAttack", true);

        // TODO: Maybe other stuff...
        // Nothing for melee
    }
}
