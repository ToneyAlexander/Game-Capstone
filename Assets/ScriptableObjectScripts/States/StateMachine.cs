using UnityEngine;

namespace States
{   
    /// <summary>
    /// Represents a finite state machine.
    /// </summary>
    [CreateAssetMenu]
    public sealed class StateMachine : ScriptableObject
    {
        [SerializeField]
        /// <summary>
        /// The description of what this Statemachine is for.
        /// </summary>
        private string description;

        [SerializeField]
        /// <summary>
        /// The State that this StateMachine initially starts in.
        /// </summary>
        private State initialState;

        /// <summary>
        /// The State that this StateMachine is currently in.
        /// </summary>
        private State currentState;

        public State CurrentState
        {
            get { return currentState; }
        }

        public void SetState(State state)
        {
            currentState = state;
            state.Handle();
        }
    }
}