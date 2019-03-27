using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state when the game is waiting for the player to select 
    /// their class.
    /// </summary>
    [CreateAssetMenu]
    public sealed class ClassSelectionGameState : GameState
    {
        public override void Enter()
        {
            Debug.Log("In ClassSelectionState.Enter");
        }

        public override void Exit()
        {
            Debug.Log("In ClassSelectionState.Exit");
        }
    }
}
