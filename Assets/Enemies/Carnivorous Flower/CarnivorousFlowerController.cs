using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousFlowerController : EnemyController
{
	private Animator animator;

	private bool inAttackCoroutine;

	protected override void Initialize () 
    {
        // Set up animator
		animator = GetComponent<Animator>();

		// Default spawnPos and movingRange
        spawnPos = transform.position;
        movingRange = 15f;
		chaseSpeed = 5f;
		movable = false;

		// Default vision
        visionAngle = 180f;
        visionDistance = 10f;
        attackDistance = 3f;

		inAttackCoroutine = false;

		// Initial animation
		animator.SetBool("Walk Forward", false);
		animator.SetBool("Walk Backward", false);
		animator.SetBool("Strafe left", false);
		animator.SetBool("Strafe Right", false);
		animator.SetBool("Sleeping", true);
	}

	protected override void UniqueUpdate()
    {
		// The carnivorous plants wake up when the player is within range
		if (InRange(player.transform.position))
		{
			animator.SetBool("Sleeping", false);
			// The carnivorous flower moves only when it sees the player
			if (InVision(player.transform.position)) 
			{
				agent.isStopped = false;
				movable = true;
			}
		}
		else
		{
			// Not move by default
			animator.SetBool("Walk Forward", false);
			animator.SetBool("Sleeping", true);
			agent.isStopped = true;
			movable = false;
		}

		DisplayVisionAndRange();
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

		// Stop moving and attack target (player character)
		animator.SetBool("Walk Forward", false);
        agent.isStopped = true;

		if (!inAttackCoroutine)
		{
			StartCoroutine(Attack());
		}

        // TODO: Cause damage
        // attackController.SetAttack("AttackMode", true);
    }

	private IEnumerator Attack()
	{
		inAttackCoroutine = true;

        // Change to a random attack animation
		float attackMode = Random.value;

		if (attackMode < 0.33f) 
		{
			animator.SetTrigger("Breath Attack");
		}
		else if (attackMode >= 0.33f && attackMode < 0.67f)
		{
			animator.SetTrigger("Bite");
		}
		else if (attackMode >= 0.67f && attackMode < 1.0f)
		{
			animator.SetTrigger("Breath Attack Surround");
		}
		yield return new WaitForSeconds(2f);

		inAttackCoroutine = false;
	}

    protected override void UnderAttack()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     healthPoints--;
        //     animator.SetTrigger("Take Damage");
        // }
    }
    
    public override IEnumerator Die()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
