using CCC.Behaviors;
using CCC.GameManagement.GameStates;
using UnityEngine;

[RequireComponent(typeof(MainMenuGameStateChanger))]
public sealed class KillablePlayer : MonoBehaviour, IKillable
{
    private IGameStateChanger gameStateChanger;

    private void Awake()
    {
        gameStateChanger = GetComponent<MainMenuGameStateChanger>();
    }

    public void Die()
    {
        Debug.Log("Player '" + gameObject.name + "' died!");
        Destroy(gameObject);
        gameStateChanger.ChangeGameState();
    }
}
