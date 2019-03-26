using UnityEngine;
using UnityEngine.Events;

namespace CCC.GameManagement.GameStates
{
    public sealed class DeathScreenGameState : IGameState
    {
        private readonly SceneReference deathScreenSceneReference;

        public DeathScreenGameState(SceneReference deathScreenSceneReference)
        {
            this.deathScreenSceneReference = deathScreenSceneReference;
        }

        #region IGameState
        public UnityAction<Game> OnEnter
        {
            get { return (Game game) => {
                Debug.Log("DeathScreenGameState OnEnter");
            }; }
        }

        public UnityAction<Game> OnExit
        {
            get { return (Game game) => {
                Debug.Log("DeathScreenGameState OnExit");
            }; }
        }

        public SceneReference SceneReference
        {
            get { return deathScreenSceneReference; }
        }
        #endregion
    }
}
