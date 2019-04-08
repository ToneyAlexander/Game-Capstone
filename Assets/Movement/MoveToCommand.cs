using UnityEngine;

namespace CCC.Movement
{
    /// <summary>
    /// Represents an ICommand that moves an IDestinationMover to a given
    /// world space destination.
    /// </summary>
    public sealed class MoveToCommand : ICommand
    {
        /// <summary>
        /// The world space location that this MoveToCommand's IDestinationMover was
        /// at before this MoveToCommand was invoked.
        /// </summary>
        private readonly Vector3 start;

        /// <summary>
        /// The world space location that this MoveToCommand's IDestinationMover
        /// will start moving towards at after this MoveToCommand is invoked.
        /// </summary>
        private readonly Vector3 destination;

        /// <summary>
        /// The IDestinationMover that this MoveToCommand will direct to move.
        /// </summary>
        private readonly IDestinationMover destinationMover;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MoveToCommand"/> class.
        /// </summary>
        /// <param name="destinationMover">The IDestinatinMover to act on.</param>
        /// <param name="start">
        /// The world space location of the IDestinationMover before this 
        /// MoveToCommand is invoked.
        /// </param>
        /// <param name="destination">
        /// The world space location that the DestinationMover will start to move
        /// towards after this MoveToCommand is invoked.
        /// </param>
        public MoveToCommand(IDestinationMover destinationMover, Vector3 start, 
            Vector3 destination)
        {
            this.start = start;
            this.destination = destination;
            this.destinationMover = destinationMover;
        }

        public void InvokeCommand()
        {   
            destinationMover.MoveTo(destination);
        }
    }
}
