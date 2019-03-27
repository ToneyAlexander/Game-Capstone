using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state of the game that also loads a new scene.
    /// </summary>
    [CreateAssetMenu]
    public sealed class SceneState: GameState
    {
        /// <summary>
        /// The SceneReference that represents the Scene to load.
        /// </summary>
        [SerializeField]
        private SceneReference sceneReference;

        /// <summary>
        /// The GameState that the game should be put into after the new Scene 
        /// is loaded.
        /// </summary>
        [SerializeField]
        private GameState gameState;

        public override void Enter()
        {
            Debug.Log("Entering SceneState " + name);
            SceneChanger.Instance.ChangeToScene(sceneReference, gameState.Enter);
        }

        public override void Exit()
        {
            Debug.Log("Exiting SceneState " + name);
        }
    }
}