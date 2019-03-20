using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeController : EnemyController
{
    public GameObject projectile;
    
    private Animator animator;

    private bool inAttackCoroutine;

    new void Start()
    {
        base.Start();

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
        attackController.ProjectileAttack(projectile, 0.5f);
		yield return new WaitForSeconds(1.0f);
        
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
        
    protected override IEnumerator Die()
    {
        animator.SetTrigger("death");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
