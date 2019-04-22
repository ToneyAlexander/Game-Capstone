using CCC.Behaviors;
using CCC.GameManagement.GameStates;
using UnityEngine;

[RequireComponent(typeof(GameStateChanger))]
[RequireComponent(typeof(RemyDead))]
public sealed class KillablePlayer : MonoBehaviour, IKillable
{
    /// <summary>
    /// The GameStateChanger that will be used to change the game state.
    /// </summary>
    private GameStateChanger gameStateChanger;

    [SerializeField]
    private RemyDead remyDead;

    public void Die()
    {
        Debug.Log("Player '" + gameObject.name + "' died!");

        Instantiate(Resources.Load("Black Screen"));

        remyDead.Dead();

        //Destroy(gameObject);
        //gameStateChanger.ChangeGameState();
    }
    
    /*
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Die();
        }
    }*/

    #region MonoBehaviour Messages
    private void Awake()
    {
        gameStateChanger = GetComponent<GameStateChanger>();
        remyDead = GetComponent<RemyDead>();
    }
    #endregion

}
