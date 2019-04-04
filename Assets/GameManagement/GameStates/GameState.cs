using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents a state that a game can be in.
    /// </summary>
    public abstract class GameState : ScriptableObject
    {
        /// <summary>
        /// Enter this GameState.
        /// </summary>
        public abstract void Enter();

        /// <summary>
        /// Exit this GameState.
        /// </summary>
        public abstract void Exit();
    }
}