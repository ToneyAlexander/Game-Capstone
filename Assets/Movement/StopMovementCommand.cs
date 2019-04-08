namespace CCC.Movement
{
    /// <summary>
    /// Represents a command that causes a GameObject to stop moving.
    /// </summary>
    public sealed class StopMovementCommand : ICommand
    {
        /// <summary>
        /// Create a new StopMovementCommand that can stop a given 
        /// MovementStopper.
        /// </summary>
        /// <returns>A new StopMovementCommand.</returns>
        /// <param name="movementStopper">
        /// The MovementStopper of the GameObject to stop movement of.
        /// </param>
        public static StopMovementCommand ForMovementStopper(
            MovementStopper movementStopper)
        {
            return new StopMovementCommand(movementStopper);
        }

        /// <summary>
        /// The MovementStopper that will stop its GameObject from moving.
        /// </summary>
        private readonly MovementStopper movementStopper;

        private StopMovementCommand(MovementStopper movementStopper)
        {
            this.movementStopper = movementStopper;
        }

        #region ICommand
        public void InvokeCommand()
        {
            movementStopper.Stop();
        }
        #endregion
    }
}