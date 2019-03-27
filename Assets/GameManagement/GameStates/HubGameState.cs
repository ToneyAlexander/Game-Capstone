using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state when the game is at the hub scene and waiting for 
    /// the player to select an island to go to.
    /// </summary>
    [CreateAssetMenu]
    public sealed class HubGameState : GameState
    {
        public override void Enter()
        {
            Debug.Log("In HubState.Enter");
        }

        public override void Exit()
        {
            Debug.Log("In HubState.Exit");
        }
    }
}
