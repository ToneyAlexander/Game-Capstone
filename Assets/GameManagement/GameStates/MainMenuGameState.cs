using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state of the game when the main menu is showed.
    /// </summary>
    [CreateAssetMenu]
    public sealed class MainMenuGameState : GameState
    {
        public override void Enter()
        {
            Debug.Log("In MainMenuState.Enter");
        }

        public override void Exit()
        {
            Debug.Log("In MainMenuState.Exit");
        }
    }
}
