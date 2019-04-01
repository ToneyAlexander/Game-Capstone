using System.Collections;
using UnityEngine;
using CCC.GameManagement.GameStates;

[RequireComponent(typeof(GameStateChanger))]
public class RemyDead : MonoBehaviour
{
    Animator animator;

    /// <summary>
    /// The GameStateChanger that will be used to change the state of the game.
    /// </summary>
    private GameStateChanger gameStateChanger;

    private bool isDead = false;

    #region MonoBehaviour Messages
    private void Awake()
    {
        gameStateChanger = GetComponent<GameStateChanger>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Died") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !isDead)
        {
            //StartCoroutine(DeathProcess());
            isDead = true;
            Debug.Log("RemyDead killing player");
            gameStateChanger.ChangeState();
        }

    }
    #endregion

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
