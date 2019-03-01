using UnityEngine;
using System.Collections;

public class FungusEnemyController : EnemyController 
{
	private Animator animator;

	protected override void Initialize () 
    {
        // Set up animator
		animator = GetComponent<Animator>();

		// Default spawnPos and movingRange
        spawnPos = transform.position;
        movingRange = 10f;
		movable = false;

		// Default vision
        visionAngle = 360f;
        visionDistance = 10f;
        attackDistance = 3f;

        // Default stat
        healthPoints = 100f;
	}

	protected override void UniqueUpdate()
    {
		// The fungus moves only when it sees the player
		if (InVision(player.transform.position + new Vector3(0.0f, 2.0f, 0.0f))) 
		{
			agent.isStopped = false;
			movable = true;
			animator.SetTrigger("AnyKey");
		}
		else
		{
			agent.isStopped = true;
			movable = false;
			animator.SetTrigger("Mimic");
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
			transform.position.y, 
			(playerPos - transform.position).z
			).normalized);

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

		if (attackMode < 0.2f) 
		{
			animator.SetTrigger("AttackRightTentacle1");
		}
		else if (attackMode >= 0.2f && attackMode < 0.4f)
		{
			animator.SetTrigger("AttackLeftTentacle2");
		}
		else if (attackMode >= 0.4f && attackMode < 0.6f)
		{
			animator.SetTrigger("AttackFourTentacle");
		}
		else if (attackMode >= 0.6f && attackMode < 0.8f)
		{
			animator.SetTrigger("AttackRolling");
		}
		else if (attackMode >= 0.8f && attackMode <= 1.0f)
		{
			animator.SetTrigger("AttackSpreadSpore");
		}
		yield return new WaitForSeconds(2f);
		Debug.Log("new");

		inCoroutine = false;
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
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
