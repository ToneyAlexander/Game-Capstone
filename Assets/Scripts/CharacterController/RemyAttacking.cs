using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyAttacking : MonoBehaviour
{
    public static Vector3 attackDirection;
    public static Ability ability;
    


    private Animator animator;
    private string ablilityName;
    private Vector3 lookAtEnemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ablilityName = ability.AbilityName;
    }

    // Update is called once per frame
    void Update()
    {
        MeleeAttack();
       
        MagicAttack();
    }

    void MeleeAttack()
    {
        if (Input.GetButton("MeleeAttackTest"))
        {
            RotateToEnemy();
            animator.SetBool("isIdleToMelee", true);
        }
    }

    void MagicAttack()
    {

        if (Input.GetButton("MagicAttackTest"))
        {
            RotateToEnemy();
            Debug.Log("abilityName: " + ablilityName);
            Debug.Log("attackDirection: " + attackDirection);
            animator.SetBool("isIdleToMagic", true);
        }
    }

    void RotateToEnemy()
    {
        if (transform.position != attackDirection)
        {
            lookAtEnemy = attackDirection - transform.position;
  
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtEnemy), 20 * Time.deltaTime);
        }
        else
        {
            lookAtEnemy = transform.position;
        }
    }
}
