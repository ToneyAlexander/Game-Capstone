namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents an IGameState where an island is generated and the player 
    /// can start exploring.
    /// </summary>
    public sealed class PlayIslandGameState : IGameState
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:CCC.GameManagement.GameStates.PlayIslandGameState"/> 
        /// class.
        /// </summary>
        /// <param name="sceneChanger">
        /// The SceneChanger to use to change a Game's current scene.
        /// </param>
        /// <param name="playIslandSceneReference">
        /// The SceneReference that represents the Scene with an island that 
        /// the player can explore.
        /// </param>
        public PlayIslandGameState(SceneChanger sceneChanger, 
            SceneReference playIslandSceneReference)
        {
            this.sceneChanger = sceneChanger;
            this.playIslandSceneReference = playIslandSceneReference;
        }

        #region IGameState
        public void Enter(Game game)
        {
            sceneChanger.ChangeToScene(playIslandSceneReference);
        }

        public void Exit(Game game)
        {
            // No-op
        }
        #endregion

        /// <summary>
        /// The SceneChanger used to change Scenes.
        /// </summary>
        private readonly SceneChanger sceneChanger;

        /// <summary>
        /// The SceneReference that represents the Scene that contains an 
        /// island that the player can explore.
        /// </summary>
        private readonly SceneReference playIslandSceneReference;
    }
}