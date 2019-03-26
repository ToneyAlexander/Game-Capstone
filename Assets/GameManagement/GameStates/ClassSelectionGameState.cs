using UnityEngine;
using UnityEngine.Events;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a state of the game where the player's class is being 
    /// selected.
    /// </summary>
    sealed class ClassSelectionGameState : IGameState
    {
        /// <summary>
        /// The SceneReference that represents the Scene to change to when 
        /// entering this ClassSelectionGameState.
        /// </summary>
        private readonly SceneReference classSelectionSceneReference;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:CCC.GameManagement.GameStates.ClassSelectionGameState"/> 
        /// class.
        /// </summary>
        /// <param name="classSelectionSceneReference">
        /// The SceneReference that represents a Scene that is used for class 
        /// selection.
        /// </param>
        public ClassSelectionGameState(SceneReference classSelectionSceneReference)
        {
            this.classSelectionSceneReference = classSelectionSceneReference;
        }

        #region IGameState
        public UnityAction<Game> OnEnter
        {
            get { return Enter; }
        }

        public UnityAction<Game> OnExit
        {
            get { return (Game game) => { }; }
        }

        public SceneReference SceneReference
        {
            get { return classSelectionSceneReference; }
        }
        #endregion

        public void Enter(Game game)
        {
            Debug.Log("ClassSelectionGameState OnEnter");
        }
    }
}