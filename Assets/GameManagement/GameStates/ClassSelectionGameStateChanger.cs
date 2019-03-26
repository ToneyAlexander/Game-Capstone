using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a Component that allows a GameObject to change the current 
    /// state of a Game.
    /// </summary>
    public sealed class ClassSelectionGameStateChanger : MonoBehaviour, 
        IGameStateChanger
    {
        /// <summary>
        /// The Game to change to the class selection state.
        /// </summary>
        [SerializeField]
        private Game game;

        /// <summary>
        /// The SceneReference that represents the Scene used for class 
        /// selection.
        /// </summary>
        [SerializeField]
        private SceneReference classSelectionSceneReference;

        #region IGameStateChanger
        public void ChangeGameState()
        {
            Debug.Log("Calling game.TransitionTo ClassSelectionScene");
            game.TransitionTo(new ClassSelectionGameState(classSelectionSceneReference));
        }
        #endregion
    }
}