using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a Component that allows its GameObject to change the current 
    /// IGameState of a Game to a new PlayIslandGameState.
    /// </summary>
    public sealed class PlayIslandGameStateChanger : MonoBehaviour, 
        IGameStateChanger
    {
        /// <summary>
        /// The Game to change the current IGameState of.
        /// </summary>
        [SerializeField]
        private Game game;

        /// <summary>
        /// The SceneReference that represents the Scene that the new 
        /// PlayIslandGameState this PlayIslandGameStateChanger creates should 
        /// load.
        /// </summary>
        [SerializeField]
        private SceneReference sceneReference;

        #region IGameStateChanger
        public void ChangeGameState()
        {
            game.TransitionTo(new PlayIslandGameState(sceneReference));
        }
        #endregion
    }
}