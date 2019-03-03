namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents an IGameState where a given Game displays the main menu 
    /// Scene.
    /// </summary>
    public sealed class MainMenuGameState : IGameState
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:CCC.GameManagement.GameStates.MainMenuGameState"/> 
        /// class.
        /// </summary>
        /// <param name="sceneChanger">
        /// The SceneChanger to use to change a Game's current scene.
        /// </param>
        /// <param name="mainMenuSceneReference">
        /// The SceneReference that represents the Scene to change a Game to.
        /// </param>
        public MainMenuGameState(SceneChanger sceneChanger, 
            SceneReference mainMenuSceneReference)
        {
            this.sceneChanger = sceneChanger;
            this.mainMenuSceneReference = mainMenuSceneReference;
        }

        #region IGameState
        public void Enter(Game game)
        {
            sceneChanger.ChangeToScene(mainMenuSceneReference);
        }

        public void Exit(Game game)
        {
            // No-op
        }
        #endregion

        /// <summary>
        /// The SceneChanger to use to change Scenes.
        /// </summary>
        private readonly SceneChanger sceneChanger;

        /// <summary>
        /// The SceneReference that represents the Scene that contains the main 
        /// menu for a Game.
        /// </summary>
        private readonly SceneReference mainMenuSceneReference;
    }
}