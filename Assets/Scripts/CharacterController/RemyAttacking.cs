using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyAttacking : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

            animator.SetBool("isIdleToMelee", true);
        }
    }

    void MagicAttack()
    {
        if (Input.GetButton("MagicAttackTest"))
        {

            animator.SetBool("isIdleToMagic", true);
        }
    }
}
