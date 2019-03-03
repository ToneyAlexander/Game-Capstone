using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a Componenet that allows its GameObject to change the 
    /// current IGameState of a Game to a QuitGameState.
    /// </summary>
    public sealed class QuitGameStateChanger : MonoBehaviour, IGameStateChanger
    {
        #region IGameStateChanger
        public void ChangeGameState()
        {
            game.CurrentState = new QuitGameState();
        }
        #endregion

        /// <summary>
        /// The Game to put in a QuitGameState.
        /// </summary>
        [SerializeField]
        private Game game;
    }
}