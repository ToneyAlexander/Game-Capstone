using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a Component that allows its GameObject to change the current 
    /// IGameState of a Game to a new PlayIslandGameState.
    /// </summary>
    [RequireComponent(typeof(SceneChanger))]
    public sealed class PlayIslandGameStateChanger : MonoBehaviour, 
        IGameStateChanger
    {
        #region IGameStateChanger
        public void ChangeGameState()
        {
            game.CurrentState = new PlayIslandGameState(sceneChanger, 
                sceneReference);
        }
        #endregion

        /// <summary>
        /// The Game to change the current IGameState of.
        /// </summary>
        [SerializeField]
        private Game game;

        /// <summary>
        /// The SceneChanger that will be used to change the current Scene of 
        /// a Game.
        /// </summary>
        private SceneChanger sceneChanger;

        /// <summary>
        /// The SceneReference that represents the Scene that the new 
        /// PlayIslandGameState this PlayIslandGameStateChanger creates should 
        /// load.
        /// </summary>
        [SerializeField]
        private SceneReference sceneReference;

        #region MonoBehaviour Messages
        private void Awake()
        {
            sceneChanger = GetComponent<SceneChanger>();
        }
        #endregion
    }
}