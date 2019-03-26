using UnityEngine;
using UnityEngine.Events;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents an IGameState where a given Game displays the main menu 
    /// Scene.
    /// </summary>
    public sealed class MainMenuGameState : IGameState
    {
        /// <summary>
        /// The SceneReference that represents the Scene that contains the main 
        /// menu for a Game.
        /// </summary>
        private readonly SceneReference mainMenuSceneReference;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:CCC.GameManagement.GameStates.MainMenuGameState"/> 
        /// class.
        /// </summary>
        /// <param name="mainMenuSceneReference">
        /// The SceneReference that represents the Scene to change a Game to.
        /// </param>
        public MainMenuGameState(SceneReference mainMenuSceneReference)
        {
            this.mainMenuSceneReference = mainMenuSceneReference;
        }

        #region IGameState
        public UnityAction<Game> OnEnter
        {
            get { return (Game game) => {
                Debug.Log("MainMenuGameState OnEnter");
            }; }
        }

        public UnityAction<Game> OnExit
        {
            get { return (Game game) => {
                Debug.Log("MainMenuState OnExit");
            }; }
        }

        public SceneReference SceneReference
        {
            get { return mainMenuSceneReference; }
        }
        #endregion
    }
}