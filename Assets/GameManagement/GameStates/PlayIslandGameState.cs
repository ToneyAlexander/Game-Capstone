using UnityEngine;
using UnityEngine.Events;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents an IGameState where an island is generated and the player 
    /// can start exploring.
    /// </summary>
    public sealed class PlayIslandGameState : IGameState
    {
        /// <summary>
        /// The SceneReference that represents the Scene that contains an 
        /// island that the player can explore.
        /// </summary>
        private readonly SceneReference playIslandSceneReference;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:CCC.GameManagement.GameStates.PlayIslandGameState"/> 
        /// class.
        /// </summary>
        /// <param name="playIslandSceneReference">
        /// The SceneReference that represents the Scene with an island that 
        /// the player can explore.
        /// </param>
        public PlayIslandGameState(SceneReference playIslandSceneReference)
        {
            this.playIslandSceneReference = playIslandSceneReference;
        }

        #region IGameState
        public UnityAction<Game> OnEnter
        {
            get { return (Game game) => {
                Debug.Log("PlayIslandGameState OnEnter");
            }; }
        }

        public UnityAction<Game> OnExit
        { 
            get { return (Game game) => {
                Debug.Log("PlayIslandGameState OnExit");
            }; }
        }

        public SceneReference SceneReference
        {
            get { return playIslandSceneReference; }
        }
        #endregion
    }
}