using UnityEngine;

namespace CCC.Inputs
{
    /// <summary>
    /// Handles raw input.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An InputManager translates given raw input into ICommands that are then
    /// passed on to a CommandProcessor.
    /// </para>
    /// <para>
    /// In general, there should only ever be one instance of InputManager per
    /// game.
    /// </para>
    /// </remarks>
    [CreateAssetMenu(menuName = "Inputs/InputManager")]
    public sealed class InputManager : ScriptableObject
    {
        [SerializeField]
        private CommandProcessor commandProcessor;

        [SerializeField]
        private InputButton leftMouseButton;

        public void HandleHorizontalAxisInput(float axisValue, Movable movable)
        {
            Vector3 velocity = Vector3.right * axisValue;
            SendMoveCommand(velocity, movable);
        }

        public void HandleVerticalAxisInput(float axisValue, Movable movable)
        {
            Vector3 velocity = Vector3.forward * axisValue;
            SendMoveCommand(velocity, movable);
        }

        /// <summary>
        /// Handles a mouse InputButton being pressed down.
        /// </summary>
        /// <param name="button">
        /// The mouse InputButton that was pressed down.
        /// </param>
        /// <param name="mouseControllable">
        /// The Movable that will react to the mouse InputButton being pressed
        /// down.
        /// </param>
        /// <param name="mousePosition">Mouse position.</param>
        public void HandleMouseButtonDown(InputButton button, 
            IDestinationMover destinationMover, Vector3 worldSpaceDestination)
        {
            if (button.Equals(leftMouseButton))
            {
                commandProcessor.ProcessCommand(new MoveToCommand(
                    destinationMover,
                    destinationMover.Position,
                    worldSpaceDestination
                ));
            }
        }

        private void SendMoveCommand(Vector3 velocity, Movable movable)
        {
            ICommand command = new MoveCommand(movable, velocity);
            commandProcessor.ProcessCommand(command);
        }
        public void SendPauseCommand()
        {
            ICommand command = new PauseCommand(GameSystem.getMenu("Pause"));
            commandProcessor.ProcessCommand(command);
        }
    }
}
