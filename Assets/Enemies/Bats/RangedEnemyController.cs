using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : EnemyController
{
    // Animator stuff
    private Animator animator;

    public const string AttackMode = "rangedAttack";

    protected override void Initialize()
    {
        // Set up animator
        animator = GetComponent<Animator>();
        animator.SetBool("meleeAttack", false);
        animator.SetBool("rangedAttack", false);

        // Default spawnPos and movingRange
        spawnPos = transform.position;
        movingRange = 20f;

        // Default vision
        visionAngle = 60f;
        visionDistance = 20;
        attackDistance = 20f;

        // Default stat
        healthPoints = 100f;
    }

    protected override void UniqueUpdate()
    {
        agent.isStopped = false;
        animator.SetBool("meleeAttack", false);
        animator.SetBool("rangedAttack", false);

        // By default, enemy is not attacking.
        attackController.SetAttack(AttackMode, false);
    }

    protected override void Attack(Vector3 playerPos)
    {
        // Look at target (player character)
        transform.rotation = Quaternion.LookRotation((playerPos - transform.position).normalized);

        // Stop and attack target (player character)
        agent.isStopped = true;

        // Change to attack animation
        animator.SetBool(AttackMode, true);

        // TODO: Maybe other stuff...
        attackController.SetAttack(AttackMode, true);
    }

    protected override void UnderAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            healthPoints--;
            animator.SetTrigger("hit");
        }
    }
    
    protected override IEnumerator Die()
    {
        animator.SetBool("death", true);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
