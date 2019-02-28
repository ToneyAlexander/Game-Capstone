using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyAttacking : MonoBehaviour
{
    public static Vector3 attackDirection;
    public static Ability ability;


    private Animator animator;
    private Vector3 lookAtEnemy;
    private float EPSSION;
    private Vector3 lastDestination;
    private bool isHoldingSword;
    private float timeLeft;


    public GameObject swordOnHand;
    public GameObject swordOnBack;

    public bool startMagicAttack;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startMagicAttack = false;
        isHoldingSword = false;
        timeLeft = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateToEnemy();

        if (Input.GetButtonDown("MeleeAttackTest"))
        {
            MeleeAttack();
        }

        EquipSword();

        if (StopMeleeAttack())
        {
            animator.SetBool("isIdleToMelee", false);
            animator.SetBool("isEquip", false);
        }

        UnEquipNow();
        UnEquipSword();


        if (StopMagicAttack())
        {
            animator.SetBool("isFireballIgnite", false);
            animator.SetBool("isFireballVolley", false);

        }

        FiringFireball(startMagicAttack);

        UnEquipTimer();

    }




    public void MeleeAttack()
    {
        RemyMovement.destination = this.transform.position;

        if (!isHoldingSword)
        {

            animator.SetBool("isEquip", true);
            animator.SetBool("isEquipToMelee", true);
            isHoldingSword = true;
        }
        else
        {
           animator.SetBool("isIdleToMelee", true);
        }

    }



    public void MagicAttack(Ability ability)
    {

        if (true) {
            RemyMovement.destination = this.transform.position;
            if (ability.AbilityName.Equals("Fireball Ignite")) {
                animator.SetBool("isFireballIgnite", true);
            }
            if (ability.AbilityName.Equals("Fireball Volley"))
            {
                animator.SetBool("isFireballVolley", true);
            }
        }

    }


    void RotateToEnemy()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Standing Melee Attack Combo3")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Equip")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Ignite")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Volley")
            ) {
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




    //stop actural melee attack and unequip sword
    bool StopMeleeAttack()
    {
        bool result = false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Standing Melee Attack Combo3") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75)
        {
            result = true;
        }
        return result;
    }



    bool StopMagicAttack()
    {
        bool result = false;
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Volley") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Ignite")) 

                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8)
            {
                result = true;
            }
        
        return result;
    }



    void EquipSword()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Equip")){

            RemyMovement.destination = this.transform.position;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6) {

                swordOnHand.SetActive(true);
                swordOnBack.SetActive(false);
                //Debug.Log("拿剑");
            }
        }
    }


    void UnEquipTimer()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle") && isHoldingSword)
        {
            if (timeLeft < -0.01)
            {
                timeLeft = 5;
            }
            timeLeft -= Time.deltaTime;
            //Debug.Log(timeLeft);
        }
    }


    void UnEquipNow()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle")
            && timeLeft < 0
            && isHoldingSword)
        {
            //Debug.Log("收剑动作开始");
            animator.SetBool("isUnEquip", true);
        }
    }


    void UnEquipSword()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.UnEquip"))
        {
            RemyMovement.destination = this.transform.position;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6)
            {
                swordOnHand.SetActive(false);
                swordOnBack.SetActive(true);
                isHoldingSword = false;
                //Debug.Log("真的收剑");
            }

            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8)
            {
                animator.SetBool("isUnEquip", false);
            }
        }

    }



    void DoNotFly()
    {
        attackDirection.y = this.transform.position.y;
    }



    void FiringFireball(bool start)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Ignite") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6 &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1
                )
        {
            start = true;
        }else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Volley") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4 &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            start = true;
        }
        else
        {
            start = false;
        }
    }
}
