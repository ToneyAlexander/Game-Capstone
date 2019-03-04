using UnityEngine;

namespace CCC.Inputs
{
    /// <summary>
    /// Represents a Component that causes a GameObject to poll the input system
    /// for a given list of mouse buttons that an IDestinationMover is 
    /// interested in.
    /// </summary>
    [RequireComponent(typeof(MousePositionDetector))]
    sealed class MouseMovementInputDetector : MonoBehaviour
    {
        [SerializeField]
        private InputButtonList buttonList;

        /// <summary>
        /// The CommandProcessor to send MoveToCommands to.
        /// </summary>
        [SerializeField]
        private CommandProcessor commandProcessor;

        /// <summary>
        /// The destination mover.
        /// </summary>
        private IDestinationMover destinationMover;

        /// <summary>
        /// The MousePositionDetector that this MouseButtonDownDetector will 
        /// use to get the world space location of the mouse cursor.
        /// </summary>
        private MousePositionDetector mousePositionDetector;

        #region MonoBehaviour Messages
        private void Awake()
        {
            destinationMover = GetComponent<IDestinationMover>();
            mousePositionDetector = GetComponent<MousePositionDetector>();
        }

        private void Start()
        {
            if (destinationMover == null)
            {
                Debug.LogError(gameObject + "doesn't have an IDestinationMover" +
                    " Component attached to it!");
            }
        }

        private void Update()
        {
            foreach (InputButton button in buttonList.Buttons)
            {
                if (destinationMover != null)
                {
                    if (Input.GetButton(button.Name))
                    {
                        Vector3 destination = 
                            mousePositionDetector.CalculateWorldPosition();
                            
                        // Only move if destination is valid (not clicked on 
                        // the void).
                        if (mousePositionDetector.IsValid(destination))
                        {
                            ICommand moveToCommand = 
                                new MoveToCommand(destinationMover, 
                                    transform.position, destination);
                            commandProcessor.ProcessCommand(moveToCommand);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
