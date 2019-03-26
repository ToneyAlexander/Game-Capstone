using UnityEngine;
using UnityEngine.Events;

namespace CCC.GameManagement.GameStates
{
    public class HubGameState : IGameState
    {
        private readonly SceneReference hubSceneReference;

        public HubGameState(SceneReference hubSceneReference)
        {
            this.hubSceneReference = hubSceneReference;
        }

        #region IGameState
        public UnityAction<Game> OnEnter
        {
            get { return Enter; }
        }

        public UnityAction<Game> OnExit
        {
            get { return (Game game) => { }; }
        }

        public SceneReference SceneReference
        {
            get { return hubSceneReference; }
        }
        #endregion

        public void Enter(Game game)
        {
            Debug.Log("Entering HubGameState");
        }
    }
}
