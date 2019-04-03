using CCC.Behaviors;
using CCC.GameManagement;
using CCC.GameManagement.GameStates;
using UnityEngine;

[RequireComponent(typeof(MainMenuGameStateChanger))]
[RequireComponent(typeof(RemyDead))]
public sealed class KillablePlayer : MonoBehaviour, IKillable
{
    private IGameStateChanger gameStateChanger;

    [SerializeField]
    private RemyDead remyDead;

    private void Awake()
    {
        gameStateChanger = GetComponent<MainMenuGameStateChanger>();
        remyDead = GetComponent<RemyDead>();
    }

    public void Die()
    {
        Debug.Log("Player '" + gameObject.name + "' died!");

        Instantiate(Resources.Load("Black Screen"));

        remyDead.Dead();

        //Destroy(gameObject);
        //gameStateChanger.ChangeGameState();
    }

    //TODO: REMOVE
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Die();
        }
    }
}
