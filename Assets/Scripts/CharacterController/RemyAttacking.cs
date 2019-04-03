using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MousePositionDetector))]

public class RemyAttacking : MonoBehaviour
{
    public static Vector3 attackDirection;
    public static Ability ability;

    MousePositionDetector mousePositionDetector;

    private Animator animator;
    private Vector3 lookAtEnemy;
    private readonly float EPSSION;
    private Vector3 lastDestination;

    private float timeLeft;
    private Vector3 dynamicAttackDirection;


    public GameObject swordOnHand;
    public GameObject swordOnBack;

    public bool startMagicAttack;

    private string thisMeleeBool;

    private void Awake()
    {
        mousePositionDetector = GetComponent<MousePositionDetector>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startMagicAttack = false;
        timeLeft = 5;
        dynamicAttackDirection = mousePositionDetector.CalculateWorldPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateToEnemy();
        RotateToEnemyDynamic();

        UnEquipNow();


        if (StopMagicAttack())
        {
            animator.SetBool("isFireballIgnite", false);
            animator.SetBool("isFireballVolley", false);
            animator.SetBool("isAblaze", false);
            animator.SetBool("isMagic4", false);
            animator.SetBool("isMagic5", false);
        }

        FiringFireball(startMagicAttack);

        UnEquipTimer();

    }




    public void MeleeAttack()
    {
        RemyMovement.destination = this.transform.position;
        thisMeleeBool = "is" + ability.TypeString;

        if (swordOnBack.activeSelf && thisMeleeBool != "isThrowingDagger")
        {
            animator.SetBool("isEquip", true);
            animator.SetBool(thisMeleeBool, true);
        }

        else
        {
           animator.SetBool(thisMeleeBool, true);
        }

    }



    public void MagicAttack(Ability ability)
    {

            if (ability.AbilityName.Equals("Fireball Ignite")) {
                RemyMovement.destination = this.transform.position;
                animator.SetBool("isFireballIgnite", true);
            }
            if (ability.AbilityName.Equals("Fireball Volley"))
            {
                RemyMovement.destination = this.transform.position;
                animator.SetBool("isFireballVolley", true);
            }
            if (ability.AbilityName.Equals("Ablaze"))
            {
                RemyMovement.destination = this.transform.position;
                animator.SetBool("isAblaze", true);
            }
            if (ability.AbilityName.Equals("Dash"))
            {
                //animator.SetBool("isMagic4", true);
            }
            if (ability.AbilityName.Equals("Empowered Mending"))
            {
                //animator.SetBool("isMagic5", true);
            }

    }


    void RotateToEnemyDynamic()
    {
        dynamicAttackDirection = mousePositionDetector.CalculateWorldPosition();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Volley")) {
            dynamicAttackDirection.y = transform.position.y;
            if (transform.position != dynamicAttackDirection)
            {
                lookAtEnemy = dynamicAttackDirection - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtEnemy), 20 * Time.deltaTime);
            }
            else
            {
                lookAtEnemy = transform.position;
            }
        }
    }


    void RotateToEnemy()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Standing Melee Attack Combo3")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Equip")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Ignite")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Ablaze")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Slash")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.WhirlwindSlash")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.RainOfDeath")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.VampireStrike")
            //|| animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Magic Attack 04")
            //|| animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Magic Attack 05")
            )
        {
            attackDirection.y = transform.position.y;

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



    void StopThisMelee()
    {
        animator.SetBool(thisMeleeBool, false);
        animator.SetBool("isEquip", false);
    }



    bool StopMagicAttack()
    {
        bool result = false;
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Volley") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Ignite") || 
                animator.GetCurrentAnimatorStateInfo(0).IsName("Ablaze"))

                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8)
            {
                result = true;
            }
        
        return result;
    }

    void EquipWeapon()
    {
        //Debug.Log("拿剑");
        swordOnHand.SetActive(true);
        swordOnBack.SetActive(false);
    }


    void UnEquipTimer()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle") && swordOnHand.activeSelf)
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
            && swordOnHand.activeSelf)
        {
            //Debug.Log("收剑动作开始");
            animator.SetBool("isUnEquip", true);
        }
    }

    void UnEquipWeapon()
    {
        //Debug.Log("收剑");
        swordOnHand.SetActive(false);
        swordOnBack.SetActive(true);
        animator.SetBool("isUnEquip", false);
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
