using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a Component that allows its GameObject to change the current 
    /// IGameState of a Game.
    /// </summary>
    public sealed class MainMenuGameStateChanger : MonoBehaviour, 
        IGameStateChanger
    {
        #region IGameStateChanger
        public void ChangeGameState()
        {
            game.TransitionTo(new MainMenuGameState(mainMenuSceneReference));
        }
        #endregion

        /// <summary>
        /// The Game to change the current IGameState of.
        /// </summary>
        [SerializeField]
        private Game game;

        /// <summary>
        /// The SceneReference that represents the Scene that contains a Game's 
        /// main menu.
        /// </summary>
        [SerializeField]
        private SceneReference mainMenuSceneReference;

        #region MonoBehaviour Messages
        // Just for testing
        // TODO: Remove
        private void Update()
        {
            if (Input.GetButtonDown("Fire3"))
            {
                Debug.Log("Switching to main menu.");
                ChangeGameState();
            }
        }
        #endregion
    }
}