using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state of the game where the player has quit.
    /// </summary>
    [CreateAssetMenu]
    public class QuitGameState : GameState
    {
        public override void Enter()
        {
            Debug.Log("In QuitState.Enter");
            GameManager.Instance.Quit();
        }

        public override void Exit()
        {
            Debug.Log("In QuitState.Exit");
            // No-op
        }
    }
}
