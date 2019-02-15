using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : EnemyController
{
    protected override void Initialize()
    {
        // Default spawnPos and movingRange
        spawnPos = transform.position;
        movingRange = 20f;

        // Default vision
        visionAngle = 90f;
        visionDistance = 15;
        attackDistance = 10f;
    }

    protected override void UniqueUpdate()
    {
        GetComponent<BasicAttackController>().SetAttack(false);
    }

    protected override void Attack(Vector3 playerPos)
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
}
