using UnityEngine;

namespace CCC.Inputs
{
    /// <summary>
    /// Represents a Component that causes a GameObject to poll the input system
    /// for a given list of mouse buttons that an IDestinationMover is 
    /// interested in.
    /// </summary>
    sealed class MouseButtonDownDetector : MonoBehaviour
    {
        [SerializeField]
        private InputButtonList buttonList;

        private IDestinationMover destinationMover;

        [SerializeField]
        private InputManager inputManager;

        private void Start()
        {
            destinationMover = GetComponent<IDestinationMover>();
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
                    if (Input.GetButtonDown(button.Name))
                    {
                        inputManager.HandleMouseButtonDown(
                            button,
                            destinationMover,
                            Input.mousePosition
                        );
                    }
                }
            }
        }
    }
}
