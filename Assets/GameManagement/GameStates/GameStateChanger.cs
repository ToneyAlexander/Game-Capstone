using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a Component that allows its GameObject to change the state 
    /// of the game.
    /// </summary>
    public sealed class GameStateChanger : MonoBehaviour
    {
        /// <summary>
        /// The IGameState to change the game to.
        /// </summary>
        [SerializeField]
        private GameState gameState;

        /// <summary>
        /// Change the state of the game.
        /// </summary>
        public void ChangeState()
        {
            Debug.Log("Changing game state!");
            GameManager.Instance.TransitionTo(gameState);
        }
    }
}