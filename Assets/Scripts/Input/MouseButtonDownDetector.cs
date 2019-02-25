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

                        // MousePositionDetector returns 
                        // Vector3.negativeInfinity when the mouse pointer is 
                        // either over an EventSystem object or when the ray 
                        // cast from the camera to it does not collide with a 
                        // GameObject in the scene. So, we have to check it 
                        // here. If destination is equal to 
                        // Vector3.negativeInfinity then we don't want to have 
                        // the player move since they shouldn't anyways. The 
                        // only problem is that Vector3's operator== does not 
                        // return true when comparing Vector3.negativeInfinity 
                        // to itself. So, we have to compare each component to 
                        // the components of Vector3.negativeInfinity. It is 
                        // safe to do this here without Mathf.Approximately 
                        // because Vector3.negativeInfinity always has the same 
                        // component values.
                        if (destination.x != Vector3.negativeInfinity.x &&
                            destination.y != Vector3.negativeInfinity.y &&
                            destination.z != Vector3.negativeInfinity.z)
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
    }
}
