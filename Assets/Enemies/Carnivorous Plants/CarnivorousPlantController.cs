using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousPlantController : EnemyController
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
        movingRange = 15f;
		chaseSpeed = 1f;
		movable = false;

		// Default vision
        visionAngle = 180f;
        visionDistance = 10f;
        attackDistance = 3f;

		awake = false;
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
		animator.SetBool("Walk Forward", false);
		agent.isStopped = true;
		movable = false;
		
		// The carnivorous plants wake up when the player is within range
		if (InRange(player.transform.position))
		{
			animator.SetBool("Sleeping", false);
			// Wait for seconds so the enemy does not attack immediately
			// Wakeup animation plays only when it is in Mimic state
			if (!awake && !inAttackCoroutine)
			{
				StartCoroutine(NotAttack());
			}
			awake = true;
			// The carnivorous flower moves only when it sees the player
			if (InVision(player.transform.position)) 
			{
				agent.isStopped = false;
				movable = true;
			}
		}
		else
		{
			animator.SetBool("Sleeping", true);
			awake = false;
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
    }
	
	private IEnumerator NotAttack()
	{
		inAttackCoroutine = true;

		yield return new WaitForSeconds(2.5f);

		inAttackCoroutine = false;
	}

	private IEnumerator Attack()
	{
		inAttackCoroutine = true;

        // Change to a random attack animation
		float attackMode = Random.value;

		if (attackMode < 0.33f) 
		{
			animator.SetTrigger("Breath Attack");
			attackController.ProjectileAttack(projectile, 1.5f);
			yield return new WaitForSeconds(2.5f);
		}
		else if (attackMode >= 0.33f && attackMode < 0.67f)
		{
			animator.SetTrigger("Bite");
			attackController.ProjectileAttack(projectile, 0.5f);
			yield return new WaitForSeconds(1.5f);
		}
		else if (attackMode >= 0.67f && attackMode < 1.0f)
		{
			animator.SetTrigger("Breath Attack Surround");
			attackController.ProjectileAttack(projectile, 2.0f);
			yield return new WaitForSeconds(2.5f);
		}

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
    
    protected override IEnumerator Die()
    {
		animator.SetBool("Sleeping", false);
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
