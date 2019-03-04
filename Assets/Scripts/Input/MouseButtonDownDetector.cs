using UnityEngine;

namespace CCC.Inputs
{
    /// <summary>
    /// Represents a Component that causes a GameObject to poll the input system
    /// for a given list of mouse buttons that an IDestinationMover is 
    /// interested in.
    /// </summary>
    [RequireComponent(typeof(MousePositionDetector))]
    sealed class MouseButtonDownDetector : MonoBehaviour
    {
        [SerializeField]
        private InputButtonList buttonList;

        private IDestinationMover destinationMover;

        private MousePositionDetector mousePositionDetector;

        [SerializeField]
        private InputManager inputManager;

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
                            inputManager.HandleMouseButtonDown(
                                button,
                                destinationMover,
                                destination
                            );
                        }
                    }
                }
            }
        }
        #endregion
    }
}
