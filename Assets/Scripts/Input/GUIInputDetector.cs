using UnityEngine;

namespace CCC.Inputs
{
    /// <summary>
    /// Represents a Component that causes its GameObject to listen for and 
    /// respond to inputs related to the in-game GUI.
    /// </summary>
    public sealed class GUIInputDetector : MonoBehaviour
    {
        /// <summary>
        /// The CommandProcessor that this GUIInputDetector will send ICommands 
        /// to.
        /// </summary>
        [SerializeField]
        private CommandProcessor commandProcessor;

        /// <summary>
        /// The prefab that represents the info menu that this GUIInputDetector 
        /// will get an InfoMenuScript Component from.
        /// </summary>
        [SerializeField]
        private GameObject infoMenu;

        /// <summary>
        /// The InputButton that is used to toggle the class info tab of the 
        /// info menu.
        /// </summary>
        [SerializeField]
        private InputButton classInfoTabButton;

        /// <summary>
        /// The InputButton that is used to toggle the inventory tab of the 
        /// info menu.
        /// </summary>
        [SerializeField]
        private InputButton inventoryTabButton;

        /// <summary>
        /// The InputButton that is used to toggle the stats tab of the info 
        /// menu.
        /// </summary>
        [SerializeField]
        private InputButton statsTabButton;

        /// <summary>
        /// The InfoMenuScript to use.
        /// </summary>
        private InfoMenuScript infoMenuScript;

        #region MonoBehaviour messages
        private void Awake()
        {
            infoMenuScript = infoMenu.GetComponent<InfoMenuScript>();
        }

        private void Update()
        {
            if (Input.GetButtonDown(inventoryTabButton.Name))
            {
                ICommand command = 
                    new ToggleInventoryTabCommand(infoMenuScript);
                commandProcessor.ProcessCommand(command);
            }

            if (Input.GetButtonDown(statsTabButton.Name))
            {
                ICommand command = new ToggleStatsTabCommand(infoMenuScript);
                commandProcessor.ProcessCommand(command);
            }

            if (Input.GetButtonDown(classInfoTabButton.Name))
            {
                ICommand command = new ToggleClassInfoTabCommand(infoMenuScript);
                commandProcessor.ProcessCommand(command);
            }
        }
        #endregion
    }
}
