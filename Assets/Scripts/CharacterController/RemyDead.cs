using System.Collections;
using UnityEngine;
using CCC.GameManagement.GameStates;

[RequireComponent(typeof(DeathScreenGameStateChanger))]
public class RemyDead : MonoBehaviour
{
    Animator animator;
    private IGameStateChanger deathScreenGameStateChanger;

    private bool isDead = false;

    private void Awake()
    {
        deathScreenGameStateChanger = GetComponent<DeathScreenGameStateChanger>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Died") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !isDead)
        {
            //StartCoroutine(DeathProcess());
            isDead = true;
            Debug.Log("RemyDead killing player");
            deathScreenGameStateChanger.ChangeGameState();
        }

    }

    IEnumerator DeathProcess()
    {
        //Wait for the FadeIn.cs to finish
        yield return new WaitForSeconds(7.5f);
        //GetComponent<SceneChanger>().ChangeToScene(deathScene);

        //Destroy(gameObject);
        //gameStateChanger.ChangeGameState();
    }

    public void Dead()
    {
        RemyMovement.destination = this.transform.position;

        animator.SetBool("isDead", true);

        animator.SetBool("isRunning", false);

        animator.SetBool("isIdleToMelee", false);

        animator.SetBool("isFireballIgnite", false);

        animator.SetBool("isFireballVolley", false);

        animator.SetBool("isUnEquip", false);

        animator.SetBool("isEquipToMelee", false);

        animator.SetBool("isEquip", false);

        animator.SetBool("isAblaze", false);

        animator.SetBool("isMagic4", false);

        animator.SetBool("isMagic5", false);
    }
}
