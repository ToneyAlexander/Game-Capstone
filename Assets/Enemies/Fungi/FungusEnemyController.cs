﻿using UnityEngine;
using System.Collections;

public class FungusEnemyController : EnemyController 
{
	public GameObject projectile;

	private Animator animator;

	private bool awake, inAttackCoroutine;

	new void Start()
    {
		base.Start();
		
        // Set up animator
		animator = GetComponent<Animator>();

		// Default spawnPos and movingRange
        spawnPos = transform.position;
        movingRange = 10f;
		chaseSpeed = 2.5f;
		movable = false;

		// Default vision
        visionAngle = 360f;
        visionDistance = 10f;
        attackDistance = 3f;

		awake = false;
		inAttackCoroutine = false;
	}

	protected override void UniqueUpdate()
    {
		animator.SetFloat("h", 0.0f);
		animator.SetFloat("v", 0.0f);
		agent.isStopped = true;
		movable = false;

		// The fungus moves only when it sees the player
		if (InRange(player.transform.position)) 
		{
			animator.SetBool("Mimic", false);
			// Wait for seconds so the enemy does not attack immediately
			// Wakeup animation plays only when it is in Mimic state
			if (!awake && !inAttackCoroutine)
			{
				StartCoroutine(NotAttack());
			}
			awake = true;
			// The fungus moves only when it sees the player
			if (InVision(player.transform.position)) 
			{
				agent.isStopped = false;
				movable = true;
			}
		}
		else
		{
			animator.SetBool("Mimic", true);
			awake = false;
		}
	}

	protected override bool InVision(Vector3 pos)
    {
		return Vector3.Distance(transform.position, pos) <= visionDistance;
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

		if (awake && !inAttackCoroutine)
		{
			StartCoroutine(Attack());
		}
    }

	private IEnumerator NotAttack()
	{
		inAttackCoroutine = true;

		yield return new WaitForSeconds(1.5f);

		inAttackCoroutine = false;
	}

	private IEnumerator Attack()
	{
		inAttackCoroutine = true;

        // Change to a random attack animation
		float attackMode = Random.value;

		if (attackMode < 0.2f) 
		{
			animator.SetTrigger("AttackRightTentacle1");
			attackController.ProjectileAttack(projectile, 0.5f, 2f);
			yield return new WaitForSeconds(1.5f);
		}
		else if (attackMode >= 0.2f && attackMode < 0.4f)
		{
			animator.SetTrigger("AttackLeftTentacle2");
			attackController.ProjectileAttack(projectile, 0.5f, 2f);
			yield return new WaitForSeconds(1.5f);
		}
		else if (attackMode >= 0.4f && attackMode < 0.6f)
		{
			animator.SetTrigger("AttackFourTentacle");
			attackController.ProjectileAttack(projectile, 0.5f, 2f);
			yield return new WaitForSeconds(1.5f);
		}
		else if (attackMode >= 0.6f && attackMode < 0.8f)
		{
			animator.SetTrigger("AttackRolling");
			attackController.AoeAttack(8f, 1.25f);
			yield return new WaitForSeconds(2f);
		}
		else if (attackMode >= 0.8f && attackMode <= 1.0f)
		{
			animator.SetTrigger("AttackSpreadSpore");
			attackController.AoeAttack(5f, 1.2f);
			yield return new WaitForSeconds(2.5f);
		}

		inAttackCoroutine = false;
	}

    protected override void UnderAttack()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     healthPoints--;
        //     animator.SetTrigger("TakeDamage1");
        // }
    }
    
    protected override IEnumerator Die()
    {
        animator.SetTrigger("DownSpin");
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
