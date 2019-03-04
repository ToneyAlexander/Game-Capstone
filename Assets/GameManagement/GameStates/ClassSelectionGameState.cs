namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a state of the game where the player's class is being 
    /// selected.
    /// </summary>
    sealed class ClassSelectionGameState : IGameState
    {
        /// <summary>
        /// The SceneChanger to use to change the Scene.
        /// </summary>
        private readonly SceneChanger sceneChanger;

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
        /// <param name="sceneChanger">
        /// The SceneChanger to use to change Scenes.
        /// </param>
        /// <param name="classSelectionSceneReference">
        /// The SceneReference that represents a Scene that is used for class 
        /// selection.
        /// </param>
        public ClassSelectionGameState(SceneChanger sceneChanger, 
            SceneReference classSelectionSceneReference)
        {
            this.sceneChanger = sceneChanger;
            this.classSelectionSceneReference = classSelectionSceneReference;
        }

        #region IGameState
        public void Enter(Game game)
        {
            sceneChanger.ChangeToScene(classSelectionSceneReference);
        }

        public void Exit(Game game)
        {
            // No-op
        }
        #endregion
    }
}