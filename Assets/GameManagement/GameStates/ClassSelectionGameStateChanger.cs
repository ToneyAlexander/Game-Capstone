using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a Component that allows a GameObject to change the current 
    /// state of a Game.
    /// </summary>
    [RequireComponent(typeof(SceneChanger))]
    public sealed class ClassSelectionGameStateChanger : MonoBehaviour, 
        IGameStateChanger
    {
        /// <summary>
        /// The Game to change to the class selection state.
        /// </summary>
        [SerializeField]
        private Game game;

        /// <summary>
        /// The SceneChanger to use to change the current Scene of a Game.
        /// </summary>
        private SceneChanger sceneChanger;

        /// <summary>
        /// The SceneReference that represents the Scene used for class 
        /// selection.
        /// </summary>
        [SerializeField]
        private SceneReference classSelectionSceneReference;

        #region IGameStateChanger
        public void ChangeGameState()
        {
            game.CurrentState = new ClassSelectionGameState(sceneChanger, 
                classSelectionSceneReference);
        }
        #endregion

        #region MonoBehaviour Messages
        private void Awake()
        {
            sceneChanger = GetComponent<SceneChanger>();
        }
        #endregion
    }
}