using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController
{
    // Animator stuff
    private Animator animator;

    private bool inAttackCoroutine;

    protected override void Initialize()
    {
        // Set up animator
        animator = GetComponent<Animator>();

        // Default spawnPos and movingRange
        spawnPos = transform.position;
        movingRange = 20f;
        chaseSpeed = 15f;
        movable = true;

        // Default vision
        visionAngle = 120f;
        visionDistance = 10f;
        attackDistance = 5f;

        inAttackCoroutine = false;
    }
    
    protected override void UniqueUpdate()
    {
        agent.isStopped = false;
    }

    protected override void Attack(Vector3 playerPos)
    {
        // Look at target (player character)
        transform.rotation = Quaternion.LookRotation(new Vector3((
			playerPos - transform.position).x, 
			0.0f,
			(playerPos - transform.position).z));

        // Stop and attack target (player character)
        agent.isStopped = true;

        // Play attack animation and cause damage
        if (!inAttackCoroutine)
		{
			StartCoroutine(Attack());
		}
    }
    
    private IEnumerator Attack()
	{
		inAttackCoroutine = true;

        animator.SetTrigger("meleeAttack");
		yield return new WaitForSeconds(1.0f);

        // Cause damage
        attackController.IsAttacking = true;
        
		inAttackCoroutine = false;
	}

    protected override void UnderAttack()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     healthPoints--;
        //     animator.SetTrigger("hit");
        // }
    }
        
    public override IEnumerator Die()
    {
        animator.SetTrigger("death");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
