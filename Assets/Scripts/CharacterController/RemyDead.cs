using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.GameManagement.GameStates;
using CCC.GameManagement;

[RequireComponent(typeof(MainMenuGameStateChanger))]
public class RemyDead : MonoBehaviour
{
    Animator animator;
    private IGameStateChanger gameStateChanger;
    // Start is called before the first frame update

    [SerializeField]
    private SceneReference deathScene;

    private void Awake()
    {
        gameStateChanger = GetComponent<MainMenuGameStateChanger>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Died") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            StartCoroutine(DeathProcess());
        }

    }
    IEnumerator DeathProcess()
    {
        //Wait for the FadeIn.cs to finish
        yield return new WaitForSeconds(10);
        GetComponent<SceneChanger>().ChangeToScene(deathScene);
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
