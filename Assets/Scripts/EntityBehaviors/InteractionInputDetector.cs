using UnityEngine;

namespace CCC.Behaviors
{
    /// <summary>
    /// Represents a Component that causes a GameObject to detect input related 
    /// to interacting with another GameObject using the mouse and send the 
    /// an appropriate command.
    /// </summary>
    public sealed class InteractionInputDetector : MonoBehaviour
    {
        [SerializeField]
        private CommandProcessor commandProcessor;
        private AudioSource audio;
        /// <summary>
        /// The IInteractable that will be interacted with.
        /// </summary>
        private IInteractable interactable;

        #region MonoBehaviour Messages
        private void Awake()
        {
            interactable = GetComponent<IInteractable>();
            audio = GetComponent<AudioSource>();

            if (interactable == null)
            {
                Debug.LogError("[" + gameObject.name +
                    ".InteractionInputDetector]" + gameObject.name + " has no" +
                    " IInteractable Component!");
            }
        }

        private void OnMouseDown()
        {
            if (interactable != null)
            {
                audio.Play();
                ICommand interactCommand = new InteractCommand(interactable);
                commandProcessor.ProcessCommand(interactCommand);
            }
        }
        #endregion
    }
}
