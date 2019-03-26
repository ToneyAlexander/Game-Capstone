using UnityEngine;
using UnityEngine.Events;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents an IGameState where a Game is quit.
    /// </summary>
    public sealed class QuitGameState : IGameState
    {
        #region IGameState
        public UnityAction<Game> OnEnter
        {
            get { return (Game game) => { 
                Debug.Log("QuitGameState OnEnter");
                game.Quit();
            }; }
        }

        public UnityAction<Game> OnExit
        {
            get { return (Game game) => {
                Debug.Log("QuitGameState OnExit");
            }; }
        }

        public SceneReference SceneReference
        {
            get { return null; }
        }
        #endregion
    }
}