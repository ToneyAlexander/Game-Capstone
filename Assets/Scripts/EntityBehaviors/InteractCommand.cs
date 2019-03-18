namespace CCC.Behaviors
{
    /// <summary>
    /// Represents the act of interacting with something.
    /// </summary>
    public sealed class InteractCommand : ICommand
    {
        /// <summary>
        /// The thing that was interacted with.
        /// </summary>
        private readonly IInteractable interactable;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:CCC.Behaviors.InteractCommand"/> class.
        /// </summary>
        /// <param name="interactable">The thing to interact with.</param>
        public InteractCommand(IInteractable interactable)
        {
            this.interactable = interactable;
        }

        #region ICommand
        public void InvokeCommand()
        {
            interactable.RespondToInteraction();
        }
        #endregion
    }
}
