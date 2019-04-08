using UnityEngine;

namespace CCC.Movement
{
    /// <summary>
    /// Represents a Component that allows its GameObject to be told to stop 
    /// moving.
    /// </summary>
    [RequireComponent(typeof(RemyMovement))]
    public sealed class MovementStopper : MonoBehaviour
    {
        /// <summary>
        /// The RemyMovement Component that actually controls how the player 
        /// moves.
        /// </summary>
        private RemyMovement remyMovement;

        /// <summary>
        /// Stop this MovementStopper's GameObject from moving.
        /// </summary>
        public void Stop()
        {
            // A RemyMovement Component can be told to stop moving by setting 
            // its destination to its GameObject's current location.
            remyMovement.setDetination(transform.position);
        }

        #region MonoBehaviour Messages
        private void Awake()
        {
            remyMovement = GetComponent<RemyMovement>();
        }
        #endregion
    }
}