using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state of the game when the game is loading.
    /// </summary>
    [CreateAssetMenu]
    public sealed class GameLogoGameState : GameState
    {
        public override void Enter()
        {
            Debug.Log("In GameLogoGameState.Enter");
        }

        public override void Exit()
        {
            Debug.Log("In GameLogoGameState.Exit");
        }
    }
}