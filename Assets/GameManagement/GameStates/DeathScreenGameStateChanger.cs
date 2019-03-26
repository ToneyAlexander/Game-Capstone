using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    public sealed class DeathScreenGameStateChanger : MonoBehaviour, 
        IGameStateChanger
    {
        [SerializeField]
        private Game game;

        [SerializeField]
        private SceneReference deathScreenSceneReference;

        #region IGameStateChanger
        public void ChangeGameState()
        {
            Debug.Log("DeathScreenGameStateChanger was called!");
            game.TransitionTo(new DeathScreenGameState(deathScreenSceneReference));
        }
        #endregion
    }
}