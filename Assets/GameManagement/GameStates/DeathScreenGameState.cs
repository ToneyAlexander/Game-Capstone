using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state when the game is showing the player death screen.
    /// </summary>
    [CreateAssetMenu]
    public sealed class DeathScreenGameState : GameState
    {
        public override void Enter()
        {
            Debug.Log("In DeathScreenState.Enter");
        }

        public override void Exit()
        {
            Debug.Log("In DeathScreenState.Exit");
        }
    }
}
