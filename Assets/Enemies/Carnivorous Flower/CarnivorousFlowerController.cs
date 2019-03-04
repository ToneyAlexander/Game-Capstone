using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousFlowerController : EnemyController
{
	private Animator animator;

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

        // Default stat
        healthPoints = 10f;

		// Initial animation
		animator.SetBool("Walk Forward", false);
		animator.SetBool("Walk Backward", false);
		animator.SetBool("Strafe left", false);
		animator.SetBool("Strafe Right", false);
		animator.SetBool("Sleeping", true);
	}

	protected override void UniqueUpdate()
    {
		// The carnivorous flower awakes when the player is within range
		if (InRange(player.transform.position))
		{
			agent.isStopped = true;
			movable = false;
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
			agent.isStopped = true;
			movable = false;
			animator.SetBool("Sleeping", true);
			animator.SetBool("Walk Forward", false);
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

		if (!inCoroutine)
		{
			StartCoroutine(Attack());
		}

        // TODO: Cause damage
        // attackController.SetAttack("AttackMode", true);
    }

	private IEnumerator Attack()
	{
		inCoroutine = true;

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

		inCoroutine = false;
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
