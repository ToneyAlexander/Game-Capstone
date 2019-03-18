using CCC.Items;
using UnityEngine;

namespace CCC.Behaviors.Props
{
    /// <summary>
    /// Represents an interactable prop that acts like a chest meaning it drops 
    /// Items.
    /// </summary>
    public sealed class InteractableChest : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// The CommandProcessor that ICommands created by this 
        /// InteractableChest are sent to.
        /// </summary>
        [SerializeField]
        private CommandProcessor commandProcessor;

        /// <summary>
        /// The IItemDropper that actually drops Items for this 
        /// InteractableChest.
        /// </summary>
        [SerializeField]
        private IItemDropper itemDropper;

        /// <summary>
        /// The ItemGenerator that this InteractableChest uses to create Items 
        /// to drop.
        /// </summary>
        [SerializeField]
        private ItemGenerator itemGenerator;

        #region IInteractable
        public void RespondToInteraction()
        {
            if (itemDropper != null)
            {
                Item item = itemGenerator.GenerateItem();
                ICommand dropItemCommand = new DropItemCommand(itemDropper, 
                    item, transform.position);
                commandProcessor.ProcessCommand(dropItemCommand);
            }
        }
        #endregion

        #region MonoBehaviour Messages
        private void Awake()
        {
            itemDropper = GetComponent<IItemDropper>();

            if (itemDropper == null)
            {
                Debug.LogError("[" + gameObject.name + ".InteractableChest]" +
                    gameObject.name + " does not have an IItemDropper" + 
                    " Component!");
            }
        }
        #endregion
    }
}
