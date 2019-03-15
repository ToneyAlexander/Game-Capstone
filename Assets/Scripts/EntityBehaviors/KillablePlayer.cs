using CCC.Behaviors;
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

        remyDead.Dead();

        Debug.Log("dead");

        //Destroy(gameObject);
        //gameStateChanger.ChangeGameState();
    }
}
