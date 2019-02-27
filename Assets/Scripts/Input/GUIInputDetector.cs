using UnityEngine;

namespace CCC.Inputs
{
    /// <summary>
    /// Represents a Component that causes its GameObject to listen for and 
    /// respond to inputs related to the in-game GUI.
    /// </summary>
    public sealed class GUIInputDetector : MonoBehaviour
    {
        [SerializeField]
        private CommandProcessor commandProcessor;

        [SerializeField]
        private GameObject infoMenu;

        [SerializeField]
        private InputButton inventoryButton;

        #region MonoBehaviour messages
        private void Update()
        {
            if (Input.GetButtonDown(inventoryButton.Name))
            {
                ICommand command = new ToggleInventoryTabCommand(
                    infoMenu.GetComponent<InfoMenuScript>()
                );
                commandProcessor.ProcessCommand(command);
            }
        }
        #endregion
    }
}
