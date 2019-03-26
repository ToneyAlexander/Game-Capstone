using UnityEngine;
using UnityEngine.Events;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents an empty IGameState.
    /// </summary>
    public sealed class NullGameState : IGameState
    {
        public static NullGameState Instance = new NullGameState();

        private NullGameState()
        {
        }

        #region IGameState
        public UnityAction<Game> OnEnter
        {
            get { return Enter; }
        }

        public UnityAction<Game> OnExit
        {
            get { return (Game game) => {
                Debug.Log("Exiting NullGameState");
            }; }
        }

        public SceneReference SceneReference
        {
            get { return null; }
        }
        #endregion

        public void Enter(Game game)
        {
            // No-op
        }
    }
}