using UnityEngine;
using System.Collections;

public class FungusEnemyController : EnemyController 
{
	private Animator animator;
	bool anyKey =false;
	bool animation01 = false;
	bool animation02 = false;
	bool animation03 = false;
	bool animation04 = false;
	bool animation05 = false;

	protected override void Initialize () 
    {
        // Set up animator
		animator = GetComponent<Animator>();
	}

	protected override void UniqueUpdate()
    {
		if(animator){
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			if(Input.anyKeyDown){
				anyKey = true;
				animator.SetBool("AnyKey",anyKey);
			}
			if(Input.GetKeyDown(KeyCode.Alpha1)){
				animation01 = true;
				animator.SetBool("Animation01",animation01);
			}
			if(Input.GetKeyDown(KeyCode.Alpha2)){
				animation02 = true;
				animator.SetBool("Animation02",animation02);
			}
			if(Input.GetKeyDown(KeyCode.Alpha3)){
				animation03 = true;
				animator.SetBool("Animation03",animation03);
			}
			if(Input.GetKeyDown(KeyCode.Alpha4)){
				animation04 = true;
				animator.SetBool("Animation04",animation04);
			}
			if(Input.GetKeyDown(KeyCode.Alpha5)){
				animation05 = true;
				animator.SetBool("Animation05",animation05);
			}
			
			animator.SetFloat("h",h);
			animator.SetFloat ("v",v);
		}
	}

    protected override void Attack(Vector3 playerPos)
    {
        // Look at target (player character)
        transform.rotation = Quaternion.LookRotation((playerPos - transform.position).normalized);

        // Stop and attack target (player character)
        agent.isStopped = true;

        // Change to attack animation
        animator.SetBool("AttackMode", true);

        // TODO: Maybe other stuff...
        attackController.SetAttack("AttackMode", true);
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
