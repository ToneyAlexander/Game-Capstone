using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a Componenet that allows its GameObject to change the 
    /// current IGameState of a Game to a QuitGameState.
    /// </summary>
    public sealed class QuitGameStateChanger : MonoBehaviour, IGameStateChanger
    {
        /// <summary>
        /// The Game to put in a QuitGameState.
        /// </summary>
        [SerializeField]
        private Game game;

        #region IGameStateChanger
        public void ChangeGameState()
        {
            game.TransitionTo(new QuitGameState());
        }
        #endregion
    }
}